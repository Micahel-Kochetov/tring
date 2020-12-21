using Assets.Scripts.Common.UI;
using UnityEngine;

namespace Assets.Scripts.Common.Utils
{

    public class RectPositionAnimator : MonoBehaviour
    {
        [SerializeField]
        float animationDuration = 1f;
        [SerializeField]
        RectTransform blik;
        [SerializeField]
        RectTransform blikParent;
        Vector2 startPos;
        Vector2 endPos;
        bool animationEnabled;
        float time;


        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        public void Init()
        {
            var pos = blikParent.rect.width / 2f + blik.rect.width / 2f;
            startPos = new Vector2(-pos, 0f);
            endPos = new Vector2(pos, 0f);
            blik.anchoredPosition = startPos;
            animationEnabled = false;
            blik.SetHeight(blikParent.rect.height);
        }

        public void Animate()
        {
            time = 0;
            animationEnabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (animationEnabled)
            {
                time += Time.deltaTime;
                var progress = time / animationDuration;
                if (progress > 1)
                {
                    animationEnabled = false;
                    blik.anchoredPosition = startPos;
                }
                else
                {
                    var x = Mathf.Lerp(startPos.x, endPos.x, progress);
                    var y = Mathf.Lerp(startPos.y, endPos.y, progress);
                    blik.anchoredPosition = new Vector2(x, y);
                }
            }
        }
    }
}
