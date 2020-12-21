using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.States.Common.Service;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(TestEmail))]
public class TestEmailEdior:Editor
{
    TestEmail testEmail;

    void OnEnable()
    {
        testEmail = (TestEmail)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20);
        if (GUILayout.Button("Send"))
        {
            testEmail.SendEmailAsync();
        }
    }
}
#endif

public class TestEmail : MonoBehaviour
{
    public string userEmail;
    public string userName;
    public string[] videoLinks;
    
    public void SendEmailAsync()
    {
        StartCoroutine(SendEmail());
    }

    public IEnumerator SendEmail()
    {
        Debug.Log("Sending...");
        var service = new EmailSenderService();
        yield return service.AsyncSendEmail(userEmail, userName, videoLinks);
        Debug.Log("Send "+ service.SendStatus);
    }
}

