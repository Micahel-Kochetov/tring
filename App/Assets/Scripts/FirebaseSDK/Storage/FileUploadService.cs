using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FirebaseSDK.Storage
{
    public class FileUploadService : IFileUploadService, IDisposable
    {
        bool isAuthenticated;
        string storageUrl = "gs://growring-test.appspot.com";
        string UriFileScheme = Uri.UriSchemeFile + "://";

        public FileUploadService()
        {
            isAuthenticated = false;
        }

        public void Dispose()
        {
            isAuthenticated = false;
        }

        public void Init()
        {
            if (!isAuthenticated)
            {
                Authenticate();
            }
        }

        public async Task<string> UploadFile(string localFilePath, string storageSavePath)
        {
            if (!isAuthenticated)
            {
                Debug.LogError("User is not authenticated");
                await Authenticate();
                if (!isAuthenticated)
                {
                    var userNotAuthenticatedMsg = "Failed to authenticate user";
                    Debug.LogError(userNotAuthenticatedMsg);
                    throw new Exception(userNotAuthenticatedMsg);
                }
            }
            if (!File.Exists(localFilePath))
            {
                var msg = $"File at path {localFilePath} does not exist";
                Debug.Log(msg);
                throw new Exception(msg);
            }
            var formattedPath = FormatPath(localFilePath);
            var storage = FirebaseStorage.DefaultInstance;
            var storage_ref = storage.GetReferenceFromUrl(storageUrl);
            var rivers_ref = storage_ref.Child(storageSavePath);
            Debug.Log($"FileUploadService::UploadFile \n Source path:{localFilePath} \n Cloud path:{storageSavePath}");
            Task<StorageMetadata> uploadTask = null;
            await rivers_ref.PutFileAsync(formattedPath).ContinueWith(task => uploadTask = task);
            if (uploadTask.IsFaulted)
            {
                Debug.Log(uploadTask.Exception);
                throw new AggregateException(uploadTask.Exception);
            }
            else
            {
                Debug.Log("upload complete...getting url...");
                Task<string> getUrlTask = null;
                await GetFileUrl(rivers_ref).ContinueWith(task => getUrlTask = task);
                if (getUrlTask.IsFaulted)
                {
                    Debug.Log(getUrlTask.Exception);
                    throw new AggregateException(getUrlTask.Exception);
                }
                else
                {
                    var url = getUrlTask.Result;
                    Debug.Log("url:" + url);
                    return url;
                }
            }
        }

        public async Task<string[]> UploadFiles(Dictionary<string, string> pathData)
        {
            var urls = new List<string>();
            foreach (var item in pathData.Keys)
            {
                Task<string> uploadFileTask = null;
                await UploadFile(item, pathData[item]).ContinueWith(task => { uploadFileTask = task; });
                if (uploadFileTask.IsFaulted)
                {
                    Debug.Log("Exception occured during upload: " + uploadFileTask.Exception);
                }
                else if (uploadFileTask.IsCanceled)
                {
                    Debug.Log("Upload file task is cancelled");
                }
                else
                {
                    urls.Add(uploadFileTask.Result);
                }
            }
            return urls.ToArray();
        }

        async Task<string> GetFileUrl(StorageReference storageRef)
        {
            var fileUploadUrl = string.Empty;
            await storageRef.GetDownloadUrlAsync()
                          .ContinueWith(
                              (Task<System.Uri> taskUri) =>
                              {
                                  if (taskUri.IsFaulted || taskUri.IsCanceled)
                                  {
                                      var exceptionDetails = taskUri.Exception.ToString();
                                      Debug.Log(exceptionDetails);
                                      throw new Exception(exceptionDetails);
                                  }
                                  else
                                  {
                                      fileUploadUrl = taskUri.Result.AbsoluteUri;
                                  }
                              }
                              );
            return fileUploadUrl;
        }

        async Task Authenticate()
        {
            var auth = FirebaseAuth.DefaultInstance;
            await auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }
                var newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                isAuthenticated = true;
            });
        }

        string FormatPath(string filePath)
        {
            return string.Format("{0}{1}", UriFileScheme, filePath);
        }
    }
}
