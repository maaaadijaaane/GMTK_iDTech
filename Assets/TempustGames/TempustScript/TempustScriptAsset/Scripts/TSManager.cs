using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TempustScript.Commands;
using System;
using TempustScript.Blocks;

namespace TempustScript
{
    public class TSManager : MonoBehaviour
    {
        public static TSManager singleton;

        void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Called just before a script begins execution.
        /// </summary>
        /// <param name="script">Script being executed</param>
        public virtual void OnScriptStart(TSScript script) {}

        /// <summary>
        /// Called just after a script finishes execution.
        /// </summary>
        /// <param name="script">The finished script</param>
        public virtual void OnScriptEnd(TSScript script) {}

        /// <summary>
        /// Implementation for the "goto" command. This generally doesn't need custom implementation.
        /// </summary>
        /// <param name="script">The script running the command</param>
        /// <param name="region">The region to execute</param>
        public virtual IEnumerator OnGoto(TSScript script, string region)
        {
            script.ExecuteRegion(region);
            yield return null;
        }
        
        /// <summary>
        /// Implementation for the "end" command. By default this closes the textbox and ends the coroutine. This can be overridden to execute custom code when scripts are ended.
        /// To end the script, script.StopExecution()
        /// </summary>
        public virtual IEnumerator OnEnd(TSScript script)
        {
            TextboxController.singleton.Close();
            script.StopExecution(true);
            OnScriptEnd(script);
            yield return null;
        }

        /// <summary>
        /// Implementation for the "setflag" command. Override if not using the built-in flag system.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="isGlobal">True if the flag is global, false if local</param>
        /// <param name="flagName">Name of the flag to set</param>
        /// <param name="value">Value of the flag to set</param>
        public virtual IEnumerator OnSetFlag(TSScript script, bool isGlobal, string flagName, bool value)
        {
            if (isGlobal)
                script.SetGlobalFlag(flagName, value);
            else
                script.SetLocalFlag(flagName, value);
            yield return null;
        }

        /// <summary>
        /// Implementation for the "say" command and block. Override if using a different textbox or to replace certain keywords, such as the player's name or gender.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="speaker">Speaker for the text</param>
        /// <param name="message">Text to display</param>
        public virtual IEnumerator OnSay(TSScript script, string speaker, string message)
        {
            yield return TextboxController.singleton.Type(speaker, message, true);
            TextboxController.singleton.Close();
        }

        /// <summary>
        /// Implementation for the "ask" block. This is paired with GetAskResult(), which is called immediately after to get the selected option. If using a different textbox, override both.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="speaker"></param>
        /// <param name="question"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual IEnumerator OnAsk(TSScript script, string speaker, string question, List<string> options)
        {
            yield return TextboxController.singleton.AskQuestion(speaker, question, options);
        }

        /// <summary>
        /// Called immediately after OnAsk() to get the selected option from a question box.
        /// </summary>
        /// <returns>The selected option index.</returns>
        public virtual int GetAskResult()
        {
            return TextboxController.singleton.GetSelectedOption(true);
        }

        /// <summary>
        /// Implementation for the "setpos" command. By default this sets the GameObject's transform.position, but it may need overridden if using the Unity CharacterController. Use TSManager.GetObject(script, obj) to get the GameObject.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="obj">The object to set the position of</param>
        /// <param name="position">Vector position to set</param>
        public virtual IEnumerator OnSetPos(TSScript script, string obj, Vector3 position)
        {
            GetObject(script, obj).transform.position = position;
            yield return null;
        }

        /// <summary>
        /// Implementation for the "closebox" command. Override if using a different textbox.
        /// </summary>
        public virtual IEnumerator OnCloseBox(TSScript script)
        {
            TextboxController.singleton.Close();
            yield return null;
        }

        /// <summary>
        /// Implementation for the "walk" command. Use TSManager.GetObject(script, obj) to get the GameObject.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="obj">Object to move</param>
        /// <param name="toPos">Position vector to move to</param>
        public virtual IEnumerator OnWalk(TSScript script, string obj, Vector3 toPos)
        {
            yield return Move(GetObject(script, obj), 1, toPos);
        }

        /// <summary>
        /// Implementation for the "run" command. Use TSManager.GetObject(script, obj) to get the GameObject.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="obj">Object to move</param>
        /// <param name="toPos">Position vector to move to</param>
        public virtual IEnumerator OnRun(TSScript script, string obj, Vector3 toPos)
        {
            yield return Move(GetObject(script, obj), 2, toPos);
        }

        /// <summary>
        /// Implementation for the "face" command. Not implemented by default. Use TSManager.GetObject(script, obj) to get the GameObject.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="obj">Object to change facing direction</param>
        /// <param name="direction">Normalized vector in the direction to face.</param>
        public virtual IEnumerator OnFace(TSScript script, string obj, Vector3 direction)
        {
            Debug.LogError("Tempust Script Error: command \"face\" not implemented");
            yield return null;
        }

        /// <summary>
        /// Implementation for the "enable" and "disable" commands. Use TSManager.GetObject(script, obj) to get the GameObject.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="obj">Object to enable or disable</param>
        /// <param name="value">True if enable command, false if disable command</param>
        public virtual IEnumerator OnEnableDisable(TSScript script, string obj, bool value)
        {
            GetObject(script, obj).SetActive(value);
            yield return null;
        }

        /// <summary>
        /// Implementation for the "playsound" command. Not implemented by default.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="soundName">Name of the sound to play</param>
        public virtual IEnumerator OnPlaySound(TSScript script, string soundName)
        {
            Debug.LogError("Tempust Script Error: command \"playsound\" not implemented");
            yield return null;
        }


        /// <summary>
        /// Implementation for the "bgm" command. Not implemented by default.
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="musicName">Name of the music to play</param>
        /// <param name="musicIn">True if the music should be played, false if it should be stopped</param
        /// <param name="fade">True if the effect should be faded, false if it should be instant.</param>
        public virtual IEnumerator OnBGM(TSScript script, string musicName, bool musicIn, bool fade)
        {
            Debug.LogError("Tempust Script Error: command \"playsound\" not implemented");
            yield return null;
        }

        /// <summary>
        /// Implementation for the "playsound: command. Not implemented by default.
        /// </summary>
        /// <param name="parent">The script executing the command</param>
        /// <param name="isLeftPortrait">True if this if for the left portrait, false it is for the right portrait.</param>
        /// <param name="imageName">Name of the image to display</param>
        public virtual IEnumerator OnPortrait(TSScript parent, bool isLeftPortrait, string imageName)
        {
            Debug.LogError("Tempust Script Error: command \"portrait\" not implemented");
            yield return null;
        }

        /// <summary>
        /// Implementation for the "wait" command. By default this is the same as "yield return new WaitForSeconds(time)".
        /// </summary>
        /// <param name="script">The script executing the command</param>
        /// <param name="time">Time in seconds to wait.</param>
        /// <returns></returns>
        public virtual IEnumerator OnWait(TSScript script, float time)
        {
            yield return new WaitForSeconds(time);
        }

        /// <summary>
        /// Implementation for the "checkinv" condition. Using the parameters, determine how the condition should be evaluated.
        /// The GameObject interacting with the script holder can be retrieved with the GetObject() method.
        /// 
        /// </summary>
        /// <param name="condition">Condition object to be evaluated.</param>
        public virtual bool CheckInventory(TSScript script, string item, float amount, bool negate)
        {
            Debug.LogError("Tempust Script Error: \"checkinv\" not implemented");
            return false;
        }

        /// <summary>
        /// Get the data save path. Override this if you would like to save in a different location or use subdirectories for different save files.
        /// </summary>
        /// <returns>Returns the path to the save directory. By default this is Application.persistentDataPath</returns>
        public virtual string GetSavePath()
        {
            return Application.persistentDataPath;
        }

        /// <summary>
        /// Get a Unity game object from the given script. If not overridden, this is the same as script.GetObject(obj). Override if you want to handle certain object names separately, such as dynamically finding player objects from scripts.
        /// </summary>
        /// <param name="script">The script requesting the object.</param>
        /// <param name="obj">Name of the object</param>
        /// <returns>GameObject to perform commands on</returns>
        public virtual GameObject GetObject(TSScript script, string obj)
        {
            return script.GetObject(obj);
        }


        // Private helper method used by "walk" and "run" commands
        private IEnumerator Move(GameObject obj, float speed, Vector3 pos)
        {
            //Calculate how long the move will take
            while (Vector3.Distance(obj.transform.position, pos) > .01f)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, pos, .01f * speed);
                yield return new WaitForSeconds(0.05f);
            }
            obj.transform.position = pos;
        }
    }
}
