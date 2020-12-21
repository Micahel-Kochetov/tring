using UnityEngine;
using System.Collections;
using VoxelBusters.ReplayKit.Common.Utility;
using VoxelBusters.ReplayKit.Common.UASUtils;
using System;

#if UNITY_EDITOR
using UnityEditor;

#endif

namespace VoxelBusters.ReplayKit.Internal
{
	public partial class ReplayKitSettings : SharedScriptableObject<ReplayKitSettings>, IAssetStoreProduct
	{
		/// <summary>
		/// Application Settings specific to Android platform.
		/// </summary>
		[System.Serializable]
		public class AndroidSettings : BasePlatformSettings
		{
            [SerializeField]
            [Tooltip("Set the max resolution at which you want to record. Setting higher resolution will have larger final video sizes.")]
            private VideoQuality m_videoMaxQuality = VideoQuality.QUALITY_720P;

            [SerializeField]
            [Tooltip("Enabling custom bitrates lets you set recommended bitrates compared to default values which give very big file sizes")]
            private CustomBitRateSetting m_customVideoBitrate;

            
            //[SerializeField]
            [Space(30)]
            [Header("Optional Settings")]
            [Tooltip("This captures app audio better when enabled")]
            private bool m_prioritiseAppAudioWhenUsingMicrophone = false;

            [SerializeField]
            [Tooltip("Enable this if you want to use SavePreview feature. This adds external storage permission to the manifest. Default is false")]
            private bool m_allowExternalStoragePermission = false;

            // Disabling as still its not perfect
            /*[SerializeField]
            [Space(30)]
            [Header("Advanced Settings (Useful for Vuforia to avoid ARCamera restart)")]
            [Tooltip("Enable the options if you want to show the screen recording permission and microphone permission at the time of initialisation. Else this permission will be displayed when StartRecording is called. This is useful for Vuforia as it will avoid restart of ARCamera. Default is false")]*/
            private InitialisationSettings m_initialisationSettings = new InitialisationSettings();

            public VideoQuality VideoMaxQuality
			{
				get 
				{
					return m_videoMaxQuality;
				}
			}

            public CustomBitRateSetting CustomBitrateSetting
            {
                get
                {
                    return m_customVideoBitrate;
                }
            }

            public bool PrioritiseAppAudioWhenUsingMicrophone
            {
                get
                {
                    return m_prioritiseAppAudioWhenUsingMicrophone;
                }
            }

            public InitialisationSettings ARRecordingSettings
            {
                get
                {
                    return m_initialisationSettings;
                }
            }

            public bool AllowExternalStoragePermission
            {
                get
                {
                    return m_allowExternalStoragePermission;
                }
            }
        }
	}
}