using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandPortrait")]
    public class CommandPortrait : Command
    {
        [DataMember] public TSScript parent { get; private set; }
        [DataMember] public string name { get; private set; }
        [DataMember] public bool left { get; private set; }

        public CommandPortrait(TSScript parent, bool left, string imageName)
        {
            this.parent = parent;
            this.left = left;
            this.name = imageName;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnPortrait(parent, left, name);
        }
    }
}
