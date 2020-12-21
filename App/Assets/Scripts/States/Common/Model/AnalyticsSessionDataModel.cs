using System;
using System.Collections.Generic;

namespace Assets.Scripts.States.Common.Model
{
    public class AnalyticsSessionDataModel
    {
        public DateTime StartTime;
        private DateTime endTime;
        public string SessionId;
        public bool SessionStarted;
        public int Taps;
        public int Swipes;
        public int VisitedScreens;
        public List<string> UniqueScreens;
        public Dictionary<string, bool> RingsLikeState;
        public bool ShareWasPressed;
        public bool BuyNowWasPressed;
        public float BuyNowValue;
        public float LikeValue;
        public Dictionary<string, float> ScreensDuration;
        public Dictionary<string, int> ScreensTaps;
        public bool InformationSent;
        public string UserName;
        public string Email;
        public string Phone;
        public string[] VideoUrls;
        public bool VideosReady;
        public bool Complete;
        public bool Sent { get; internal set; }
        public bool Ended
        {
            get; private set;
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                Ended = true;
            }
        }

        public AnalyticsSessionDataModel()
        {
            StartTime = default(DateTime);
            EndTime = default(DateTime);
            SessionStarted = false;
            Taps = 0;
            Swipes = 0;
            VisitedScreens = 0;
            UniqueScreens = new List<string>();
            RingsLikeState = new Dictionary<string, bool>();
            ShareWasPressed = false;
            BuyNowWasPressed = false;
            BuyNowValue = 0;
            LikeValue = 0;
            ScreensDuration = new Dictionary<string, float>();
            ScreensTaps = new Dictionary<string, int>();
            InformationSent = false;
            UserName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            VideoUrls = new string[0];
            VideosReady = false;
            Ended = false;
            Complete = false;
            Sent = false;
        }
    }
}
