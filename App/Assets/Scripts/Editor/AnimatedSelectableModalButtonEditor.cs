#if UNITY_EDITOR

using Assets.Scripts.States.Common.View;
using UnityEditor;

namespace Common.UI
{
    [CustomEditor(typeof(AnimatedSelectableModalButton))]
    [CanEditMultipleObjects]
    public class AnimatedSelectableModalButtonEditor : SelectableModalButtonEditor
    {
       
    }
}

#endif

