namespace UnityEngine.Rendering.Universal
{
    public class MobileBlurUrp : ScriptableRendererFeature
    {
        public enum Algorithm
        {
            Box = 1,
            Gaussian = 2
        }

        [System.Serializable]
        public class MobileBlurSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;

            public Algorithm Algorithm = Algorithm.Box;

            [Range(0, 2)]
            public float BlurAmount = 1f;

            [Range(2, 3)]
            public int KernelSize = 2;

            public Texture2D BlurMask;

            public Material BlitMaterial = null;
        }

        public MobileBlurSettings settings = new MobileBlurSettings();

        MobileBlurUrpPass mobileBlurLwrpPass;

        public override void Create()
        {
            mobileBlurLwrpPass = new MobileBlurUrpPass(settings.Event, settings.BlitMaterial, settings.Algorithm, settings.KernelSize, settings.BlurAmount, settings.BlurMask, this.name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            mobileBlurLwrpPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(mobileBlurLwrpPass);
        }
    }
}

