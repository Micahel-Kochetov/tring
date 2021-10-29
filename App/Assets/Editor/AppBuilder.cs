using Assets.Scripts.Common.AppVersion;
using Assets.Scripts.States.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AppBuilder
{

    [MenuItem("Tools/Build iOS")]
    public static void BuildUserApp()
    {
        ApplicationSettingsSO applicationSettingsSO = (ApplicationSettingsSO)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/ApplicationSettings.asset", typeof(ApplicationSettingsSO));

        List<string> levels = new List<string>(applicationSettingsSO.ScenesSetConfigSO.SceneList.Count);

        foreach(var scene in applicationSettingsSO.ScenesSetConfigSO.SceneList)
        {
            string scenePath = AssetDatabase.GetAssetPath(scene);
            UnityEngine.Debug.Log(scenePath);
            levels.Add(scenePath);
        }
       
        BuildApp(levels.ToArray(), BuildTarget.iOS);
    }

    private static void BuildApp(string[] scenes, BuildTarget buildTarget)
    {
        if (CheckIfScenesExists(scenes) == false)
            return;
        AppVersionService versionService = new AppVersionService();
        ProcessStartInfo startInfo = new ProcessStartInfo("git");
        startInfo.UseShellExecute = false;
        var gitFolderPath = Path.Combine(Path.GetFullPath(Application.dataPath), "..", "..");
        UnityEngine.Debug.Log($"Git folder path:{gitFolderPath}");
        startInfo.WorkingDirectory = gitFolderPath;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.Arguments = "rev-parse --abbrev-ref HEAD";
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        string branchname = process.StandardOutput.ReadLine();
        UnityEngine.Debug.Log($"branch name {branchname}");
        versionService.IncrementVersion(branchname);

        var buildPath = Path.Combine(Path.GetFullPath(Application.dataPath), "..", "..", "Builds");// EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        var appVersionFolderName = versionService.GetAppFullVersionSync();
        PlayerSettings.bundleVersion = versionService.GetAppVersion();
        if (buildTarget == BuildTarget.iOS)
        {
            PlayerSettings.iOS.buildNumber = versionService.GetBuildNumber().ToString();
        }
        appVersionFolderName = appVersionFolderName.Replace("\\", "_");
        appVersionFolderName = appVersionFolderName.Replace("/", "_");
        buildPath = Path.Combine(buildPath, appVersionFolderName);
        UnityEngine.Debug.Log($"Build path:{buildPath}");
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.locationPathName = buildPath;
        options.target = buildTarget;
        // Build player.
        BuildPipeline.BuildPlayer(options);
    }


    private static bool CheckIfScenesExists(string[] scenes)
    {
        foreach (var scene in scenes)
            if (File.Exists(scene) == false)
            {
                UnityEngine.Debug.LogError($"scene {scene} doesn't exist");
                return false;
            }
        return true;
    }
}
