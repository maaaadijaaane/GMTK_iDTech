using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TempustScript.Blocks
{
    [DataContract(Name="AskBlock")]
    public class AskBlock : ScriptBlock
    {
        [DataMember] private string speaker;
        [DataMember] private string question;
        [DataMember] private List<OptionBlock> options;
        [DataMember] private TSScript parent;

        public AskBlock(TSScript parent, string speaker, string question, List<OptionBlock> options)
        {
            this.parent = parent;
            this.speaker = speaker;
            this.question = question;
            this.options = options;
        }

        public override IEnumerator Execute()
        {
            List<string> optionLabels = new List<string>();
            foreach (OptionBlock option in options)
            {
                optionLabels.Add(option.GetOption());
            }
            
            yield return TSManager.singleton.OnAsk(parent, speaker, question, optionLabels);
            yield return options[TSManager.singleton.GetAskResult()].Execute();
        }
    }
}