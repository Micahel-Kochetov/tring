using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.Common.Service;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class PreviewVideoController : AnalyticsController
    {
        [Inject]
        PreviewVideoView view;
        [Inject]
        StopTheFunController stopTheFunController;
        [Inject]
        RecordVideoController recordVideoController;
        [Inject]
        ScreenRecordingService screenRecordingController;
        [Inject]
        PreshareController preshareController;
        string videoPath;

        public override void Init(string id)
        {
            base.Init(id);
            view.Init();
            view.OnPlay += OnPlayVideoHandler;
            view.OnGetVideos += OnGetVideosHandler;
            //view.OnVideoSelected += OnVideoSelectedHandler;
            view.OnReRecord += OnReRecordHandler;
            view.OnClose += OnCloseHandler;
            view.OnGallery += OnGalleryHandler;
        }

        public override void Dispose()
        {
            view.Dispose();
            view.OnPlay -= OnPlayVideoHandler;
            view.OnGetVideos -= OnGetVideosHandler;
            //view.OnVideoSelected -= OnVideoSelectedHandler;
            view.OnReRecord -= OnReRecordHandler;
            view.OnClose -= OnCloseHandler;
            view.OnGallery -= OnGalleryHandler;
        }
        public override void Activate()
        {
            Activate(videoPath);
        }

        public void Activate(string videoPath)
        {
            base.Activate();
            this.videoPath = videoPath;
            view.Show();
            view.PrepareVideo(videoPath);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.Hide();
        }


        private void OnGalleryHandler()
        {
            Deactivate();
        }

        void OnPlayVideoHandler()
        {
            view.PlayVideo();
        }

        //void OnVideoSelectedHandler(int index)
        //{
        //    if (videoPathes.Length > index)
        //    {
        //        view.PrepareVideo(videoPathes[index]);
        //    }
        //}

        void OnCloseHandler()
        {
            stopTheFunController.Activate();
        }

        void OnGetVideosHandler()
        {
            Deactivate();
            preshareController.Activate();
        }

        void OnReRecordHandler()
        {
            Deactivate();
            recordVideoController.Activate();
            screenRecordingController.StartRecording();
        }
    }
}
