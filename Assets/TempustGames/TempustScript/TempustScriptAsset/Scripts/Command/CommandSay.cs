using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandSay")]
    public class CommandSay : Command
    {
        [DataMember] public string speaker { get; private set; }
        [DataMember] public string message { get; private set; }
        [DataMember] public TSScript parent { get; private set; }
        public CommandSay(TSScript parent, string message)
        {
            this.parent = parent;
            this.speaker = "";
            this.message = message;
        }

        public CommandSay(TSScript parent, string speaker, string message)
        {
            this.parent = parent;
            this.speaker = speaker;
            this.message = message;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnSay(parent, speaker, message);
        }
    }
}
