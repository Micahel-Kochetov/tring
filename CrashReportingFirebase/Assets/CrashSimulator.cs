using Firebase.Crashlytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSimulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.LogLevel = Firebase.LogLevel.Debug;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SimulateCrash()
    {
        Debug.Log("Simulating crash");
    }

    public void SimulateOutOfMemory()
    {
        Debug.Log("Simulating out of memory");
        for (int i = 0; i < 1000; i++)
        {
            var tex = new Texture2D(100, 100, TextureFormat.ARGB32, false);
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    tex.SetPixel(x, y, Color.white);
                }
            }
            tex.Apply();
        }
    }

    public void FireNullRef()
    {
        Debug.Log("Simulating null ref");
        try
        {
            string str = GetString();
            str.ToCharArray();
        }
        catch (Exception ex)
        {
            Crashlytics.LogException(ex);
        }
    }

    public void ThrowException()
    {
        Debug.Log("Simulating new exception");
        var ex = new System.Exception("simulated exception");
        throw ex;
    }

    public void ThrowIndexOutOfRange()
    {
        Debug.Log("Simulating index out of range");
        try
        {
            int[] arr = new int[1];
            arr[2] = 0;
        }
        catch (Exception ex)
        {
            Crashlytics.LogException(ex);
        }
    }

    private string GetString()
    {
        return null;
    }
}
