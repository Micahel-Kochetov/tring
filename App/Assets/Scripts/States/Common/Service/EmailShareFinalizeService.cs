using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class EmailShareFinalizeService
    {
        [Inject]
        private EmailSenderService emailSenderService;

        private Dictionary<int, EmailData> postponedSharing;

        public EmailShareFinalizeService()
        {
            postponedSharing = new Dictionary<int, EmailData>();
        }

        public void SetVideoUrls(int sessionId, string[] videoUrls)
        {
            if(!postponedSharing.ContainsKey(sessionId))
            {
                //meens video are uploaded before user pass sharing screen
                postponedSharing.Add(sessionId, new EmailData() { VideoUrls = videoUrls });
            }
            else
            {
                //meens user already passed sharing screen and we can manage email sending
                EmailData emailData = postponedSharing[sessionId];
                postponedSharing.Remove(sessionId);
                emailData.VideoUrls = videoUrls;
                SendEmail(emailData);
            }
        }

        public void SetUserData(int sessionId, bool isSendRequired, string email)
        {
            if (!postponedSharing.ContainsKey(sessionId))
            {
                //meens user passed sharing screen before video is uploaded
                postponedSharing.Add(sessionId, new EmailData() { IsSendRequired = isSendRequired, Email = email});
            }
            else
            {
                //meens video already uploaded and we can manage email sending
                EmailData emailData = postponedSharing[sessionId];
                postponedSharing.Remove(sessionId);
                emailData.Email = email;
                SendEmail(emailData);
            }
        }

        private void SendEmail(EmailData emailData)
        {
            if(emailData.IsSendRequired)
            {
                Debug.Log("video urls:");
                foreach (var item in emailData.VideoUrls)
                {
                    Debug.Log(item);
                }
                Debug.Log("sending email...");
                emailSenderService.SendEmail(emailData.Email, emailData.VideoUrls);
                Debug.Log("email send status:" + emailSenderService.SendStatus);
            }
        }

        private class EmailData
        {
            public bool IsSendRequired;
            public string Email;
            public string[] VideoUrls;
        }

    }
}
