using UnityEngine.XR;

namespace UnityEngine.Rendering.Universal
{
    internal class MobileBlurUrpPass : ScriptableRenderPass
    {
        public Material material;

        private RenderTargetIdentifier source;
        private RenderTargetIdentifier blurTemp = new RenderTargetIdentifier(blurTempString);
        private RenderTargetIdentifier blurTemp1 = new RenderTargetIdentifier(blurTemp1String);
        private RenderTargetIdentifier blurTex = new RenderTargetIdentifier(blurTexString);
        private RenderTargetIdentifier tempCopy = new RenderTargetIdentifier(tempCopyString);

        private readonly string tag;
        private readonly int kernelSize;
        private readonly float blurAmount;
        private readonly MobileBlurUrp.Algorithm algorithm;
        private Texture2D blurMask, previous;

        static readonly string kernelKeyword = "KERNEL";
        static readonly int blurAmountString = Shader.PropertyToID("_BlurAmount");
        static readonly int maskTextureString = Shader.PropertyToID("_MaskTex");
        static readonly int blurTexString = Shader.PropertyToID("_BlurTex");
        static readonly int blurTempString = Shader.PropertyToID("_BlurTemp");
        static readonly int blurTemp1String = Shader.PropertyToID("_BlurTemp1");
        static readonly int tempCopyString = Shader.PropertyToID("_TempCopy");

        private int numberOfPasses, pass;
        RenderTextureDescriptor opaqueDesc, half, quarter, eighths, sixths;

        public MobileBlurUrpPass(RenderPassEvent renderPassEvent, Material material,
            MobileBlurUrp.Algorithm algorithm, int kernelSize, float blurAmount, Texture2D blurMask, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.tag = tag;
            this.material = material;
            this.algorithm = algorithm;
            this.blurAmount = blurAmount;
            this.kernelSize = kernelSize;
            this.blurMask = blurMask == null ? Texture2D.whiteTexture : blurMask;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (blurAmount == 0)
                return;

            if (XRSettings.enabled)
            {
                opaqueDesc = XRSettings.eyeTextureDesc;
                half = XRSettings.eyeTextureDesc;
                half.height /= 2; half.width /= 2;
                quarter = XRSettings.eyeTextureDesc;
                quarter.height /= 4; quarter.width /= 4;
                eighths = XRSettings.eyeTextureDesc;
                eighths.height /= 8; eighths.width /= 8;
                sixths = XRSettings.eyeTextureDesc;
                sixths.height /= XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePass ? 8 : 16; sixths.width /= XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePass ? 8 : 16;
            }
            else
            {
                opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
                half = new RenderTextureDescriptor(opaqueDesc.width / 2, opaqueDesc.height / 2);
                quarter = new RenderTextureDescriptor(opaqueDesc.width / 4, opaqueDesc.height / 4);
                eighths = new RenderTextureDescriptor(opaqueDesc.width / 8, opaqueDesc.height / 8);
                sixths = new RenderTextureDescriptor(opaqueDesc.width / 16, opaqueDesc.height / 16);
                opaqueDesc.depthBufferBits = 0;
                half.depthBufferBits = 0;
                quarter.depthBufferBits = 0;
                eighths.depthBufferBits = 0;
                sixths.depthBufferBits = 0;
            }

            CommandBuffer cmd = CommandBufferPool.Get(tag);
            cmd.GetTemporaryRT(tempCopyString, opaqueDesc, FilterMode.Bilinear);
            cmd.Blit(source, tempCopy);

            if (kernelSize == 2)
                material.DisableKeyword(kernelKeyword);
            else
                material.EnableKeyword(kernelKeyword);

            if (blurMask != null || previous != blurMask)
            {
                previous = blurMask;
                material.SetTexture(maskTextureString, blurMask);
            }

            pass = algorithm == MobileBlurUrp.Algorithm.Box ? 0 : 1;
            numberOfPasses = Mathf.Clamp(Mathf.CeilToInt(blurAmount * 4), 1, 4);
            material.SetFloat(blurAmountString, numberOfPasses > 1 ? blurAmount > 1 ? blurAmount : (blurAmount * 4 - Mathf.FloorToInt(blurAmount * 4 - 0.001f)) * 0.5f + 0.5f : blurAmount * 4);

            if (numberOfPasses == 1)
            {
                cmd.GetTemporaryRT(blurTexString, half, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, pass);
            }
            else if (numberOfPasses == 2)
            {
                cmd.GetTemporaryRT(blurTexString, half, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, quarter, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTemp, material, pass);
                cmd.Blit(blurTemp, blurTex, material, pass);
            }
            else if (numberOfPasses == 3)
            {
                cmd.GetTemporaryRT(blurTexString, quarter, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, eighths, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, pass);
                cmd.Blit(blurTex, blurTemp, material, pass);
                cmd.Blit(blurTemp, blurTex, material, pass);
            }
            else if (numberOfPasses == 4)
            {
                cmd.GetTemporaryRT(blurTexString, quarter, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, eighths, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTemp1String, sixths, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, pass);
                cmd.Blit(blurTex, blurTemp, material, pass);
                cmd.Blit(blurTemp, blurTemp1, material, pass);
                cmd.Blit(blurTemp1, blurTemp, material, pass);
                cmd.Blit(blurTemp, blurTex, material, pass);
            }

            cmd.Blit(tempCopy, source, material, 2);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(blurTemp1String);
            cmd.ReleaseTemporaryRT(tempCopyString);
            cmd.ReleaseTemporaryRT(blurTexString);
            cmd.ReleaseTemporaryRT(blurTempString);
        }
    }
}
