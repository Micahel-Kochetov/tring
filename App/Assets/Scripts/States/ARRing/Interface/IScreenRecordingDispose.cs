namespace Assets.Scripts.States.ARRing.Interface
{
    public interface IScreenRecordingDispose
    {
        void DisposeVideos();
        bool IsRecording { get; }
    }
}