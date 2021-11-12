using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States.Common
{
    [CreateAssetMenu(fileName = "Ring", menuName = "ScriptableObjects/RingSO")]
    public class RingSO : ScriptableObject
    {
        [SerializeField] private string ringPrefabPath;

        [SerializeField] private float price;

        [SerializeField] private string name;

        [SerializeField] private Sprite sprite;

        [SerializeField] private List<Material> metalMaterials;

        public List<Material> MetalMaterials => metalMaterials;

        public string RingPrefabPath => ringPrefabPath;

        public float Price => price;

        public string Name => name;

        public Sprite Sprite => sprite;
    }
}