using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{

    [CustomEditor(typeof(RectPositionAnimator))]
    [CanEditMultipleObjects]
    public class RectPositionAnimatorEditor : UnityEditor.Editor
    {
        private RectPositionAnimator animator;

        private void OnEnable()
        {
            animator = (RectPositionAnimator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(10);
            if (GUILayout.Button("Initialize"))
            {
                animator.Init();
            }
            if (GUILayout.Button("Animate"))
            {
                animator.Animate();
            }
        }
    }
}
