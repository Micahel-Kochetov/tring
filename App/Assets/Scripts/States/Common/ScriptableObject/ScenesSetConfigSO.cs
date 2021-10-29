using Assets.Scripts.States.ARRing.DTO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.States.Common
{
    [CreateAssetMenu(fileName = "ScenesSetConfig", menuName = "ScriptableObjects/ScenesSetConfigSO")]
    public class ScenesSetConfigSO : ScriptableObject
    {
        [SerializeField] private List<SceneAsset> sceneList;

        public List<SceneAsset> SceneList
        {
            get
            {
                return sceneList;
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ScenesSetConfigSO))]
        public class ScenesSetConfigSOEditor : Editor
        {
            ScenesSetConfigSO scenesSetConfigSO;

            private void OnEnable()
            {
                scenesSetConfigSO = target as ScenesSetConfigSO;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Activate this set"))
                {
                    SetEditorBuildSettingsScenes();
                    SetApplicationSettingsSO();
                    Debug.Log("ScenesSetConfigSO is updated");
                }
            }

            private void SetApplicationSettingsSO()
            {
                ApplicationSettingsSO applicationSettingsSO = (ApplicationSettingsSO)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/ApplicationSettings.asset", typeof(ApplicationSettingsSO));
                applicationSettingsSO.SetScenesSetConfigSO(scenesSetConfigSO);
                EditorUtility.SetDirty(applicationSettingsSO);
                AssetDatabase.SaveAssets();
            }

            private void SetEditorBuildSettingsScenes()
            {
                // Find valid Scene paths and make a list of EditorBuildSettingsScene
                List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
                foreach (SceneAsset sceneAsset in scenesSetConfigSO.SceneList)
                {
                    string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                    if (!string.IsNullOrEmpty(scenePath))
                        editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                }

                // Set the Build Settings window Scene list
                EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            }
        }
#endif
    }
}
