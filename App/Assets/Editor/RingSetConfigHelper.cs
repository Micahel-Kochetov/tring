using Assets.Scripts.States.Common;
using UnityEditor;
using UnityEngine;

public class RingSetConfigHelper
{

    [MenuItem("Tools/RingsSetConfig/10 Rings")]
    public static void Configurate10RingSet()
    {
        ConfigurateRingSet("Assets/ScriptableObjects/10RingsSetConfig.asset");
    }

    [MenuItem("Tools/RingsSetConfig/28 Rings")]
    public static void Configurate28RingSet()
    {
        ConfigurateRingSet("Assets/ScriptableObjects/28RingsSetConfig.asset");
    }

    private static void ConfigurateRingSet(string ringsSetConfigPath)
    {
        RingsSetConfigSO ringsSetConfigSO = (RingsSetConfigSO)AssetDatabase.LoadAssetAtPath(ringsSetConfigPath, typeof(RingsSetConfigSO));
        ApplicationSettingsSO applicationSettingsSO = (ApplicationSettingsSO)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/ApplicationSettings.asset", typeof(ApplicationSettingsSO));
        applicationSettingsSO.SetRingsSetConfigSO(ringsSetConfigSO);
        EditorUtility.SetDirty(applicationSettingsSO);
        AssetDatabase.SaveAssets();
        Debug.Log("RingsSetConfig is updated");
    }
}
