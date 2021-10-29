using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class VuforiaBehaviourRemover : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += FixCamera;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FixCamera;
    }

    void FixCamera(Scene scene, LoadSceneMode mode)
    {
        var _vuforiaBehaviour = GetComponent<VuforiaBehaviour>();

        if (_vuforiaBehaviour != null)
        {
            Destroy(_vuforiaBehaviour);
        }
    }
}
