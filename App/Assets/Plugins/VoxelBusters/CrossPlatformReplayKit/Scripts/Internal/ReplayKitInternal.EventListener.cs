using VoxelBusters.ReplayKit.Common.DesignPatterns;

namespace VoxelBusters.ReplayKit
{
	using Internal;
    using UnityEngine;

    internal partial class ReplayKitInternal : SingletonPattern<ReplayKitInternal>, INativeCallbackListener
    {
        #region Events

        public  event ReplayKitDelegates.InitialiseCallback                 DidInitialiseEvent;
        public  event ReplayKitDelegates.RecordingStateChangedCallback      DidRecordingStateChangeEvent;
        private event ReplayKitDelegates.PreviewStateChangedCallback        DidPreviewStateChangeEvent; // For future updates, we provide preview states. So currently making it private

        #endregion


        #region INativeCallbackListener implementation

        public void OnInitialiseSuccess ()
		{
			if (DidInitialiseEvent != null) 
			{
                DidInitialiseEvent(ReplayKitInitialisationState.Success, string.Empty);
			}
		}

        public void OnInitialiseFailed(string message)
        {
            if (DidInitialiseEvent != null)
            {
                DidInitialiseEvent(ReplayKitInitialisationState.Failed, message);
            }
        }

        public void OnRecordingStarted ()
		{
#if !DONT_ALLOW_REPLAY_KIT_PAUSE_RESUME_AUDIO_LISTENER
            AudioListener.pause = m_audioListenerStatus;
#endif

            if (DidRecordingStateChangeEvent != null) 
			{
                DidRecordingStateChangeEvent(ReplayKitRecordingState.Started, string.Empty);
			}
		}

        public void OnRecordingStopped()
        {
#if !DONT_ALLOW_REPLAY_KIT_PAUSE_RESUME_AUDIO_LISTENER
            AudioListener.pause = m_audioListenerStatus;
#endif

            if (DidRecordingStateChangeEvent != null)
            {
                DidRecordingStateChangeEvent(ReplayKitRecordingState.Stopped, string.Empty);
            }
        }

        public void OnRecordingFailed(string message)
        {
#if !DONT_ALLOW_REPLAY_KIT_PAUSE_RESUME_AUDIO_LISTENER
            AudioListener.pause = m_audioListenerStatus;
#endif

            if (DidRecordingStateChangeEvent != null)
            {
                DidRecordingStateChangeEvent(ReplayKitRecordingState.Failed, message);
            }
        }

        public void OnRecordingAvailable()
        {
            if (DidRecordingStateChangeEvent != null)
            {
                DidRecordingStateChangeEvent(ReplayKitRecordingState.Available, string.Empty);
            }
        }


        public void OnPreviewOpened()
        {
            if (DidPreviewStateChangeEvent != null)
            {
                DidPreviewStateChangeEvent(ReplayKitPreviewState.Opened, string.Empty);
            }
        }

        public void OnPreviewClosed()
        {
            if (DidPreviewStateChangeEvent != null)
            {
                DidPreviewStateChangeEvent(ReplayKitPreviewState.Closed, string.Empty);
            }
        }

        public void OnPreviewPlayed()
        {
            if (DidPreviewStateChangeEvent != null)
            {
                DidPreviewStateChangeEvent(ReplayKitPreviewState.Played, string.Empty);
            }
        }

        public void OnPreviewShared()
        {
            if (DidPreviewStateChangeEvent != null)
            {
                DidPreviewStateChangeEvent(ReplayKitPreviewState.Shared, string.Empty);
            }
        }

        public void OnPreviewSaved(string error)
        {
            if (m_savePreviewCallback != null)
            {
                m_savePreviewCallback(string.IsNullOrEmpty(error) ? null : error);
            }
        }

        #endregion
    }
}