using System.Collections.Generic;
using System.Text.RegularExpressions;
using TempustScript.Blocks;
using TempustScript.InterpreterException;

namespace TempustScript
{
    public class BlockFactory
    {
        public static AskBlock MakeAskBlock(TSScript parent, List<string> lines)
        {
            MatchCollection matches = Regex.Matches(lines[0], "\"[\\w ,!?.<>'/]*\"");

            string speaker;
            string question;
            
            if (matches.Count == 1)
            {
                question = matches[0].Value.Substring(1, matches[0].Length - 2);
                speaker = "";
            }
            else
            {
                question = matches[1].Value.Substring(1, matches[1].Length - 2);
                speaker = matches[0].Value.Substring(1, matches[0].Length - 2);
            }

            //Find the option blocks
            List<OptionBlock> options = new List<OptionBlock>();
            try
            {
                foreach (ScriptElement element in Interpreter.ReadLines(parent, lines.GetRange(2, lines.Count - 3)))
                {
                    options.Add((OptionBlock)element);
                }
            }
            catch
            {
                throw new InvalidBlockException("Element other than option found in ask block");
            }

            return new AskBlock(parent, speaker, question, options);
        }

        public static OptionBlock MakeOptionBlock(TSScript parent, List<string> lines)
        {
            string optionText = lines[0].Trim().Split(" ".ToCharArray(), 2)[1].Trim().Replace("\"", "");

            //Add code to allow adding conditions to options

            return new OptionBlock(parent, optionText, Interpreter.ReadLines(parent, lines.GetRange(2, lines.Count - 3)));
        }

        public static ConditionalBlock MakeConditionalBlock(TSScript parent, List<string> lines)
        {
            string condition = lines[0];
            int elseIndex = lines.FindIndex(x => x.ToString().Contains("else"));
            if (elseIndex != -1)
            {
                return new ConditionalBlock(parent, condition, Interpreter.ReadLines(parent, lines.GetRange(2, elseIndex - 3)), Interpreter.ReadLines(parent, lines.GetRange(elseIndex + 2, lines.Count - 3 - elseIndex)));
            }
            else
            {
                return new ConditionalBlock(parent, condition, Interpreter.ReadLines(parent, lines.GetRange(2, lines.Count - 3)));
            }
        }

        public static TextBlock MakeTextBlock(TSScript parent, List<string> lines)
        {
            MatchCollection matches = Regex.Matches(lines[0], "\"[\\w ,!?.<>'/]*\"");
            string speaker;
            if (matches.Count > 0)
            {
                speaker = matches[0].Value.Substring(1, matches[0].Length - 2);
            }
            else
            {
                speaker = "";
            }

            List<string> sayLines = new List<string>();
            for (int i = 2; i < lines.Count - 1; i++)
            {
                sayLines.Add(lines[i].Trim().Replace("\"", ""));
            }

            return new TextBlock(parent, speaker, sayLines);
        }

        /**
         * Turn the given lines of tempust script into a code block based on the header at lines[0].
         */
        public static ScriptBlock MakeBlock(TSScript parent, List<string> lines)
        {
            string keyword = lines[0].Trim().Split(new char[] { ' ', '[' }, 2)[0];
            ScriptBlock block = null;

            switch (keyword)
            {
                case "ask":
                    block = MakeAskBlock(parent, lines);
                    break;
                case "opt":
                    block = MakeOptionBlock(parent, lines);
                    break;
                case "if":
                    lines[0] = lines[0].Substring(3);
                    block = MakeConditionalBlock(parent, lines);
                    break;
                case "say":
                    block = MakeTextBlock(parent, lines);
                    break;
                default:
                    throw new InvalidBlockException(string.Format("Invalid block keyword \"{0}\"", keyword));

            }

            return block;
        }
    }
}