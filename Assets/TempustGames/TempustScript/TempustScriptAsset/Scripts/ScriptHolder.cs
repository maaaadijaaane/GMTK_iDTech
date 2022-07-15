using System.Collections.Generic;
using UnityEngine;
//using tempustgames.scriptprototype.interfaces;

namespace TempustScript
{
    public class ScriptHolder : MonoBehaviour
    {
        protected TSScript script;
        public TextAsset scriptFile;

        [SerializeField] private List<GameObject> objects;
        [SerializeField] private List<string> objectReferences;

        // Start is called before the first frame update
        public void Start()
        {
            LoadScript();
        }

        public bool Execute(GameObject player = null)
        {
            if (player != null && script.objects != null)
                script.SetPlayer(player);

            if (!script.isRunning)
            {
                TSManager.singleton.OnScriptStart(script);
                script.ExecuteRegion("default", true);
                return true;
            }
            return false;
        }

        private void LoadScript()
        {
            if (scriptFile == null)
            {
                Debug.LogError("Error: ScriptHolder with no assigned script");
            }
            script = TSEncryption.DeserializeScript(scriptFile);
            script.SetGlobalReference(GameStateManager.singleton.GetGlobalFlags());
            script.SetLocalReference(GameStateManager.singleton.GetLocalFlags(gameObject.name));

            script.AssignObjects(gameObject, objects);
            script.holder = this;

            if (script.GetRegion("init") != null) {
                script.ExecuteRegion("init");
            }
        }

        [ContextMenu("Update Object List")]
        private void GetObjects()
        {
            script = TSEncryption.DeserializeScript(scriptFile);
            objectReferences = script.objects;
            int numObjects = objectReferences.Count;
            while (objects.Count != numObjects)
            {
                if (objects.Count < numObjects)
                {
                    objects.Add(null);
                }
                else if (objects.Count > numObjects)
                {
                    objects.RemoveAt(objects.Count - 1);
                }
            }
        }
    }
}