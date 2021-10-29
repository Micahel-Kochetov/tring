using System;
using Common.UI;
using UnityEngine;

namespace Assets.Scripts.States.Common.View
{
    public class AnimatedSelectableModalButton : SelectableModalButton
    {
        Animator animator;
        string selectedParameterName = "selected";

        public override void Init(Action handler)
        {
            base.Init(handler);
            animator = transform.GetComponentInChildren<Animator>();
        }

        public override void SetSelected()
        {
            base.SetSelected();
            animator?.SetBool(selectedParameterName, true);
        }

        public override void SetDeselected()
        {
            base.SetDeselected();
            animator?.SetBool(selectedParameterName, false);
        }

    }
}
