using Assets.Scripts.States.Common.Interface;
using Assets.Scripts.States.Common.Model;
using Firebase.Analytics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class AnalyticsService : IAnalyticsService, ITickable
    {
        string SessionLogEvent = "SessionLog";
        string LikeEvent = "Like";
        string ScreenEvent = "Screen";
        string dateFormat = "MM/dd/yyyy HH:mm:ss";
        AnalyticsSessionDataModel currentEvent;
        List<AnalyticsSessionDataModel> events;

        public int CurrentEventID
        {
            get
            {
                if (currentEvent == null)
                {
                    throw new Exception("Current event is null.");
                }
                return currentEvent.GetHashCode();
            }
        }

        public AnalyticsService()
        {
            events = new List<AnalyticsSessionDataModel>();
        }

        public void StartSession()
        {
            currentEvent = new AnalyticsSessionDataModel();
            currentEvent.StartTime = DateTime.Now;
            currentEvent.SessionStarted = true;
            events.Add(currentEvent);
        }
        public int FinishSession(bool autoSendEvent = true)
        {
            if (!IsSessionStarted())
            {
                throw new Exception("Cannot finish session");
            }
            currentEvent.EndTime = DateTime.Now;
            currentEvent.Complete = autoSendEvent;
            var eventID = currentEvent.GetHashCode();
            currentEvent = null;
            return eventID;
        }

        AnalyticsSessionDataModel GetEventModel(int eventID)
        {
            var model = events.Find(item => item.GetHashCode() == eventID);
            return model;
        }

        void SendEventToServer(AnalyticsSessionDataModel model)
        {
            if (model == null)
            {
                Debug.Log("Model is null");
                return;
            }
            var duration = model.EndTime - model.StartTime;
            var sessionLogParameters = new Dictionary<string, object>();
            sessionLogParameters.Add("session_id", model.SessionId);
            sessionLogParameters.Add("start", model.StartTime.ToString(dateFormat));
            sessionLogParameters.Add("end", model.EndTime.ToString(dateFormat));
            sessionLogParameters.Add("session_duration", duration.TotalSeconds);
            sessionLogParameters.Add("taps", (long)model.Taps);
            sessionLogParameters.Add("swipes", (long)model.Swipes);
            sessionLogParameters.Add("share", model.ShareWasPressed.ToString());
            sessionLogParameters.Add("buynow", model.BuyNowWasPressed.ToString());
            sessionLogParameters.Add("buynow_value", (double)model.BuyNowValue);
            sessionLogParameters.Add("information_sent", model.InformationSent.ToString());
            sessionLogParameters.Add("name", model.UserName);
            sessionLogParameters.Add("email", model.Email);
            sessionLogParameters.Add("phone", model.Phone);
            int index = -1;
            foreach (var item in model.VideoUrls)
            {
                index++;
                sessionLogParameters.Add($"video_{index}", item);
            }
            FirebaseAnalytics.LogEvent(SessionLogEvent, ConvertToFirebaseParameters(sessionLogParameters));
            PrintEventParametersToJson(SessionLogEvent, sessionLogParameters);
            var screenParameters = new Dictionary<string, object>();
            screenParameters.Add("screens_total", (long)model.VisitedScreens);
            screenParameters.Add("screens_unique", (long)model.UniqueScreens.Count);
            foreach (var item in model.ScreensDuration)
            {
                screenParameters.Add($"screen_{item.Key}_duration", (double)item.Value);
            }
            foreach (var item in model.ScreensTaps)
            {
                screenParameters.Add($"screen_{item.Key}_taps", (long)item.Value);
            }
            FirebaseAnalytics.LogEvent(ScreenEvent, ConvertToFirebaseParameters(screenParameters));
            PrintEventParametersToJson(ScreenEvent, screenParameters);
            var likeParameters = new Dictionary<string, object>();
            foreach (var item in model.RingsLikeState)
            {
                likeParameters.Add($"like_{item.Key}", item.Value.ToString());
            }
            likeParameters.Add("like_value", (double)model.LikeValue);
            FirebaseAnalytics.LogEvent(LikeEvent, ConvertToFirebaseParameters(likeParameters));
            PrintEventParametersToJson(LikeEvent, likeParameters);
            Debug.Log("Log sent");
            Dispose();
        }

        Parameter[] ConvertToFirebaseParameters(Dictionary<string, object> input)
        {
            var parameters = new List<Parameter>();
            foreach (var item in input)
            {
                if (item.Value.GetType() == typeof(string))
                {
                    parameters.Add(new Parameter(item.Key, (string)item.Value));
                }
                else if (item.Value.GetType() == typeof(long))
                {
                    parameters.Add(new Parameter(item.Key, (long)item.Value));
                }
                else if (item.Value.GetType() == typeof(double))
                {
                    parameters.Add(new Parameter(item.Key, (double)item.Value));
                }
            }
            return parameters.ToArray();
        }

        void PrintEventParametersToJson(string eventName, object parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            Debug.Log($"{eventName} data:/n{json}");
        }

        public void SetSessionID(string id)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.SessionId = id;
        }

        public void RegisterBuyNowClick(float ringPrice)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.BuyNowValue += ringPrice;
            currentEvent.BuyNowWasPressed = true;
        }

        public void RegisterEmail(string email)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.Email = email;
        }

        public void RegisterLikeValue(float likeValue)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.LikeValue += likeValue;
        }

        public void RegisterName(string userName)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.UserName = userName;
        }

        public void RegisterPersonalInfo()
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.InformationSent = true;
        }

        public void RegisterPhone(string phone)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.Phone = phone;
        }

        public void RegisterRingLikeState(string ringName, bool ringLikeState)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            if (!currentEvent.RingsLikeState.ContainsKey(ringName))
            {
                currentEvent.RingsLikeState.Add(ringName, ringLikeState);
            }
            else
            {
                currentEvent.RingsLikeState[ringName] = ringLikeState;
            }
        }

        public void RegisterScreenDuration(int eventID, string screenID, float duration)
        {
            var model = GetEventModel(eventID);
            if (model == null)
            {
                return;
            }
            if (model.ScreensDuration.ContainsKey(screenID))
            {
                model.ScreensDuration[screenID] += duration;
            }
            else
            {
                model.ScreensDuration.Add(screenID, duration);
            }
        }

        public void RegisterSwipe()
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.Swipes++;
        }

        public void RegisterTap(string screenID)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.Taps++;
            if (currentEvent.ScreensTaps.ContainsKey(screenID))
            {
                currentEvent.ScreensTaps[screenID]++;
            }
            else
            {
                currentEvent.ScreensTaps.Add(screenID, 1);
            }
        }

        public void RegisterVideoRecords(int eventID, string[] videoUrls)
        {
            var targetEvent = GetEventModel(eventID);
            if (targetEvent != null)
            {
                targetEvent.VideoUrls = videoUrls;
                targetEvent.VideosReady = true;
            }
        }

        public void RegisterVisitedScreen(string screenId)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            currentEvent.VisitedScreens++;
            if (!currentEvent.UniqueScreens.Contains(screenId))
            {
                currentEvent.UniqueScreens.Add(screenId);
            }
        }

        bool IsSessionStarted()
        {
            if (currentEvent == null)
            {
                Debug.Log("currentEvent is null");
                return false;
            }
            return true;
        }

        void Initialize()
        {

        }

        void Dispose()
        {

        }

        public void InitRings(List<string> list)
        {
            if (!IsSessionStarted())
            {
                return;
            }
            foreach (var item in list)
            {
                if (!currentEvent.RingsLikeState.ContainsKey(item))
                {
                    currentEvent.RingsLikeState.Add(item, false);
                }
            }
        }

        public void Tick()
        {
            if (events != null)
            {
                foreach (var item in events)
                {
                    if (item.Complete || item.Ended && item.VideosReady)
                    {
                        Debug.Log("Sending event " + item.GetHashCode());
                        SendEventToServer(item);
                        item.Sent = true;
                    }
                }
                events.RemoveAll(item => item.Sent);
            }
        }
    }
}
