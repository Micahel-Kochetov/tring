using Assets.Scripts.Common.Utils;
using Assets.Scripts.States.Common.Interface;
using Common.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.States.ARRing.View
{
    public class VideoPreviewIcon : ModalView, ISelectable
    {
        public event Action<VideoPreviewIcon, bool> OnSelectionChange;
        public event Action<VideoPreviewIcon> OnVideoStarted;
        [SerializeField]
        SelectableModalButton selectBtn;
        [SerializeField]
        SelectableModalButton playbackBtn;
        [SerializeField]
        RectTransform selectionFrame;
        [SerializeField]
        VideoPlayer videoPlayer;
        [SerializeField]
        RawImage playerTexture;
        bool selected;
        bool isPlaying;
        Texture2D previewImage;
        IVideoPreviewPath videoContainer;

        public void Init(Texture2D preview, IVideoPreviewPath videoContainer)
        {
            base.Init();
            SetDeselected();
            previewImage = preview;
            this.videoContainer = videoContainer;
            ScaleImageForRectContainer(previewImage, playerTexture.rectTransform);
        }

        public override void Show()
        {
            base.Show();
            videoPlayer.source = VideoSource.Url;
            videoPlayer.playOnAwake = false;
            videoPlayer.sendFrameReadyEvents = true;
            videoPlayer.frameReady -= VideoPlayer_frameReady;
            videoPlayer.loopPointReached -= StopVideoHandler;
            videoPlayer.frameReady += VideoPlayer_frameReady;
            videoPlayer.loopPointReached += StopVideoHandler;
            IsPlaying = false; 
        }

        public override void Hide()
        {
            base.Hide();
            videoPlayer.Stop();
            videoPlayer.loopPointReached -= StopVideoHandler;
            videoPlayer.frameReady -= VideoPlayer_frameReady;
        }

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(selectBtn, OnSelectHandler);
            AddButton(playbackBtn, OnPlaybackHandler);
        }

        void ScaleImageForRectContainer(Texture2D tex, RectTransform container) {
            var height = container.rect.height;
            var scaler = new TextureScaleSingleThread();
            scaler.Point(tex, (int)height);
            Resources.UnloadUnusedAssets();
        }

        private void SetPlayerTexture(Texture texture)
        {
            if (texture == null)
            {
                Debug.Log("Setting null image to preview component");
            }
            playerTexture.texture = texture;
        }

        private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
        {
            SetPlayerTexture(videoPlayer.texture);
        }

        void StopVideo()
        {
            videoPlayer.Stop();
            playerTexture.texture = previewImage;
        }

        void StopVideoHandler(VideoPlayer source)
        {
            playbackBtn.SetDeselected();
            source.Stop();
            playerTexture.texture = previewImage;
        }

        public void PlayVideo()
        {
            videoPlayer.url = videoContainer.ConvertedVideoPath;
            videoPlayer.Play();
        }

        private void OnPlaybackHandler()
        {
            IsPlaying = !IsPlaying;
            if (IsPlaying)
            {
                OnVideoStarted?.Invoke(this);
            }
        }

        private void OnSelectHandler()
        {
            selected = !selected;
            OnSelectionChange?.Invoke(this, IsSelected);
        }

        public void SetSelected()
        {
            selected = true;
            UpdateUIState(selected);
        }

        public void SetDeselected()
        {
            selected = false;
            UpdateUIState(selected);
        }

        public void UpdateUIState(bool selected)
        {
            this.selected = selected;
            selectionFrame.gameObject.SetActive(selected);
            selectBtn.UpdateUIState(selected);
        }

        public bool IsSelected { get { return selected; } }

        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }

            set
            {
                isPlaying = value;
                if (isPlaying)
                {
                    PlayVideo();
                }
                else
                {
                    StopVideo();
                }
                playbackBtn.UpdateUIState(isPlaying);
            }
        }
    }
}
