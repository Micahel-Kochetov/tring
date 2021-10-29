using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
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

        public void SendMessage(string userPhone, string userName, string[] videoLinks, Action success, Action<string> error)
        {
            coroutineStarterService.StartCoroutine(AsyncSendMessage(userPhone, userName, videoLinks, success, error));
        }

        #region SendMessage
        private IEnumerator AsyncSendMessage(string userPhone, string userName, string[] videoLinks, Action success, Action<string> error)
        {
            Debug.Log("Messages sending");

            string url = Constants.Server + Constants.SendMessage;
            string message = Constants.MessageTemplate;
            message = message.Replace(Constants.MessageUserNameKey, userName);

            for (int i = 0; i < videoLinks.Length; i++)
            {
                message += "\n" + videoLinks[i];
            }

            Debug.Log("Send main message");

            SendMessageRequest mainMessage = new SendMessageRequest
            {
                Phone = userPhone,
                Message = message
            };

            bool isError = false;

            yield return SendRequest(url,
                mainMessage,
                (SendMessageResponce response) => Debug.Log("Main message sent"),
                (errorMessage) => isError = true);

            if (isError)
            {
                yield break;
            }

            success?.Invoke();
        }

        private IEnumerator UploadVideos(string[] videoLinks, Action<string[]> success, Action<string> error)
        {
            Debug.Log("Videos are uploading");

            string url = Constants.Server + Constants.Files;
            string[] videoIds = new string[videoLinks.Length];

            for (int i = 0; i < videoLinks.Length; i++)
            {
                using (UnityWebRequest uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
                {
                    uwr.SetRequestHeader("Content-Type", "video/mp4");
                    uwr.SetRequestHeader("Token", Constants.Token);
                    Debug.Log("Upload video: " + videoLinks[i]);
                    byte[] videoBytes = File.ReadAllBytes(videoLinks[i]);
                    uwr.uploadHandler = new UploadHandlerRaw(videoBytes);
                    uwr.uploadHandler.contentType = "video/mp4";
                    uwr.downloadHandler = new DownloadHandlerBuffer();

                    yield return uwr.SendWebRequest();

                    if (uwr.result == UnityWebRequest.Result.Success)
                    {
                        try
                        {
                            SendFileListResponce responseList = JsonConvert.DeserializeObject<SendFileListResponce>("{\"list\":" + uwr.downloadHandler.text + "}");
                            Debug.Log("Video id: " + responseList.Items[0].ID);
                            videoIds[i] = responseList.Items[0].ID;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex.Message);
                            error?.Invoke(ex.Message);

                            yield break;
                        }
                    }
                    else
                    {
                        Debug.LogError(uwr.error);
                        error?.Invoke(uwr.error);

                        yield break;
                    }
                }
            }

            success?.Invoke(videoIds);
        }

        public class SendMessageRequest
        {
            [JsonProperty("phone")]
            public string Phone;

            [JsonProperty("message")]
            public string Message;

            [JsonProperty("media")]
            public Media MediaValue;

            public class Media
            {
                [JsonProperty("file")]
                public string File;
            }
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

        public class SendFileListResponce
        {
            [JsonProperty("list")]
            public SendFileResponce[] Items;
        }

        public class SendFileResponce
        {
            [JsonProperty("id")]
            public string ID;
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
                catch (Exception ex)
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
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string requestJson = JsonConvert.SerializeObject(request, jsonSerializerSettings);

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
            public const string Files = "/files";

            //Other
            public const string Token = "fe7b9ac7f74e3a47de528aa27b0092d8e5ed3f03984ce4bb382b57d21f08f323a5049ef13669ffc4";
            public const string MessageTemplate = "Hello %user_name%, here are your AR Experience!\nPlease share it with your friends and make them envy;)";
            public const string MessageUserNameKey = "%user_name%";
            public const string MessageLink1Key = "%link_1%";
            public const string MessageLink2Key = "%link_2%";
            public const string MessageLink3Key = "%link_3%";
        }
        #endregion

    }
}
