using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TempustScript.Commands;
using TempustScript.InterpreterException;

namespace TempustScript
{
    public class CommandFactory
    {
        /**
         * Create a MovementCommand object from the given line and with the given parent.
         */

        public static Command MakeCommand(TSScript parent, string line)
        {
            //Decide which command to make, then redirect
            string keyword = line.Trim().Split(new char[] { ' ', '[' }, 2)[0];

            switch (keyword)
            {
                case "say":
                    return MakeSayCommand(parent, line);
                case "end":
                    return MakeEndCommand(parent, line);
                case "goto":
                    return MakeGotoCommand(parent, line);
                case "setflag":
                    return MakeSetFlagCommand(parent, line);
                case "face":
                    return MakeFaceCommand(parent, line);
                case "give":
                    return MakeGiveCommand(parent, line);
                case "closebox":
                    return MakeCloseBoxCommand(parent);
                case "setpos":
                    return MakeSetPosCommand(parent, line);
                case "walk":
                case "run":
                    return MakeMovementCommand(parent, line);
                case "enable":
                case "disable":
                    return MakeEnableCommand(parent, line);
                case "wait":
                    return MakeWaitCommand(parent, line);
                case "playsound":
                    return MakePlaySoundCommand(parent, line);
                case "bgm":
                    return MakeBGMCommand(parent, line);
                case "portrait":
                    return MakePortraitCommand(parent, line);
            }
            throw new InvalidCommandException("Tempust Script Error: Invalid command '" + keyword + "'");
        }

        public static Command MakeEnableCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(' ');
            bool value;
            string obj;
            if (splitLine[0].Equals("enable"))
            {
                value = true;
            }
            else 
            {
                value = false;
            }

            if (splitLine.Length > 1)
                obj = splitLine[1];
            else
                obj = "obj";
            return new CommandEnable(parent, obj, value);
        }
        public static Command MakeCloseBoxCommand(TSScript parent)
        {
            return new CommandCloseBox(parent);
        }
        public static Command MakeSayCommand(TSScript parent, string line)
        {
            Match speakerMatch = Regex.Match(line, "\\[\"[\\w ']+\"\\]");

            bool hasSpeaker = speakerMatch.Success;

            string message;

            if (hasSpeaker)
            {
                int startIndex = line.IndexOf("]") + 3;
                message = line.Substring(startIndex, line.Length - startIndex - 1);

                return new CommandSay(parent, speakerMatch.Value.Substring(2, speakerMatch.Value.Length - 4), message);
            }
            else
            {
                message = line.Trim().Split(" ".ToCharArray(), 2)[1].Trim();

                return new CommandSay(parent, message.Substring(1, message.Length - 2));
            }
        }
        public static Command MakeGotoCommand(TSScript parent, string line)
        {
            return new CommandGoto(parent, line.Trim().Split(" ".ToCharArray(), 2)[1].Trim());
        }
        public static Command MakeSetFlagCommand(TSScript parent, string line)
        {
            bool isGlobal;
            switch (line.Trim().Split(' ')[1].Trim())
            {
                case "local":
                    isGlobal = false;
                    break;
                case "global":
                    isGlobal = true;
                    break;
                default:
                    throw new InvalidCommandException("Tempust Script Error: setflag command must specify global or local flag");
            }

            bool value;

            switch (line.Trim().Split(' ')[3].Trim())
            {
                case "true":
                    value = true;
                    break;
                case "false":
                    value = false;
                    break;
                default:
                    throw new InvalidCommandException("Tempust Script Error: setflag command must specify a true or false value");
            }

            return new CommandSetFlag(parent, isGlobal, line.Trim().Split(' ')[2].Trim(), value); ;
        }
        public static Command MakeFaceCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), 4, StringSplitOptions.RemoveEmptyEntries);
            string obj = "obj";

            if (splitLine[2].Equals("to"))
            {
                obj = splitLine[1];
            }
            else if (!splitLine[1].Equals("to"))
            {
                throw new InvalidCommandException("Tempust Script Error: Invalid face arguments");
            }
            ObjectCoordinate coords = ParseObjectCoords(parent, line.Substring(line.IndexOf(" to ") + 4).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            return new CommandFace(parent, obj, coords);
        }
        public static Command MakeSetPosCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), 4, StringSplitOptions.RemoveEmptyEntries);
            string obj = "obj";

            if (splitLine[2].Equals("to"))
            {
                obj = splitLine[1];
            }
            else if (!splitLine[1].Equals("to"))
            {
                throw new InvalidCommandException("Tempust Script Error: Invalid setpos arguments");
            }
            ObjectCoordinate coords = ParseObjectCoords(parent, line.Substring(line.IndexOf(" to ") + 4).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            return new CommandSetPos(parent, obj, coords);
        }
        public static Command MakeEndCommand(TSScript parent, string line)
        {
            return new CommandEnd(parent);
        }
        public static Command MakeGiveCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Split(' ');
            return new CommandGive(parent, splitLine[1], int.Parse(splitLine[2]));
        }
        public static Command MakeMovementCommand(TSScript parent, string line)
        {
            CommandMovement.Action action;

            string[] splitLine = line.Trim().Split(" ".ToCharArray(), 4);
            string obj = "obj";

            if (splitLine[0].Equals("run"))
            {
                action = CommandMovement.Action.RUN;
            }
            else
            {
                action = CommandMovement.Action.WALK;
            }

            if (splitLine[2].Equals("to"))
            {
                obj = splitLine[1];
            }
            else if (!splitLine[1].Equals("to"))
            {
                throw new InvalidCommandException("Tempust Script Error: Invalid " + action.ToString().ToLower() + " arguments");
            }

            ObjectCoordinate coords = ParseObjectCoords(parent, line.Substring(line.IndexOf(" to ") + 4).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            return new CommandMovement(parent, action, obj, coords);
        }
        public static Command MakeWaitCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), 2);
            try
            {
                float time = float.Parse(splitLine[1]);
                return new CommandWait(parent, time);
            }
            catch (FormatException)
            {
                throw new InvalidCommandException("Tempust Script Error: Invalid wait time");
            }
        }
        public static Command MakePlaySoundCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new CommandPlaySound(parent, splitLine[1]);
        }
        public static Command MakeBGMCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            bool fade;
            bool musicIn;

            switch (splitLine[1])
            {
                case "fadein":
                    fade = true;
                    musicIn = true;
                    break;
                case "fadeout":
                    fade = false;
                    musicIn = true;
                    break;
                case "in":
                    fade = false;
                    musicIn = true;
                    break;
                case "out":
                    fade = false;
                    musicIn = false;
                    break;
                default:
                    throw new InvalidCommandException("Tempust Script Error: Invalid BGM parameter. Must be followed by 'in', 'out', 'fadein', or 'fadeout'.");
            }

            if (musicIn && splitLine.Length <3)
            {
                throw new InvalidCommandException("Tempust Script Error: No trackname given for BGM command");
            }

            return new CommandBGM(parent, musicIn ? splitLine[2] : "", musicIn, fade);
        }
        public static Command MakePortraitCommand(TSScript parent, string line)
        {
            string[] splitLine = line.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            bool left;

            switch (splitLine[1])
            {
                case "left":
                    left = true;
                    break;
                case "right":
                    left = false;
                    break;
                default:
                    throw new InvalidCommandException("Tempust Script Error: Invalid portrait parameter. Must be followed by 'left' or 'right'.");
            }

            if (splitLine.Length < 3)
            {
                throw new InvalidCommandException("Tempust Script Error: No portrait name given for BGM command");
            }

            return new CommandPortrait(parent, left, splitLine[2]);
        }
        public static ObjectCoordinate ParseObjectCoords(TSScript parent, string[] args)
        {
            //Get the object 
            ObjectCoordinate coords = new ObjectCoordinate(parent);
            List<string> objectParams = new List<string>();

            foreach (string param in args)
            {
                if (param.StartsWith("x:") || param.StartsWith("y:") || param.StartsWith("z:"))
                {
                    float value = 0;
                    bool isRelative = false;
                    string relativeObject = "";

                    //if no object
                    if (!float.TryParse(param.Substring(2), out value))
                    {
                        isRelative = true;

                        int valueIndex = param.LastIndexOfAny("+-".ToCharArray());
                        if (valueIndex >= 0)
                        {
                            relativeObject = param.Substring(2, valueIndex - 2);
                            if (!float.TryParse(param.Substring(valueIndex), out value))
                                throw new InvalidCommandException("Tempust Script Error: Invalid setpos coordinate");
                        }
                        else
                        {
                            value = 0;
                            relativeObject = param.Substring(2);
                        }

                    }

                    if (param[0] == 'x')
                    {
                        coords.SetXCoord(value, isRelative, relativeObject);
                    }
                    else if (param[0] == 'y')
                    {
                        coords.SetYCoord(value, isRelative, relativeObject);
                    }
                    else if (param[0] == 'z')
                    {
                        coords.SetZCoord(value, isRelative, relativeObject);
                    }
                }
                else
                {
                    objectParams.Add(param);
                }
            }

            if (objectParams.Count > 1)
            {
                throw new InvalidCommandException("Tempust Script Error: Invalid coordinates");
            }
            else if (objectParams.Count == 1) //If there is an object as a coordinate, set the coordinate to that object
            {
                coords.SetXCoord(0, true, objectParams[0]);
                coords.SetYCoord(0, true, objectParams[0]);
                coords.SetZCoord(0, true, objectParams[0]);
            }
            return coords;
        }
    }
}