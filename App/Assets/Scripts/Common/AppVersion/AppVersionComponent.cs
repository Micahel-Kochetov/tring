using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.AppVersion
{
    class AppVersionComponent : MonoBehaviour
    {
        [SerializeField]
        private Text appVersionText;

        private void Start()
        {
            var appVersionService = new AppVersionService();
            StartCoroutine(appVersionService.GetAppFullVersionAsync(OnModelLoaded));
        }

        private void OnModelLoaded(string version)
        {
            appVersionText.text = version;
        }
    }
}
