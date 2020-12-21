using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
namespace VoxelBusters.ReplayKit.Internal
{
	public partial class ReplayKitAndroid : MonoBehaviour, INativeService
	{

		private INativeCallbackListener 		m_listener;

#region Constructors

		public ReplayKitAndroid()
		{
			AndroidJavaClass _class = new AndroidJavaClass(Native.Class.NAME);
			Plugin = _class.CallStatic<AndroidJavaObject>("getInstance");
		}

#endregion

#region INativeService implementation

        public void Initialise(INativeCallbackListener listener)
		{
			m_listener = listener;

            if (IsRecordingAPIAvailable())
            {
                InitialisationSettings settings = ReplayKitSettings.Instance.Android.ARRecordingSettings;
                Plugin.Call(Native.Methods.INITIALISE, settings.RequestScreenRecordPermissionOnInitialise, settings.RequestMicrophonePermissionOnInitialise, ReplayKitSettings.Instance.Android.PrioritiseAppAudioWhenUsingMicrophone);
            }
            else
            {
                m_listener.OnInitialiseFailed("Replay Kit API not available");
            }
		}

        public bool IsRecordingAPIAvailable()
        {
            return Plugin.Call<bool>(Native.Methods.IS_RECORDING_API_AVAILABLE);
        }

        public bool IsRecording()
        {
            return Plugin.Call<bool>(Native.Methods.IS_RECORDING); ;
        }

        public bool IsPreviewAvailable()
        {
            return Plugin.Call<bool>(Native.Methods.IS_PREVIEW_AVAILABLE); ;
        }

        public bool IsCameraEnabled()
        {
            return Plugin.Call<bool>(Native.Methods.IS_CAMERA_ENABLED);
        }

        public void StartRecording(bool enableMicrophone)
        {
            ReplayKitSettings.AndroidSettings androidSettings = ReplayKitSettings.Instance.Android;
            Plugin.Call(Native.Methods.START_RECORDING, enableMicrophone, (int)androidSettings.VideoMaxQuality,  androidSettings.CustomBitrateSetting.AllowCustomBitrates ? androidSettings.CustomBitrateSetting.BitrateFactor : -1f, false);
        }

        public void StopRecording()
        {
            Plugin.Call(Native.Methods.STOP_RECORDING);
        }

        public bool Preview()
        {
            return Plugin.Call<bool>(Native.Methods.PREVIEW_RECORDING);
        }

        public bool Discard()
        {
            return Plugin.Call<bool>(Native.Methods.DISCARD_RECORDING);
        }

        public string GetPreviewFilePath()
        {
            return Plugin.Call<string>(Native.Methods.PREVIEW_FILE_PATH);
        }

        public void SavePreview(string filename = null)
        {
            if(!ReplayKitSettings.Instance.Android.AllowExternalStoragePermission)
            {
                string message = "Please enable AllowExternalStoragePermission in ReplayKit Settings and click on save to use this feature!";
                Debug.LogError("[ReplayKit] " + message);
                Plugin.Call(Native.Methods.SHOW_MESSAGE, message);
            }
            Plugin.Call(Native.Methods.SAVE_PREVIEW, filename);
        }

        public void SharePreview(string text = null, string subject = null)
        {
            Plugin.Call(Native.Methods.SHARE_PREVIEW, text, subject);
        }

#endregion
    }
}
#endif
