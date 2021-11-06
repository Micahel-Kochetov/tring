using System;
using System.Collections.Generic;
using Assets.Scripts.States.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.States.ARRing.View
{
    [Serializable]
    public class ShowRingView
    {
        [SerializeField] private Transform modelsParent;

        [SerializeField] private ApplicationSettingsSO applicationSettingsSO;

        [SerializeField] private GameObject arContent;

        private int cachedRingIndex;
        private Dictionary<int, ModelInfo> ringInstances;

        private string[] trimStrings =
        {
            "(Instance)", "(Clone)"
        };

        private List<RingSO> Models => applicationSettingsSO.RingsSetConfigSO.RingModelDatas;

        public int RingsCount => Models.Count;

        public void Init()
        {
            ringInstances = new Dictionary<int, ModelInfo>();
            cachedRingIndex = -1;
        }

        public void Dispose()
        {
            UnloadAllModels();
            cachedRingIndex = -1;
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
                    if (ringInstances[index] == null || ringInstances[index].Model == null)
                    {
                        var model = LoadModel(Models[index], modelsParent);
                        ringInstances[index] = new ModelInfo(model);
                    }
                }
                else
                {
                    var model = LoadModel(Models[index], modelsParent);
                    ringInstances.Add(index, new ModelInfo(model));
                }

                ringInstances[index].Model.SetActive(true);
                cachedRingIndex = index;
            }
        }

        public void SetNextMaterial()
        {
            ChangeMetalMaterial(true);
        }

        public void SetPreviousMaterial()
        {
            ChangeMetalMaterial(false);
        }

        private void ChangeMetalMaterial(bool setNextMaterial)
        {
            if (ringInstances.ContainsKey(cachedRingIndex))
            {
                var modelInfo = ringInstances[cachedRingIndex];
                var metalMaterials = Models[cachedRingIndex].MetalMaterials;
                if (metalMaterials.Count == 0)
                {
                    Debug.LogError($"There are no materials for ring with index {cachedRingIndex}");
                    return;
                }

                var meshRenderers = modelInfo.Model.GetComponentsInChildren<MeshRenderer>();
                foreach (var meshRenderer in meshRenderers)
                    if (TryGetNewMaterial(meshRenderer.material, metalMaterials,
                        setNextMaterial, out var newMat))
                        meshRenderer.material = newMat;
            }
            else
            {
                Debug.Log($"ringInstances does not contain key {cachedRingIndex}");
            }
        }

        private bool TryGetNewMaterial(Material mat, List<Material> metalMaterials, bool setNextMaterial,
            out Material newMat)
        {
            newMat = null;
            var searchName = mat.name.Clone() as string;
            foreach (var trimString in trimStrings)
                while (searchName.IndexOf(trimString) >= 0)
                {
                    var i = searchName.IndexOf(trimString);
                    searchName = searchName.Remove(i, trimString.Length);
                }

            searchName = searchName.Trim();
            var match = metalMaterials.Find(item => item.name.Equals(searchName));

            if (match != null)
            {
                var index = metalMaterials.IndexOf(match);
                if (index >= 0)
                {
                    if (setNextMaterial)
                        index++;
                    else
                        index--;

                    if (index < 0) index = metalMaterials.Count - 1;

                    if (index >= metalMaterials.Count) index = 0;
                    newMat = Object.Instantiate(metalMaterials[index]);
                    return true;
                }
            }

            return false;
        }

        private GameObject LoadModel(RingSO ringModelData, Transform parent)
        {
            var loadedAsset = Resources.Load<GameObject>(ringModelData.RingPrefabPath);
            var instance = Object.Instantiate(loadedAsset);
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

        private void ShowARContent(bool isVisible)
        {
            arContent.SetActive(isVisible);
        }

        private void HideModels()
        {
            foreach (var item in ringInstances)
                if (item.Value != null && item.Value.Model != null)
                    item.Value.Model.SetActive(false);
        }

        private void UnloadAllModels()
        {
            foreach (var item in ringInstances)
                if (item.Value != null)
                    Object.Destroy(item.Value.Model);
            ringInstances.Clear();
            Resources.UnloadUnusedAssets();
        }

        private void UnloadModels(int exceptIndex)
        {
            foreach (var item in ringInstances)
                if (item.Key != exceptIndex)
                    Object.Destroy(item.Value.Model);
            Resources.UnloadUnusedAssets();
        }

        public Sprite GetRingSprite(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var sprite = Models[index].Sprite;
                return sprite;
            }

            Debug.Log("Invalid ring index:" + index);
            return null;
        }

        public float GetRingPrice(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var price = Models[index].Price;
                return price;
            }

            return 0;
        }

        public string GetRingName(int index)
        {
            if (Models.Count > index && index >= 0)
            {
                var name = Models[index].Name;
                return name;
            }

            return string.Empty;
        }

        private class ModelInfo
        {
            public readonly GameObject Model;

            public ModelInfo(GameObject model)
            {
                Model = model;
            }
        }
    }
}