namespace Common.UI
{
    using System;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine;
    using System.Threading.Tasks;

    public class ModalButton : Button, IButton
    {
        public event Action<PointerEventData> OnPointerDownEvent;
        public event Action<PointerEventData> OnPointerUpEvent;
        protected Action handler;
        protected bool isEnabled = false;
        private UnityAction call;
        [SerializeField]
        bool enableParticles = false;
        [SerializeField]
        ParticleSystem particleSystem;
        float particlesDuration = 4/9f;
        bool isPlayingParticles;
        [SerializeField]
        bool vibrateOnClick = false;

        protected override void Start()
        {
            base.Start();
        }

        public void Subscribe()
        {
            onClick.AddListener(call);
        }

        public void Unsubscribe()
        {
            onClick.RemoveAllListeners();
        }

        public void EnableButton()
        {
            isEnabled = true;
        }

        public void DisableButton()
        {
            isEnabled = false;
        }

        public virtual void Init(Action handler)
        {
            this.handler = handler;
            isEnabled = true;
            call = new UnityAction(ClickHandler);
            isPlayingParticles = false;
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerDownEvent?.Invoke(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnPointerUpEvent?.Invoke(eventData);
        }

        protected virtual void ClickHandler()
        {
            if (isEnabled)
            {
                if (vibrateOnClick)
                {
                    //Handheld.Vibrate();
                }
                if (enableParticles)
                {
                    if (!isPlayingParticles)
                    {
                        isPlayingParticles = true;
                        PlayParticles();
                    }
                }
                else
                {
                    handler?.Invoke();
                }
            }
        }

        async Task PlayParticles()
        {
            if (particleSystem == null)
            {
                isPlayingParticles = false;
                Debug.LogWarning("Particles not assigned");
                handler?.Invoke();
            }
            else
            {
                particleSystem.Play();
                Debug.Log(particlesDuration);
                await Task.Delay((int)(particlesDuration * 1000));
                particleSystem.Stop();
                isPlayingParticles = false;
                handler?.Invoke();
            }
        }
    }
}
