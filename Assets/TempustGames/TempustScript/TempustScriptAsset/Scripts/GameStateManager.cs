using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace TempustScript
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager singleton;

        private Dictionary<string, bool> globalFlags;
        private Dictionary<string, Dictionary<string, bool>> localFlags;
        private Dictionary<string, Dictionary<string, Dictionary<string, bool>>> persistentData;
        
        public event UnityAction onSceneDataLoaded;

        public bool isSaving { get; private set; }

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded += LoadFlags;
            TSScript.onLocalFlagChanged += AddCurSceneToPersistentData;
        }

        private void LoadGlobalData()
        {
            FlagList loadFlags = TSEncryption.DeserializeGlobalData(Path.Combine(TSManager.singleton.GetSavePath(), "global.sav"));
            if (loadFlags != null)
            {
                globalFlags = loadFlags.GetDictionary();
            }
            else
            {
                globalFlags = new Dictionary<string, bool>();
            }
        }

        private void LoadSceneFlags(string scene)
        {
            if (persistentData == null)
                persistentData = new Dictionary<string, Dictionary<string, Dictionary<string, bool>>>();
            else if (localFlags != null && !persistentData.ContainsKey(scene))
                persistentData.Add(SceneManager.GetActiveScene().name, localFlags);

            if (persistentData.ContainsKey(scene))
            {
                localFlags = persistentData[scene];
            }
            else
            {
                FlagList.FlagListGroup flagLists = TSEncryption.DeserializeLocalData(Path.Combine(TSManager.singleton.GetSavePath(), scene + ".sav"));
                if (flagLists == null)
                {
                    localFlags = new Dictionary<string, Dictionary<string, bool>>();
                    return;
                }

                if (localFlags == null)
                {
                    localFlags = new Dictionary<string, Dictionary<string, bool>>();
                }
                else
                {
                    localFlags.Clear();
                }
                foreach (FlagList flag in flagLists.GetList())
                {
                    localFlags.Add(flag.Label, flag.GetDictionary());
                }
            }
            onSceneDataLoaded?.Invoke();
        }

        private void LoadFlags(Scene scene, LoadSceneMode mode)
        {
            if (globalFlags == null)
            { 
                LoadGlobalData();
            }
            LoadSceneFlags(scene.name);
        }

        private void AddPersistentData(string sceneName, Dictionary<string, Dictionary<string, bool>> data)
        {
            if (persistentData.ContainsKey(sceneName))
                return;

            persistentData.Add(sceneName, data);
        }

        public Dictionary<string, bool> GetGlobalFlags()
        {
            return globalFlags;
        }

        public Dictionary<string, bool> GetLocalFlags(string objectName)
        {
            if (localFlags.ContainsKey(objectName))
                return localFlags[objectName];
            else
            {
                Dictionary<string, bool> objectFlags = new Dictionary<string, bool>();
                localFlags.Add(objectName, objectFlags);
                return objectFlags;
            }
        }
        
        /// <summary>
        /// Adds the current scene's local flags to the list of data to save.
        /// </summary>
        /// <param name="scene">Name of the scene to add.</param>
        public void AddCurSceneToPersistentData()
        {
            string scene = SceneManager.GetActiveScene().name;
            AddPersistentData(scene, localFlags);
        }

        public IEnumerator Save()
        {
            isSaving = true;
            yield return null;

            TSEncryption.SerializeGlobalData(Path.Combine(TSManager.singleton.GetSavePath(), "global.sav"), new FlagList("global", globalFlags));

            AddPersistentData(SceneManager.GetActiveScene().name, localFlags);
            foreach (string scene in persistentData.Keys)
            {
                FlagList[] localFlags = new FlagList[persistentData[scene].Count];
                int index = 0;
                foreach (KeyValuePair<string, Dictionary<string, bool>> data in persistentData[scene])
                {
                    localFlags[index] = new FlagList(data.Key, data.Value);
                    index++;
                }

                TSEncryption.SerializeLocalData(Path.Combine(TSManager.singleton.GetSavePath(), scene + ".sav"), new FlagList.FlagListGroup(localFlags));
            }
            persistentData.Clear();
            isSaving = false;
        }

        public void ClearSaveData()
        {
            globalFlags.Clear();
            localFlags.Clear();
            persistentData.Clear();

            foreach (string file in Directory.GetFiles(TSManager.singleton.GetSavePath()))
            {
                if (Path.GetExtension(file) == ".sav")
                {
                    File.Delete(file);
                }
            }
        }
    }
}