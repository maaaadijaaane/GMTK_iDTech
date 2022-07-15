using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TempustScript
{
    [RequireComponent(typeof(ScriptHolder))]
    public class InteractScript: MonoBehaviour, TSInteractable
    {
        private ScriptHolder scriptHolder;
        void Start()
        {
            scriptHolder = GetComponent<ScriptHolder>();
        }

        public void OnInteract(GameObject interactor)
        {
            scriptHolder.Execute(interactor);
        }
    }
}