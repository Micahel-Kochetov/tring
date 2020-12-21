using Assets.Scripts.CoreStates;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.States.StartTheMagic;
using Assets.Scripts.States.ARRing;
using Assets.Scripts.States.GetYourVideos;
using System.Linq;

namespace Assets.Scripts.States.Common.Service
{
    public class ScenePathProvider
    {
        readonly float aspect;
        Dictionary<Type, ScenePathMappingData> uiSceneMapping;

        public ScenePathProvider()
        {
            aspect = (float)Screen.width / (float)Screen.height;
            uiSceneMapping = new Dictionary<Type, ScenePathMappingData>();
            uiSceneMapping.Add(typeof(StartTheMagicState), new ScenePathMappingData(new Dictionary<float, string>()
            {
                       { 0.462f, "StartTheMagicMobile" },
                       { 0.698f, "StartTheMagicTablet" }
            }));
            uiSceneMapping.Add(typeof(ARRingState), new ScenePathMappingData(new Dictionary<float, string>()
            {
                       { 0.462f, "ARRingMobile" },
                       { 0.698f, "ARRingTablet_1668_2388" },
                       { 0.749f, "ARRingTablet_2048_2732" }
            }
            ));
            uiSceneMapping.Add(typeof(GetYourVideosState), new ScenePathMappingData(new Dictionary<float, string>()
            {
                       { 0.462f, "GetVideosMobile" },
                       { 0.698f, "GetVideosTablet_1668_2388" },
                       { 0.749f, "GetVideosTablet_2048_2732" }
            }
            ));
        }

        public bool TryGetUIScenePath(Type type, out string newSceneName)
        {
            ScenePathMappingData data;
            newSceneName = string.Empty;
            if (uiSceneMapping.TryGetValue(type, out data))
            {
                if (data != null && data.Mapping != null && data.Mapping.Count > 0)
                {
                    newSceneName = data.Mapping.OrderBy(item => Mathf.Abs(item.Key - aspect)).First().Value;
                    return true;
                }
            }
            return false;
        }
    }
}
