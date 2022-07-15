using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandSetFlag")]
    public class CommandSetFlag : Command
    {
        [DataMember] public string flagName { get; private set; }
        [DataMember] public bool value { get; private set; }
        [DataMember] public bool isGlobal { get; private set; }
        [DataMember] public TSScript parent { get; private set; }

        public CommandSetFlag(TSScript parent, bool isGlobal, string name, bool value)
        {
            this.flagName = name;
            this.value = value;
            this.parent = parent;
            this.isGlobal = isGlobal;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnSetFlag(parent, isGlobal, flagName, value);
        }
    }
}
