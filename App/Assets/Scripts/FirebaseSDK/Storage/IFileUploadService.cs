using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.FirebaseSDK.Storage
{
    interface IFileUploadService
    {
        Task<string> UploadFile(string localFilePath, string storageSavePath);

        Task<string[]> UploadFiles(Dictionary<string, string> pathData);

        void Init();
    }
}
