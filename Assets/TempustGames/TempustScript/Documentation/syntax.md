# Language Overview

Tempust script is a scripting language designed for Unity. Scripts (.tmpst) are encrypted and compiled into .bytes files to be used in Unity as TextAssets.

## What can .tmpst files do?
The main purpose of .tmpst files is to create branching dialogue trees. Tempust Script also supports setting and checking flags, giving items, and moving characters. More commands will be added in future updates.

It is important to note that .tmpst files are just a way of storing data. When compiled and added to a unity project, they provide an easy way to organize and control the execution of code. You can use the included Unity implementation, or customize the scripting commands to execute your own code (See [Customizing Unity Implementation](implementation.md)).

## Syntax
Tempust script is composed of three parts: regions, blocks, and commands.

### Regions
Regions are a way of organizing groups of commands and blocks to prevent duplicating code. (See [Regions](region.md))

### Commands
Commands are single lines consisting of a command name and parameters. These are tied to methods in Unity to execute game code. See the list of included commands below.

### Blocks
Blocks provide ways for the script execution to change based on player progress or choice. They begin with a line defining the type of block, followed by commands surrounded in braces:

    block-type arguments
    {
        commands
    }

For more informatino, see the list of included blocks below.

## Compiling Scripts
To compile scripts, open the Tempust Script window in Unity from Window > Tempust Script. Set the scripting directory to the root folder containing the scripts to compile, then click the "compile" button. This will generate .bytes files in the TempustScript/Compiled folder.

## Setting Up Tempust Script
To use Tempust Script in a scene, first create a manager game object with the TSManager and GameStateManager components. To add a script to an object, add a ScriptHolder component, then assign the script from the inspector.

The default implementation for dialogue also requires a TextboxController game object. If you are not customizing the say commands

## Included Blocks
* [ask](ask.md)
* [if](conditional.md)
* [opt](ask.md)
* [say](say.md)

## Included Commands
* [bgm](unimplemented.md)
* [closebox]()
* [enable / disable](enable.md)
* [end](end.md)
* [give](unimplemented.md)
* [goto](goto.md)
* [playsound](unimplemented.md)
* [portrait](unimplemented.md)
* [say](say.md)
* [setflag](setflag.md)
* [setpos](setpos.md)
* [walk / run](move.md)
* [wait](wait.md)