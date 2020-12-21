#if UNITY_EDITOR

using Common.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ModalButton))]
    [CanEditMultipleObjects]
    public class ModalButtonEditor : ButtonEditor
    {
        SerializedProperty enableParticles;
        SerializedProperty particleSystem;
        SerializedProperty enableVibration;

        protected override void OnEnable()
        {
            base.OnEnable();
            enableParticles = serializedObject.FindProperty("enableParticles");
            particleSystem = serializedObject.FindProperty("particleSystem");
            enableVibration = serializedObject.FindProperty("vibrateOnClick");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(enableParticles);
            if (enableParticles.boolValue)
            {
                EditorGUILayout.PropertyField(particleSystem);
            }
            EditorGUILayout.PropertyField(enableVibration);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif
