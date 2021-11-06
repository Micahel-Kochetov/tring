using Assets.Scripts.States.ARRing.DTO;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States.Common
{
    [CreateAssetMenu(fileName = "RingsSetConfig", menuName = "ScriptableObjects/RingsSetConfigSO")]
    public class RingsSetConfigSO : ScriptableObject
    {
        [SerializeField] private List<RingSO> ringModelDatas;

        public List<RingSO> RingModelDatas
        {
            get
            {
                return ringModelDatas;
            }
        }
    }
}
