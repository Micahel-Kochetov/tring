using UnityEngine;
using System;
using Assets.Scripts.States.ARRing.DTO;
using System.Collections.Generic;
using Assets.Scripts.States.Common;

namespace Assets.Scripts.States.ARRing.View
{
    [Serializable]
    public class ShowRingView
    {
        [SerializeField]
        Transform modelsParent;
        [SerializeField]
        ApplicationSettingsSO applicationSettingsSO;
        [SerializeField]
        GameObject arContent;
        Dictionary<int, GameObject> ringInstances;

        private List<RingModelData> Models
        {
            get
            {
                return applicationSettingsSO.RingsSetConfigSO.RingModelDatas;
            }
        }

        public void Init()
        {
            ringInstances = new Dictionary<int, GameObject>();
        }

        public void Dispose() {
            UnloadAllModels();
        }

        public void ShowModel(int index)
        {
            HideModels();
            UnloadModels(index);
            ShowARContent(true);
            index = index % RingsCount;
            if (Models.Count > index && index >= 0)
            {
                if (ringInstances.ContainsKey(index))
                {
                    if (ringInstances[index] == null)
                    {
                        ringInstances[index] = LoadModel(Models[index], modelsParent);
                    }
                }
                else
                {
                    ringInstances[index] = LoadModel(Models[index], modelsParent);
                }
                ringInstances[index].SetActive(true);
            }
        }

        private GameObject LoadModel(RingModelData ringModelData, Transform parent)
        {
            var loadedAsset = Resources.Load<GameObject>(ringModelData.RingPrefabPath);
            var instance = UnityEngine.Object.Instantiate(loadedAsset);
            instance.transform.SetParent(parent);
            instance.transform.rotation = Quaternion.identity;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            return instance;
        }

        public void Hide()
        {
            HideModels();
            ShowARContent(false);
        }

        void ShowARContent(bool isVisible)
        {
            arContent.SetActive(isVisible);
        }

        void HideModels()
        {
            foreach (var item in ringInstances)
            {
                if (item.Value != null)
                {
                    item.Value.SetActive(false);
                }
            }
        }

        void UnloadAllModels()
        {
            foreach (var item in ringInstances)
            {
                if (item.Value != null)
                {
                    UnityEngine.Object.Destroy(item.Value);
                }
            }
            ringInstances.Clear();
            Resources.UnloadUnusedAssets();
        }

        void UnloadModels(int exceptIndex)
        {
            foreach (var item in ringInstances)
            {
                if (item.Key != exceptIndex)
                {
                    UnityEngine.Object.Destroy(item.Value);
                }
            }
            Resources.UnloadUnusedAssets();
        }

        public int RingsCount
        {
            get
            {
                return Models.Count;
            }
        }

        public Sprite GetRingSprite(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var sprite = Models[index].Sprite;
                return sprite;
            }
            else
            {
                Debug.Log("Invalid ring index:" + index);
                return null;
            }
        }

        public float GetRingPrice(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var price = Models[index].Price;
                return price;
            }
            else
            {
                return 0;
            }
        }

        public string GetRingName(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var name = Models[index].Name;
                return name;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
