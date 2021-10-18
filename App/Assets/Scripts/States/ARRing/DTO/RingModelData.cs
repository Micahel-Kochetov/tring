using System;
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
