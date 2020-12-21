#if UNITY_EDITOR

using Assets.Scripts.Common.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(SelectableImage))]
    [CanEditMultipleObjects]
    public class SelectableImageEditor : ImageEditor
    {
        SerializedProperty initialSprite;
        SerializedProperty selectedSprite;

        protected override void OnEnable()
        {
            base.OnEnable(); 
            initialSprite = serializedObject.FindProperty("initialSprite");
            selectedSprite = serializedObject.FindProperty("selectedSprite");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(initialSprite);
            EditorGUILayout.PropertyField(selectedSprite);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
