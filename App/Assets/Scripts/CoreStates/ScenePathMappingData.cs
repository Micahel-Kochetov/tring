using System.Collections.Generic;

namespace Assets.Scripts.CoreStates
{
    public class ScenePathMappingData
    {
        public Dictionary<float, string> Mapping;

        public ScenePathMappingData(Dictionary<float, string> mapping)
        {
            Mapping = mapping;
        }
    }
}
