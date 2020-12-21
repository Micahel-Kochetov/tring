using System;
using UnityEngine;

namespace Assets.Scripts.CoreStates
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField]
        private GameObject parent;
        private RectTransform rectTransform;

        public virtual void Init()
        {
            if (parent != null)
            {
                parent.SetActive(false);
            }
            IsActive = false;
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update() { }

        public virtual void Show()
        {
            if (parent != null)
            {
                parent.SetActive(true);
            }
            IsActive = true;
        }

        public virtual void Hide()
        {
            if (parent != null)
            {
                parent.SetActive(false);
            }
            IsActive = false;
        }

        public bool IsVisible
        {
            get
            {
                if (parent == null)
                {
                    return false;
                }
                else
                {
                    return parent.activeSelf;
                }
            }
        }

        public GameObject Parent
        {
            get
            {
                return parent;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null && parent != null)
                {
                    rectTransform = parent.GetComponent<RectTransform>();
                }
                return rectTransform;
            }
        }

        protected bool IsActive {get; private set; }
    }
}
