using UnityEngine;

namespace Assets.Scripts.States.Common
{
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "ScriptableObjects/ApplicationSettingsSO")]
    public class ApplicationSettingsSO : ScriptableObject
    {
        [SerializeField] private RingsSetConfigSO ringsSetConfigSO;

        public RingsSetConfigSO RingsSetConfigSO
        {
            get
            {
                return ringsSetConfigSO;
            }
        }

#if UNITY_EDITOR
        [SerializeField] private ScenesSetConfigSO scenesSetConfigSO;

        public ScenesSetConfigSO ScenesSetConfigSO
        {
            get
            {
                return scenesSetConfigSO;
            }
        }

        public void SetRingsSetConfigSO(RingsSetConfigSO ringsSetConfigSO)
        {
            this.ringsSetConfigSO = ringsSetConfigSO;
        }

        public void SetScenesSetConfigSO(ScenesSetConfigSO scenesSetConfigSO)
        {
            this.scenesSetConfigSO = scenesSetConfigSO;
        } 
#endif

    }
}
