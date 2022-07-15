using System.Collections;
using System.Runtime.Serialization;

namespace TempustScript.Blocks
{
    [DataContract]
    public abstract class ScriptBlock : ScriptElement
    {
        public abstract IEnumerator Execute();
    }
}
