using System.Collections;

namespace TempustScript
{
    public interface ScriptElement
    {
        /**
         * Run the script element.
         */
        public IEnumerator Execute();
    }
}
