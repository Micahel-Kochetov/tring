#if UNITY_EDITOR

using Assets.Scripts.Editor;
using UnityEditor;
using UnityEditor.UI;

namespace Common.UI
{
    [CustomEditor(typeof(SelectableModalButton))]
    [CanEditMultipleObjects]
    public class SelectableModalButtonEditor : ModalButtonEditor
    {
        SerializedProperty initialColor;
        SerializedProperty initialSprite;
        SerializedProperty initialTextColor;
        SerializedProperty selectedColor;
        SerializedProperty selectedSprite;
        SerializedProperty selectedTextColor;

        protected override void OnEnable()
        {
            base.OnEnable();
            initialColor = serializedObject.FindProperty("initialColor");
            initialSprite = serializedObject.FindProperty("initialSprite");
            initialTextColor = serializedObject.FindProperty("initialTextColor");
            selectedColor = serializedObject.FindProperty("selectedColor");
            selectedSprite = serializedObject.FindProperty("selectedSprite");
            selectedTextColor = serializedObject.FindProperty("selectedTextColor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(initialColor);
            EditorGUILayout.PropertyField(initialSprite);
            EditorGUILayout.PropertyField(initialTextColor);
            EditorGUILayout.PropertyField(selectedColor);
            EditorGUILayout.PropertyField(selectedSprite);
            EditorGUILayout.PropertyField(selectedTextColor);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif

