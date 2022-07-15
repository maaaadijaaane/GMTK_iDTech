using System;
using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandFace")]
    public class CommandFace : Command
    {
        [DataMember] public TSScript parent { get; private set; }
        [DataMember] public string obj { get; private set; }

        [DataMember] public ObjectCoordinate coords { get; private set; }

        public CommandFace(TSScript parent, string obj, ObjectCoordinate coords)
        {
            this.parent = parent;
            this.obj = obj;
            this.coords = coords;
        }

        public override IEnumerator Execute()
        {
            yield return TSManager.singleton.OnFace(parent, obj, coords.GetVector(obj).normalized);
        }
    }
}
