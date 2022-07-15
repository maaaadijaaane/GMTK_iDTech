using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandBGM")]
    public class CommandBGM : Command
    {
        [DataMember] public TSScript script { get; private set; }
        [DataMember] public string trackName { get; private set; }
        [DataMember] public bool musicIn { get; private set; }
        [DataMember] public bool fade { get; private set; }

        public CommandBGM(TSScript script, string trackName, bool musicIn, bool fade)
        {
            this.script = script;
            this.trackName = trackName;
            this.musicIn = musicIn;
            this.fade = fade;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnBGM(script, trackName, musicIn, fade);
        }
    }
}
