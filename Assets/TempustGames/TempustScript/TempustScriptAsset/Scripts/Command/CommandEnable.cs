using System.Runtime.Serialization;
using System.Collections;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandEnable")]
    public class CommandEnable : Command
    {
        [DataMember] private bool value;
        [DataMember] private TSScript parent;
        [DataMember] private string obj;

        public CommandEnable(TSScript parent, string obj, bool value)
        {
            this.parent = parent;
            this.obj = obj;
            this.value = value;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnEnableDisable(parent, obj, value);
        }
    }
}
