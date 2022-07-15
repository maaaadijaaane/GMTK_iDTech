using System;
using UnityEngine;
using TempustScript.Blocks;
using System.Collections.Generic;

namespace TempustScript.Shortcuts
{
    /// <summary>
    /// The TSDialogue class is a shortcut to writing a script with a single say block. It dynamically creates an instance of TSScript using the given speaker and message.
    /// </summary>
    public class TSDialogue : ScriptHolder
    {
        [SerializeField] private string speaker;
        [SerializeField] private string message;
        
        private new void Start()
        {
            script = new TSScript();
            TextBlock text = new TextBlock(script, speaker, new List<string>(message.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)));
            List<ScriptElement> elements = new List<ScriptElement>();
            elements.Add(text);
            elements.Add(new Commands.CommandEnd(script));
            script.AddRegion(new Region(script, "default", elements));
            script.AssignObjects(gameObject, null);
            script.holder = this;
        }
    }
}