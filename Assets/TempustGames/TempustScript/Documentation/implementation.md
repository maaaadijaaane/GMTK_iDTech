# Customizing Unity Implementation

While Tempust Script can provide many useful features as is, it becomes much more useful as it is customized to your individual project. Some commands provide no default implementation, and must be implemented to be used.

## Creating a Custom TSManager

The TSManager class contains all of the in-game functionality for commands. To customize commands, create a new c# script that inherits from TSManager.

    using TempustScript;
    public class CustomTSManager : TSManager {}

From here, you can customize the functionality of a command by overriding specific methods. These methods have the return type IEnumerator and are used in unity coroutines. They will be named something like "OnCommandName()". For example, to implement the "walk" command:

    public override IEnumerator OnWalk(PCScript script, string obj, Vector3 toPos)
    {
        yield return null;
    }

## Getting Objects from the Script

Some commands have a string argument called "obj". To retrieve the Unity GameObject from the object argument, use the following code:

    script.GetObject(obj);

## Other Override Methods

In addition to command methods, there are other useful overrides available:

| Name | Return Type | Notes |
| ---- | ----------- | ----- |
| OnScriptStart(TSScript script) | void | Called just before the script *script* begins execution. |
| OnScriptEnd(TSScript script) | void | Called just after the script *script* finishes execution. |
| CheckInventory(TSScript script, string item, int amount, bool negate) | bool | Implementation for the checkinv conditional statement. |
| GetSavePath() | string | Returns the game save location. Useful for multiple save files. | 