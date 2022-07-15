using System;
using System.Runtime.Serialization;
using System.Collections;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandCloseBox")]
    public class CommandCloseBox : Command
    {
        [DataMember] public TSScript parent { get; private set;}
        public CommandCloseBox(TSScript parent)
        {
            this.parent = parent;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnCloseBox(parent);
        }
    }
}
