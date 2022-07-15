using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TempustScript.Blocks
{
    [DataContract(Name = "TextBlock")]
    public class TextBlock : ScriptBlock
    {
        [DataMember] private List<string> lines;
        [DataMember] private TSScript parent;
        [DataMember] private string speaker;
        private bool shouldContinue;

        public TextBlock(TSScript parent, string speaker, List<string> textLines)
        {
            this.parent = parent;
            lines = textLines;
            this.speaker = speaker;
        }
        public override IEnumerator Execute()
        {
            foreach (string line in lines)
            {
                yield return TextboxController.singleton.Type(speaker, line);
            }
        }
    }
}
