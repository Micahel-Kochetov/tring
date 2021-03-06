﻿using UnityEngine;
using System;
using Assets.Scripts.States.ARRing.DTO;
using System.Collections.Generic;

namespace Assets.Scripts.States.ARRing.View
{
    [Serializable]
    public class ShowRingView
    {
        [SerializeField]
        Transform modelsParent;
        [SerializeField]
        RingModelData[] models;
        [SerializeField]
        GameObject arContent;
        Dictionary<int, GameObject> ringInstances;

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
            if (models.Length > index && index >= 0)
            {
                if (ringInstances.ContainsKey(index))
                {
                    if (ringInstances[index] == null)
                    {
                        ringInstances[index] = LoadModel(models[index], modelsParent);
                    }
                }
                else
                {
                    ringInstances[index] = LoadModel(models[index], modelsParent);
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
                return models.Length;
            }
        }

        public Sprite GetRingSprite(int index)
        {
            if (models.Length > index && index >= 0)
            {
                var sprite = models[index].Sprite;
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
            if (models.Length > index && index >= 0)
            {
                var price = models[index].Price;
                return price;
            }
            else
            {
                return 0;
            }
        }

        public string GetRingName(int index)
        {
            if (models.Length > index && index >= 0)
            {
                var name = models[index].Name;
                return name;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
