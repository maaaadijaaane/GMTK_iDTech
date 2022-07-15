using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandGoto")]
    public class CommandGoto : Command
    {
        [DataMember] public TSScript parent { get; private set; }
        [DataMember] public string region { get; private set; }

        public CommandGoto(TSScript parent, string region)
        {
            this.parent = parent;
            this.region = region;
        }
        public override IEnumerator Execute()
        {
            yield return parent.GetRegion(region).Execute();
        }
    }
}
