using System;
using System.Runtime.Serialization;
using System.Collections;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandEnd")]
    public class CommandEnd : Command
    {
        [DataMember] TSScript parent;

        public CommandEnd(TSScript parent)
        {
            this.parent = parent;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnEnd(parent);
        }
    }
}
