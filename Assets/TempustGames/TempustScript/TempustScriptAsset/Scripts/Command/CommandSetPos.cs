using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace TempustScript.Commands
{
    [DataContract(Name = "CommandSetPos")]
    public class CommandSetPos : Command
    {
        [DataMember] public TSScript parent { get; private set; }
        [DataMember] public string obj { get; private set; }
        [DataMember] private ObjectCoordinate coords;

        public CommandSetPos(TSScript parent, string obj, ObjectCoordinate coords)
        {
            this.parent = parent;
            this.coords = coords;
            this.obj = obj;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnSetPos(parent, obj, coords.GetVector(obj, true));
        }
    }
}
