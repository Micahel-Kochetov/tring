using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class BulldogService
    {
        [Inject]
        private CoroutineHandlerService coroutineStarterService;

        public void CheckPhone(string userPhone, Action<CheckPhoneResponce> success, Action<string> error)
        {
            coroutineStarterService.StartCoroutine(AsyncCheckPhone(userPhone, success, error));
        }

        #region CheckPhone
        private IEnumerator AsyncCheckPhone(string userPhone, Action<CheckPhoneResponce> success, Action<string> error)
        {
            string url = Constants.Server + Constants.CheckPhone;

            CheckPhoneRequest checkPhoneRequest = new CheckPhoneRequest
            {
                Phone = userPhone
            };

            yield return SendRequest(url, checkPhoneRequest, success, error);
        }

        public class CheckPhoneRequest
        {
            [JsonProperty("phone")]
            public string Phone;
        }

        public class CheckPhoneResponce
        {
            [JsonProperty("exists")]
            public bool Exists;

            [JsonProperty("phone")]
            public string Phone;

            [JsonProperty("wid")]
            public string WID;

            [JsonProperty("countryPrefix")]
            public string CountryPrefix;

            [JsonProperty("isBusiness")]
            public bool IsBusiness;
        }
        #endregion

        public void SendMessage(string userPhone, string userName, string[] videoLinks, Action<SendMessageResponce> success, Action<string> error)
        {
            coroutineStarterService.StartCoroutine(AsyncSendMessage(userPhone, userName, videoLinks, success, error));
        }

        #region SendMessage
        private IEnumerator AsyncSendMessage(string userPhone, string userName, string[] videoLinks, Action<SendMessageResponce> success, Action<string> error)
        {
            string url = Constants.Server + Constants.SendMessage;
            string message = CreateMessage(userName, videoLinks);

            SendMessageRequest checkPhoneRequest = new SendMessageRequest
            {
                Phone = userPhone,
                Message = message
            };

            yield return SendRequest(url, checkPhoneRequest, success, error);
        }

        private string CreateMessage(string userName, string[] videoLinks)
        {
            string message = Constants.MessageTemplate;

            videoLinks = new string[]
            {
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4"
            };

            try
            {
                message = message.Replace(Constants.MessageUserNameKey, userName);
                message = message.Replace(Constants.MessageLink1Key, videoLinks[0]);
                message = message.Replace(Constants.MessageLink2Key, videoLinks[1]);
                message = message.Replace(Constants.MessageLink3Key, videoLinks[2]);
            }
            catch(Exception ex)
            {
                Debug.LogError("Error on creating message" + ex.Message);
            }

            return message;
        }

        public class SendMessageRequest
        {
            [JsonProperty("phone")]
            public string Phone;

            [JsonProperty("message")]
            public string Message;
        }

        public class SendMessageResponce
        {
            [JsonProperty("id")]
            public string ID;

            [JsonProperty("waId")]
            public string WAID;

            [JsonProperty("phone")]
            public string Phone;

            [JsonProperty("wid")]
            public string WID;

            [JsonProperty("status")]
            public string Status;

            [JsonProperty("deliveryStatus")]
            public string DeliveryStatus;

            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("deliverAt")]
            public DateTime DeliverAt;

            [JsonProperty("message")]
            public string Message;

            [JsonProperty("priority")]
            public string Priority;

            [JsonProperty("retentionPolicy")]
            public string RetentionPolicy;

            //parameter not implemented
            //[JsonProperty("retry")]
            //public string Retry;

            [JsonProperty("webhookStatus")]
            public string WebhookStatus;

            [JsonProperty("device")]
            public string Device;
        }
        #endregion

        #region Common
        private IEnumerator SendRequest<T>(string url, object request, Action<T> success, Action<string> error)
        {
            UnityWebRequest uwr = CreateUnityWebRequest(url, request);

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    T response = JsonConvert.DeserializeObject<T>(uwr.downloadHandler.text);

                    success?.Invoke(response);
                }
                catch(Exception ex)
                {
                    error?.Invoke(ex.Message);
                }
                
            }
            else
            {
                error?.Invoke(uwr.error);
            }

        }

        private UnityWebRequest CreateUnityWebRequest(string url, object request)
        {
            string requestJson = JsonConvert.SerializeObject(request);

            UnityWebRequest uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.SetRequestHeader("Token", Constants.Token);
            uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestJson));
            uwr.uploadHandler.contentType = "application/json";
            uwr.downloadHandler = new DownloadHandlerBuffer();

            return uwr;
        }

        private static class Constants
        {
            //Url
            public const string Server = "https://api.bulldog-wp.co.il/v1";
            public const string CheckPhone = "/numbers/exists";
            public const string SendMessage = "/messages";

            //Other
            public const string Token = "fe7b9ac7f74e3a47de528aa27b0092d8e5ed3f03984ce4bb382b57d21f08f323a5049ef13669ffc4";
            public const string MessageTemplate = "Hello %user_name%, here are the links to your AR Experience!\nPlease share it with your friends and make them envy;)\n%link_1%\n%link_2%\n%link_3%";
            public const string MessageUserNameKey = "%user_name%";
            public const string MessageLink1Key = "%link_1%";
            public const string MessageLink2Key = "%link_2%";
            public const string MessageLink3Key = "%link_3%";
        }
        #endregion

    }
}
