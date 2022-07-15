using System;
using System.Collections.Generic;
using System.Text;
using TempustScript.InterpreterException;

namespace TempustScript
{
    public class RegionFactory
    {
        public static Region MakeRegion(TSScript parent, List<string> lines)
        {
            return new Region(parent, lines[0].Split(" ".ToCharArray(), 2)[1], Interpreter.ReadLines(parent, lines.GetRange(1, lines.Count - 2)));
        }
    }
}