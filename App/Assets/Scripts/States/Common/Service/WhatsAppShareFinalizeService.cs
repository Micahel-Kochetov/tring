using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class WhatsAppShareFinalizeService
    {
        [Inject]
        private BulldogService bulldogService;

        private Dictionary<int, WhatsAppData> postponedSharing;

        public WhatsAppShareFinalizeService()
        {
            postponedSharing = new Dictionary<int, WhatsAppData>();
        }

        public void SetVideoUrls(int sessionId, string[] videoUrls)
        {
            if(!postponedSharing.ContainsKey(sessionId))
            {
                //meens video are uploaded before user pass sharing screen
                postponedSharing.Add(sessionId, new WhatsAppData() { VideoUrls = videoUrls });
            }
            else
            {
                //meens user already passed sharing screen and we can manage email sending
                WhatsAppData whatsAppData = postponedSharing[sessionId];
                postponedSharing.Remove(sessionId);
                whatsAppData.VideoUrls = videoUrls;
                SendMessage(whatsAppData);
            }
        }

        public void SetUserData(int sessionId, bool isSendRequired, string phone)
        {
            if (!postponedSharing.ContainsKey(sessionId))
            {
                //meens user passed sharing screen before video is uploaded
                postponedSharing.Add(sessionId, new WhatsAppData() { IsSendRequired = isSendRequired, Phone = phone });
            }
            else
            {
                //meens video already uploaded and we can manage email sending
                WhatsAppData whatsAppData = postponedSharing[sessionId];
                postponedSharing.Remove(sessionId);
                whatsAppData.IsSendRequired = isSendRequired;
                whatsAppData.Phone = phone;
                SendMessage(whatsAppData);
            }
        }

        private void SendMessage(WhatsAppData whatsAppData)
        {
            if(whatsAppData.IsSendRequired)
            {
                Debug.Log("video urls:");
                foreach (var item in whatsAppData.VideoUrls)
                {
                    Debug.Log(item);
                }
                Debug.Log("sending message...");
                bulldogService.SendMessage(whatsAppData.Phone, whatsAppData.VideoUrls, HandleSendMessageSuccess, HandleSendMessageError);
            }
        }

        private void HandleSendMessageSuccess()
        {
            Debug.Log("message sent");
        }

        private void HandleSendMessageError(string error)
        {
            Debug.Log($"message didn't sended. error: {error}");
        }
        private class WhatsAppData
        {
            public bool IsSendRequired;
            public string Phone;
            public string[] VideoUrls;
        }

    }
}
