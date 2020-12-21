using Assets.Scripts.States.Common.Model;
using Common.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.View
{
    public class PreshareView : ModalView
    {
        public event Action OnClose;
        public event Action OnSubmit;
        public event Action OnContinueRecording;
        [SerializeField]
        ModalButton closeBtn;
        [SerializeField]
        SelectableModalButton submitBtn;
        [SerializeField]
        List<VideoPlaceholderView> previewObjects;
        [SerializeField]
        RectTransform iconsParent;
        [SerializeField]
        VideoPreviewIcon iconPrefab;
        [SerializeField]
        SelectableModalButton continueRecordingBtn;
        protected Dictionary<VideoRecordModel, VideoPreviewIcon> modelViewMapping;
        readonly int maxVideoRecordsNumber = 5;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(closeBtn, OnCloseHandler);
            AddButton(submitBtn, OnSubmitHandler);
            AddButton(continueRecordingBtn, OnContinueRecordingHandler);
        }

        public override void Init()
        {
            base.Init();
            modelViewMapping = new Dictionary<VideoRecordModel, VideoPreviewIcon>();
            foreach (var item in previewObjects)
            {
                item.Init();
                item.OnAdd += OnAddVideoHandler;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in previewObjects)
            {
                item.Dispose();
                item.OnAdd -= OnAddVideoHandler;
            }
        }


        public void Show(List<VideoRecordModel> videoModels)
        {
            base.Show();
            submitBtn.SetDeselected();
            continueRecordingBtn.UpdateUIState(videoModels.Count < maxVideoRecordsNumber);
            for (int i = 0; i < videoModels.Count; i++)
            {
                if (i < previewObjects.Count)
                {
                    var viewInstance = UnityEngine.Object.Instantiate(iconPrefab);
                    viewInstance.transform.SetParent(iconsParent);
                    viewInstance.transform.localScale = Vector3.one;
                    viewInstance.transform.localPosition = previewObjects[i].RectTransform.localPosition;
                    var image = Scripts.Common.Utility.LoadTexture(videoModels[i].ThumbnailPath);
                    var sprite = Scripts.Common.Utility.ConvertTex2DToSprite(image);
                    viewInstance.Init(image, videoModels[i]);
                    viewInstance.Show();
                    viewInstance.OnSelectionChange += OnIconClickHandler;
                    viewInstance.OnSelectionChange += SelectionCounter;
                    viewInstance.OnVideoStarted += ViewInstance_OnVideoStarted;
                    modelViewMapping.Add(videoModels[i], viewInstance);
                }
            }
            for (int i = 0; i < previewObjects.Count; i++)
            {
                var isVisible = i >= videoModels.Count;
                var obj = previewObjects[i];
                if (isVisible)
                {
                    obj.Show();
                }
                else
                {
                    obj.Hide();
                }
            }
        }

        private void OnAddVideoHandler()
        {
            OnContinueRecording?.Invoke();
        }

        private void ViewInstance_OnVideoStarted(VideoPreviewIcon obj)
        {
            foreach (var item in modelViewMapping.Values)
            {
                if (item != obj)
                {
                    item.IsPlaying = false;
                }
            }
        }

        private void OnContinueRecordingHandler()
        {
            if (continueRecordingBtn.IsSelected)
            {
                OnContinueRecording?.Invoke();
            }
        }

        private void SelectionCounter(VideoPreviewIcon arg1, bool arg2)
        {
            int counter = 0;
            foreach (var item in modelViewMapping.Values)
            {
                if (item.IsSelected)
                {
                    counter++;
                }
            }
            submitBtn.UpdateUIState(counter > 0);
        }

        public override void Hide()
        {
            base.Hide();
            foreach (var item in modelViewMapping)
            {
                item.Value.OnSelectionChange -= OnIconClickHandler;
                item.Value.OnSelectionChange -= SelectionCounter;
                item.Value.OnVideoStarted -= ViewInstance_OnVideoStarted;
                Destroy(item.Value.gameObject);
            }
            modelViewMapping.Clear();
            foreach (var item in previewObjects)
            {
                item.Hide();
            }
            Resources.UnloadUnusedAssets();
        }

        public string[] GetSelectedVidePathes()
        {
            var selectedItems = from item in modelViewMapping where item.Value.IsSelected select item.Key.FilePath;
            var result = selectedItems.ToArray();
            return result;
        }

        void OnIconClickHandler(VideoPreviewIcon senderView, bool state)
        {
            senderView.UpdateUIState(state);
        }

        private void OnCloseHandler()
        {
            OnClose?.Invoke();
        }

        private void OnSubmitHandler()
        {
            if (submitBtn.IsSelected)
            {
                OnSubmit.Invoke();
            }
        }
    }
}
