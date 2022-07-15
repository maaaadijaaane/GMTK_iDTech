using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandWait")]
    public class CommandWait : Command
    {
        [DataMember] public TSScript parent { get; private set; }
        [DataMember] public float seconds { get; private set; }

        public CommandWait (TSScript parent, float seconds)
        {
            this.parent = parent;
            this.seconds = seconds;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnWait(parent, seconds);
        }
    }
}