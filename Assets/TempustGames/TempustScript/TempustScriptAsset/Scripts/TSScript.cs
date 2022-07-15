using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using TempustScript.InterpreterException;
using UnityEngine;

namespace TempustScript
{
    [DataContract(Name = "TSScript", IsReference =true)]
    public class TSScript
    {
        [DataMember] public List<string> objects { get; private set; }
        [DataMember] private Dictionary<string, Region> regions = new Dictionary<string, Region>();

        private Dictionary<string, bool> localFlags;
        private Dictionary<string, bool> globalFlags;
        private Dictionary<string, GameObject> gameObjects;

        private Coroutine curExecution;

        public static event Action onLocalFlagChanged;

        public MonoBehaviour holder { get; set; }
        public bool isRunning { get { return curExecution != null; }}

        public TSScript()
        {
            objects = new List<string>();
        }

        public GameObject GetObject(string obj)
        {
            if (!gameObjects.ContainsKey(obj)) 
                return null;
            return gameObjects[obj];
        }

        public void AssignObjects(GameObject holder, List<GameObject> objectList)
        {
            gameObjects = new Dictionary<string, GameObject>();
            gameObjects.Add("obj", holder);
            
            if (objectList != null)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    gameObjects.Add(objects[i], objectList[i]);
                }
            }
        }

        public void SetPlayer(GameObject playerObj)
        {
            if (gameObjects.ContainsKey("plr"))
            {
                gameObjects["plr"] = playerObj;
            }
            else
            {
                gameObjects.Add("plr", playerObj);
            }
        }

        public void AddObject(string obj)
        {
            objects.Add(obj);
        }

        public Region GetRegion(string region)
        {
            Region outRegion;
            regions.TryGetValue(region, out outRegion);
            return outRegion;
        }

        public void AddRegion(Region region)
        {
            if (regions.ContainsKey(region.GetName()))
            {
                if (region.GetName().Equals("default"))
                {
                    throw new InvalidRegionException("Script already contains a default region");
                }
                throw new InvalidRegionException("Script already contains region " + region.GetName());
            }

            regions.Add(region.GetName(), region);
        }

        public void SetGlobalReference(Dictionary<string, bool> flags)
        {
            globalFlags = flags;
        }
        public void SetLocalReference(Dictionary<string, bool> flags)
        {
            localFlags = flags;
        }

        public bool GetLocalFlag(string flag)
        {
            return localFlags.ContainsKey(flag) && localFlags[flag];
        }

        public bool GetGlobalFlag(string flag)
        {
            return globalFlags.ContainsKey(flag) && globalFlags[flag];
        }

        public void SetLocalFlag(string flag, bool value)
        {
            if (localFlags.ContainsKey(flag))
            {
                localFlags[flag] = value;
            }
            else
            {
                localFlags.Add(flag, value);
            }
            onLocalFlagChanged?.Invoke();
        }

        public void SetGlobalFlag(string flag, bool value)
        {
            if (globalFlags.ContainsKey(flag))
            {
                globalFlags[flag] = value;
            }
            else
            {
                globalFlags.Add(flag, value);
            }
        }

        public void StopExecution(bool scriptFinished)
        {
            if (curExecution != null)
            {
                holder.StopCoroutine(curExecution);
                curExecution = null;
            }
            if (scriptFinished)
            {
                TSManager.singleton.OnScriptEnd(this);
            }
        }

        /// <summary>
        /// Stop the current region and execute a new one.
        /// </summary>
        /// <param name="region">The region to execute</param>
        /// <param name="isStart">True if the script is beginning execution for the first time, false if using goto.</param>
        public void ExecuteRegion(string region, bool isStart = false)
        {
            StopExecution(false);
            curExecution = holder.StartCoroutine(GetRegion(region).Execute());
        }
    }
}