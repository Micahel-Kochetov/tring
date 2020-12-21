using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.Scripts.CoreStates;
using Common.UI;
using UnityEngine.Video;

namespace Assets.Scripts.States.ARRing.View
{
    [Serializable]
    public class VideoScreenView : ModalView
    {
        [SerializeField]
        ModalButton ReRecordButton;
        [SerializeField]
        ModalButton GetVideosButton;
        [SerializeField]
        ModalButton PlayButton;
        [SerializeField]
        ModalButton CloseButton;
        [SerializeField]
        VideoPlayer Player;
        [SerializeField]
        RawImage Background;       

        public Action OnReRecordButton;
        public Action OnGetVideosButton;
        public Action OnPlayButton;
        public Action OnCloseButton;

        protected override void AddButtons()
        {
            AddButton(ReRecordButton, OnReRecordButton);
            AddButton(ReRecordButton, OnGetVideosButton);
            AddButton(PlayButton, OnPlayButton);
        }

        public VideoPlayer GetVideoPlayer()
        {
            return Player;
        }

        public void SetBackgroundTexture(Texture texture)
        {
            Background.texture = texture;
        }

        public void CloseVideoScreenButton()
        {

        }
    }
}
