# Flags and the "setflag" command

Flags are used to save data between script executions and game sessions. They are set using the setflag command, and evaluated in [conditional blocks](conditional.md).

## Scopes
Flags can either be *local* or *global*.

Local flags are specific to a script, and are not needed by other objects. For example, they can be used to check if the player has interacted with an npc before, if a generic chest has been opened, or if an npc has given you materials.

Global flags are accessible by any script. They are for recording more important events, like if a player has completed a quest or dungeon, has received a key item, or completed a conversation needed for story progression.

## Setting flags
To set a flag, use the setflag command. The scope should be either global or local, and the value should be true or false.

**Usage**

    setflag [scope] flag_name

## Example

    if (not checkflag local sample_given)
    {
        say["Merchant"] "Please enjoy this free sample!"
        give potion 1
        say["none"] "You got a potion"
    }
    else
    {
        say["Merchant"] "Enjoy the potion!"
    }