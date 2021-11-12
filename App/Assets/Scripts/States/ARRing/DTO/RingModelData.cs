using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.DTO
{
    [Serializable]
    public class RingModelData
    {
        [SerializeField]
        string ringPrefabPath;
        [SerializeField]
        float price;
        [SerializeField]
        string name;
        [SerializeField]
        Sprite sprite;

        [SerializeField] private List<Material> metalMaterials;

        public List<Material> MetalMaterials => metalMaterials;

        public string RingPrefabPath
        {
            get
            {
                return ringPrefabPath;
            }
        }

        public float Price
        {
            get
            {
                return price;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Sprite Sprite { get => sprite; }
    }
}
