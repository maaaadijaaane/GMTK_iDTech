using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandPlaySound")]
    public class CommandPlaySound : Command
    {
        [DataMember] public TSScript script { get; private set; }
        [DataMember] public string soundName { get; private set; }

        public CommandPlaySound(TSScript script, string soundName)
        {
            this.script = script;
            this.soundName = soundName;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnPlaySound(script, soundName);
        }
    }
}
