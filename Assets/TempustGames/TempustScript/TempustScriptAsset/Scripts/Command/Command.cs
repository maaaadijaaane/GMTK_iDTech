using System.Runtime.Serialization;
using System.Collections;

namespace TempustScript.Commands
{
    [DataContract]
    public abstract class Command : ScriptElement
    {
        public abstract IEnumerator Execute();
    }
}
