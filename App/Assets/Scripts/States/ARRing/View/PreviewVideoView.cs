using Assets.Scripts.Common;
using Common.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.States.ARRing.View
{
    public class PreviewVideoView : ModalView
    {

        public event Action OnReRecord;
        public event Action OnGetVideos;
        public event Action OnPlay;
        public event Action<int> OnVideoSelected;
        public event Action OnClose;
        public event Action OnGallery;

        [SerializeField]
        ModalButton reRecordButton;
        [SerializeField]
        ModalButton getVideosButton;
        [SerializeField]
        ModalButton playButton;
        [SerializeField]
        ModalButton stopButton;
        [SerializeField]
        ModalButton closeButton;
        [SerializeField]
        ModalButton galleryButton;
        [SerializeField]
        VideoPlayer videoPlayer;
        [SerializeField]
        RawImage background;
        [Header("Video Preview Buttons")]
        [SerializeField]
        GameObject videoButtonTemplate;
        [SerializeField]
        Transform videoButtonContainer;
        [SerializeField]
        RawImage selectedImage;
        [SerializeField]
        ModalButton selectedVideoButton;
        ModalButton[] videoButtons;
        RawImage[] videoButtonImage;

        int selectedIndex = 0;

        protected override void AddButtons()
        {
            AddButton(reRecordButton, ReRecordHandler);
            AddButton(getVideosButton, GetVideosHandler);
            AddButton(playButton, PlayVideoHanlder);
            AddButton(closeButton, CloseHandler);
            AddButton(stopButton, StopVideo);
            AddButton(galleryButton, OpenGalleryHandler);
        }

        private void OpenGalleryHandler()
        {
            OnGallery?.Invoke();
        }

        //public void Show(string[] previewPathes)
        //{
        //    videoButtons = new ModalButton[previewPathes.Length];
        //    videoButtonImage = new RawImage[previewPathes.Length];

        //    for (int i = 0; i < previewPathes.Length; i++)
        //    {
        //        GameObject newVideoButton = Instantiate(videoButtonTemplate, videoButtonContainer, false);
        //        newVideoButton.name = "video" + i;
        //        ModalButton mb = newVideoButton.GetComponent<ModalButton>();
        //        int index = i;
        //        AddButton(mb, () => OnVideoSelectedHandler(index));
        //        mb.Subscribe();
        //        videoButtons[i] = mb;

        //        RawImage ri = newVideoButton.transform.Find("previewMask/preview").GetComponent<RawImage>();
        //        ri.texture = Utility.CropToSquareImage(Utility.LoadTexture(previewPathes[i]));
        //        videoButtonImage[i] = ri;
        //    }

        //    videoButtons[0].gameObject.SetActive(false);
        //    selectedImage.texture = videoButtonImage[0].texture;
        //    stopButton.gameObject.SetActive(false);
        //    base.Show();
        //}

        public override void Show()
        {
            base.Show();
            videoPlayer.loopPointReached += StopVideoHandler;
        }

        public override void Hide()
        {
            base.Hide();
            videoPlayer.Stop();
            videoPlayer.loopPointReached -= StopVideoHandler;
            videoPlayer.frameReady -= VideoPlayer_frameReady;
            //for (int i = 0; i < videoButtons.Length; i++)
            //{
            //    Destroy(videoButtons[i].gameObject);
            //}
        }

        void CloseHandler()
        {
            OnClose?.Invoke();
        }

        void PlayVideoHanlder()
        {
            OnPlay?.Invoke();
        }

        void GetVideosHandler()
        {
            OnGetVideos?.Invoke();
        }

        void ReRecordHandler()
        {
            OnReRecord?.Invoke();
        }

        private void SetBackgroundTexture(Texture texture)
        {
            if (texture == null)
            {
                Debug.Log("Setting null image to preview component");
            }
            background.texture = texture;
        }

        public void PrepareVideo(string path)
        {
            Debug.Log("Prepare video at path:" + path);
            videoPlayer.url = path;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.playOnAwake = false;
            videoPlayer.Play();
            videoPlayer.sendFrameReadyEvents = true;
            videoPlayer.frameReady += VideoPlayer_frameReady;
        }

        private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
        {
            videoPlayer.frameReady -= VideoPlayer_frameReady;
            SetBackgroundTexture(videoPlayer.texture);
            if (frameIdx > 0)
            {
                videoPlayer.Pause();
            }
        }

        public void StopVideo()
        {
            stopButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            videoPlayer.time = 0;
            videoPlayer.Pause();
        }

        void StopVideoHandler(VideoPlayer source)
        {
            stopButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            source.time = 0;
            source.Pause();
        }

        public void PlayVideo()
        {
            UnityEngine.Debug.Log("Try to play video at path:" + videoPlayer.url);
            playButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
            videoPlayer.Play();
        }

        void OnVideoSelectedHandler(int index)
        {
            stopButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            videoButtons[selectedIndex].gameObject.SetActive(true);
            videoButtons[index].gameObject.SetActive(false);
            selectedIndex = index;
            selectedImage.texture = videoButtonImage[index].texture;
            selectedVideoButton.transform.SetSiblingIndex(index);
            OnVideoSelected?.Invoke(index);
        }
    }
}
