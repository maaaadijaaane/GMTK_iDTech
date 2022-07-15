using UnityEngine;
using UnityEditor;

namespace TempustScript
{
    [CustomEditor(typeof(ScriptHolder))]
    public class ScriptHolderInspector : Editor
    {
        SerializedProperty scriptFile;
        SerializedProperty objectList;
        SerializedProperty objectReferences;

        void OnEnable()
        {
            objectList = serializedObject.FindProperty("objects");
            scriptFile = serializedObject.FindProperty("scriptFile");
            objectReferences = serializedObject.FindProperty("objectReferences");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(scriptFile, new GUIContent("Script:"));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Objects:");

            for (int i = 0; i < objectList.arraySize; i++)
            {
                EditorGUILayout.PropertyField(objectList.GetArrayElementAtIndex(i), new GUIContent("\t" + objectReferences.GetArrayElementAtIndex(i).stringValue));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}