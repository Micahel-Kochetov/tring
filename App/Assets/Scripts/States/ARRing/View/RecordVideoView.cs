using Common.UI;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.View
{
    public class RecordVideoView : ModalView
    {
        [SerializeField]
        TextMeshProUGUI progressTime;
        float initialWidth;

        public override void Init()
        {
            base.Start();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void SetRecordProgress(float totalSeconds)
        {
            var seconds = Mathf.CeilToInt(totalSeconds);
            var time = TimeSpan.FromSeconds(seconds);
            progressTime.text = time.ToString(@"mm\:ss");
        }
    }
}
