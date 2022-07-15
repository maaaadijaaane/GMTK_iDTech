using System;
using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandMovement")]
    public class CommandMovement : Command
    {
        public enum Action { WALK, RUN }
        [DataMember] private string obj;
        [DataMember] private Action action;
        [DataMember] private ObjectCoordinate coords;
        [DataMember] private TSScript parent;

        public CommandMovement(TSScript parent, Action action, string obj, ObjectCoordinate coords)
        {
            this.parent = parent;
            this.action = action;
            this.obj = obj;
            this.coords = coords;
        }

        public override IEnumerator Execute()
        {
            if (action == Action.WALK)
            {
                yield return TSManager.singleton.OnWalk(parent, obj, coords.GetVector(obj, true));
            }
            else if (action == Action.RUN)
            {
                yield return TSManager.singleton.OnRun(parent, obj, coords.GetVector(obj, true));
            }
        }
    }
}
