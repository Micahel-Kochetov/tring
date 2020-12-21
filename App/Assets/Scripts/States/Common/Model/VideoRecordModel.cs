using Assets.Scripts.States.Common.Interface;

namespace Assets.Scripts.States.Common.Model
{
    public class VideoRecordModel : IVideoPreviewPath
    {
        public string FilePath { get; private set; }
        string convertedVideoPath;
        public string ConvertedVideoPath { get
            {
                if (convertedVideoPath == null)
                {
                    return FilePath;
                }
                else {
                    return convertedVideoPath;
                }
            }  set {
                convertedVideoPath = value;
            } }
        public string ThumbnailPath { get; private set; }

        public VideoRecordModel(string filePath, string thumbnailPath)
        {
            FilePath = filePath;
            ThumbnailPath = thumbnailPath;
        }
    }
}
