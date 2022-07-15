using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TempustScript
{
    public class TSEditorWindow : EditorWindow
    {
        byte[] tempKey = new byte[48];
        string keyString = "Click the button to generate a new key.";
        string scriptPath;
        string compilePath;

        private void OnEnable()
        {
            titleContent.text = "Tempust Script";
            scriptPath = EditorPrefs.GetString("TSPath");
            compilePath = EditorPrefs.GetString("TSCompPath");
        }

        private void OnDisable()
        {
        }

        [MenuItem("Window/Tempust Script")]
        public static void ShowWindow()
        {
            GetWindow(typeof(TSEditorWindow));
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Tempust Script", EditorStyles.boldLabel);

            //Key Display
            EditorGUILayout.LabelField("Encryption Key:");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.SelectableLabel(keyString, EditorStyles.helpBox);

            if (GUILayout.Button("Copy", GUILayout.Width(70)))
                GUIUtility.systemCopyBuffer = keyString;
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Generate Key"))
                GenerateKey();
            EditorGUILayout.Space();
            EditorGUILayout.PrefixLabel("Scripting Directory: ");
            EditorGUILayout.BeginHorizontal();
            scriptPath = EditorGUILayout.TextField(scriptPath, EditorStyles.textField);
            if (GUILayout.Button("Choose Path"))
            {
                scriptPath = EditorUtility.OpenFolderPanel("Select a TempustScript Directory", "", "");
                EditorPrefs.SetString("TSPath", scriptPath);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PrefixLabel("Compile Directory: ");
            EditorGUILayout.BeginHorizontal();
            compilePath = EditorGUILayout.TextField(compilePath, EditorStyles.textField);
            if (GUILayout.Button("Choose Path"))
            {
                compilePath = EditorUtility.OpenFolderPanel("Select a Directory for Compiled Scripts", "", "");
                EditorPrefs.SetString("TSCompPath", compilePath);
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Compile Scripts"))
            {
                var assembly = Assembly.GetAssembly(typeof(Editor));
                var type = assembly.GetType("UnityEditor.LogEntries");
                var method = type.GetMethod("Clear");
                method.Invoke(new object(), null);
                TSEncryption.Compile(scriptPath, compilePath);
            }
        }

        private void GenerateKey()
        {
            new System.Random().NextBytes(tempKey);
            keyString = System.BitConverter.ToString(tempKey);
        }
    }
}
