#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoadAttribute]
public static class DefaultSceneLoader
{
    private static string mainScenePath = "Assets/Scenes/Main.unity";
    private static string mainScene = "Main";
    static string loadFromMain = "LoadFromMain";

    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        var loadFromMainFlag = PlayerPrefs.GetInt(loadFromMain) == 1;
        if (loadFromMainFlag && state == PlayModeStateChange.EnteredPlayMode)
        {
            var currentScene = EditorSceneManager.GetActiveScene();
            if (currentScene.name != mainScene)
            {
                EditorSceneManager.LoadScene(mainScenePath);
            }
        }
    }

    [MenuItem("Tools/Load From Main Scene/Enable")]
    public static void EnableBuildFromMainScene()
    {
        PlayerPrefs.SetInt(loadFromMain, 1);
    }

    [MenuItem("Tools/Load From Main Scene/Disable")]
    public static void DisableBuildFromMainScene()
    {
        PlayerPrefs.SetInt(loadFromMain, 0);
    }
}
#endif
