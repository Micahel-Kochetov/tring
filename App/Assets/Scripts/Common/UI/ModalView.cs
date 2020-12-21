using Assets.Scripts.CoreStates;
using System;
using System.Collections.Generic;

namespace Common.UI
{
    public abstract class ModalView : BaseView
    {
        protected List<IButton> buttons;

        public override void Show()
        {
            base.Show();
            Unsubscribe();
            Subscribe();
        }

        public override void Hide()
        {
            base.Hide();
            Unsubscribe();
        }

        public override void Init()
        {
            base.Init();
            buttons = new List<IButton>();
            AddButtons();
        }

        public virtual void Dispose() { }

        public virtual void Subscribe()
        {
            if (buttons != null)
            {
                foreach (var item in buttons)
                {
                    item.Subscribe();
                }
            }
        }

        public virtual void Unsubscribe()
        {
            if (buttons != null)
            {
                foreach (var item in buttons)
                {
                    item.Unsubscribe();
                }
            }
        }

        protected void AddButton(IButton btn, Action clickListener)
        {
            buttons.Add(btn);
            btn.Init(clickListener);
        }

        protected virtual void AddButtons()
        {
        }
    }
}
