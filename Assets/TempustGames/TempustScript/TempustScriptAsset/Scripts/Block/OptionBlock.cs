using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TempustScript.Blocks
{
    [DataContract(Name="OptionBlock")]
    public class OptionBlock : ScriptBlock
    {
        [DataMember] private string optionText;
        [DataMember] private List<ScriptElement> elements;
        [DataMember] private TSScript parent;
        public OptionBlock(TSScript parent, string optionText, List<ScriptElement> elements)
        {
            this.parent = parent;
            this.optionText = optionText;
            this.elements = elements;
        }

        public string GetOption()
        {
            return optionText;
        }

        public override IEnumerator Execute()
        {
            foreach (ScriptElement element in elements)
            {
                yield return element.Execute();
            }
        }
    }
}
