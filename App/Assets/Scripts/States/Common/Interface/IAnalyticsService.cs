using System.Collections.Generic;

namespace Assets.Scripts.States.Common.Interface
{
    public interface IAnalyticsService
    {
        void SetSessionID(string id);
        void StartSession();
        int FinishSession(bool autoSendEvent=true);
        void RegisterTap(string screenID);
        void RegisterSwipe();
        void RegisterVisitedScreen(string id);
        void RegisterRingLikeState(string ringName, bool ringLikeState);
        void RegisterBuyNowClick(float ringPrice);
        void RegisterLikeValue(float totalValue);
        void RegisterScreenDuration(int eventID, string screenID, float duration);
        void RegisterPersonalInfo();
        void RegisterName(string userName);
        void RegisterEmail(string email);
        void RegisterPhone(string phone);
        void RegisterVideoRecords(int eventID, string[] videoUrls);
        int CurrentEventID { get; }

        void InitRings(List<string> list);
    }
}
