using UnityEditor;
using TempustScript.Shortcuts;

namespace TempustScript
{
    [CustomEditor(typeof(TSDialogue)), CanEditMultipleObjects]
    public class TSDialogueEditor : Editor
    {

        public SerializedProperty messageProperty;
        public SerializedProperty speakerProperty;
        void OnEnable()
        {
            messageProperty = serializedObject.FindProperty("message");
            speakerProperty = serializedObject.FindProperty("speaker");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Speaker:");
            speakerProperty.stringValue = EditorGUILayout.TextField(speakerProperty.stringValue);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Message:");
            messageProperty.stringValue = EditorGUILayout.TextArea(messageProperty.stringValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
}