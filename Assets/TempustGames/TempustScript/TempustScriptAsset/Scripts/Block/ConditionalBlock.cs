using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using TempustScript.InterpreterException;
using UnityEngine;

namespace TempustScript.Blocks
{
    [DataContract(Name="ConditionalBlock")]
    public class ConditionalBlock : ScriptBlock
    {
        [DataMember] private List<ScriptElement> elements;
        [DataMember] private List<ScriptElement> elseBlock;
        [DataMember] private string condition;
        [DataMember] private TSScript parent;

        public ConditionalBlock(TSScript parent, string condition, List<ScriptElement> elements) : this(parent, condition, elements, new List<ScriptElement>()) { }
        public ConditionalBlock(TSScript parent, string condition, List<ScriptElement> elements, List<ScriptElement> elseBlock)
        {
            this.condition = condition;
            this.elements = elements;
            this.elseBlock = elseBlock;
            this.parent = parent;
        }
        private FlagCondition MakeFlagCondition(List<string> args, bool negate)
        {
            bool global = false;

            switch (args[1])
            {
                case "global":
                    global = true;
                    break;
                case "local":
                    global = false;
                    break;
                default:
                    throw new InvalidBlockException("Tempust Script Error: \"checkflag\" conditions must be followed by either \"global\" or \"local\"");
            }
            return new FlagCondition(parent, global, args[2], negate);
        }

        private InvCondition MakeInvCondition(List<string> keys, bool negate)
        {
            try
            {
                if (keys.Count > 2)
                {
                    float amount = float.Parse(keys[2]);
                    return new InvCondition(parent, keys[1], amount, negate);
                }
                else
                {
                    return new InvCondition(parent, keys[1], 1, negate);
                }
            }
            catch
            {
                throw new InvalidBlockException("Tempust Script Error (checkinv): Unable to parse hasinv condition.");
            }
        }

        private bool ConditionMet()
        {
            int curWordIndex = 0;
            condition = condition.Replace("(", "[ ");
            condition = condition.Replace(")", " ]");
            string[] conditionWords = condition.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            Stack<string> operators = new Stack<string>();
            Stack<Condition> operands = new Stack<Condition>();

            while (curWordIndex < conditionWords.Length)
            {
                string item = conditionWords[curWordIndex];
                if (item.StartsWith("not") || item.StartsWith("checkinv") || item.StartsWith("checkflag"))
                {
                    List<string> args = new List<string>();
                    while (curWordIndex < conditionWords.Length && !(conditionWords[curWordIndex].Equals("and") || conditionWords[curWordIndex].Equals("or") || conditionWords[curWordIndex].Equals("]")))
                    {
                        args.Add(conditionWords[curWordIndex]);
                        curWordIndex++;
                    }

                    bool negate = item.Equals("not");
                    if (negate)
                        args.RemoveAt(0);
                    
                    if (args[0].Equals("checkinv"))
                    {
                        operands.Push(MakeInvCondition(args, negate));
                    }
                    else if (args[0].Equals("checkflag"))
                    {
                        operands.Push(MakeFlagCondition(args, negate));
                    }
                    curWordIndex--;
                }
                else if (item.Trim().Equals("["))
                {
                    operators.Push(item);
                }
                else if (item.Trim().Equals("]"))
                {
                    //When there is a closing parenthesis, evaluate everything back until there is an opening parenthesis
                    while (!operators.Peek().Equals("["))
                    {
                        bool result = Operate(operands, operators);
                        operands.Push(new Condition(result));
                    }
                    //Remove the opening parenthesis
                    operators.Pop();
                }
                // current character is operator
                else if (item.Equals("or") || item.Equals("and"))
                {
                    while (operators.Count > 0 && !(operators.Peek().Equals("[") || operators.Peek().Equals("]")))
                    {
                        bool result = Operate(operands, operators);
                        operands.Push(new Condition(result));   //push result back to stack
                    }
                    operators.Push(item);   //push the current operator to stack
                }
                curWordIndex++;
            }
            while (operators.Count > 0)
            {
                bool result = Operate(operands, operators);
                operands.Push(new Condition(result));   //push final result back to stack
            }
            return operands.Pop().Evaluate();
        }

        private bool Operate(Stack<Condition> operands, Stack<string> operators)
        {
            Condition a = operands.Pop();
            Condition b = operands.Pop();
            string op = operators.Pop();

            switch (op)
            {
                case "and":
                    return a.Evaluate() && b.Evaluate();
                case "or":
                    return a.Evaluate() || b.Evaluate();
                default:
                    throw new InvalidBlockException("Bad condition operator");
            }
        }

        public override IEnumerator Execute()
        {
            bool conditionMet = ConditionMet();

            List<ScriptElement> block = conditionMet ? elements : elseBlock;
            
            foreach(ScriptElement element in block)
            {
                yield return element.Execute();
            }
        }

        private class Condition
        {
            private bool value;

            public Condition() { }
            public Condition(bool value)
            {
                this.value = value;
            }

            public virtual bool Evaluate()
            {
                return value;
            }
        }

        private class FlagCondition : Condition
        {
            private bool negate;
            private string flag;

            //true=global, false=local
            private bool global;
            private TSScript script;

            public FlagCondition(TSScript script, bool global, string flag, bool negate = false)
            {
                this.negate = negate;
                this.flag = flag;
                this.global = global;
                this.script = script;
            }

            public override bool Evaluate()
            {
                if (!global)
                {
                    return negate != script.GetLocalFlag(flag);
                }
                else
                {
                    return negate != script.GetGlobalFlag(flag);
                }
            }
        }
        private class InvCondition : Condition
        {
            private bool negate;
            private string item;
            private float amount;
            private TSScript script;

            public InvCondition(TSScript script, string item, float amount, bool negate = false)
            {
                this.negate = negate;
                this.item = item;
                this.amount = amount;
                this.script = script;
            }

            public override bool Evaluate()
            {
                return negate != TSManager.singleton.CheckInventory(script, item, amount, negate);
            }
        }
    }
}
