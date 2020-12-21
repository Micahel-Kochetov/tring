using Zenject;

namespace Assets.Scripts.Common.Controller
{
    public abstract class AbstractController :  ITickable
    {
        private bool isActive;

        public virtual void Activate()
        {
            ActivateController();
        }

        public virtual void Deactivate()
        {
            DeactivateController();
        }

        public virtual void Dispose() { }

        public void Tick()
        {
            Update();
        }

        public void Update()
        {
            if (isActive)
            {
                OnUpdate();
            }
        }


        void ActivateController()
        {
            isActive = true;
        }

        void DeactivateController()
        {
            isActive = false;
        }

        protected virtual void OnUpdate() { }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
    }
}
