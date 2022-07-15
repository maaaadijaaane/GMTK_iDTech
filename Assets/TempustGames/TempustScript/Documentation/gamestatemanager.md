# The GameStateManager Component

The GameStateManager component is a singleton that keeps track and saves game [flags](setflag.md). Any game using Tempust Script requires a game object with the GameStateManager component to keep track of flags.

While more difficult, it is possible to override the "setflag" command and replace the ScriptHolder component with a custom script to use a different flag saving system.

## Persistent Data

The persistent data dictionary contains the local flags of any object that's state has changed, in all scenes, since the last save. When the game is saved, the persistent data is cleared.

## Public Methods

| Name | Type | Notes |
| ---- | ---- | ----- |
| GetGlobalFlags() | Dictionary<string, bool> | Returns the dictionary mapping global flag names to their values.
| GetLocalFlags() | Dictionary<string, bool> | Returns the dictionary mapping the local flags of the object in the current scene named *objectName* to their values. |
| AddCurSceneToPersistentData() | void | Adds the current scene's local flags to the list of data to save. This is automatically called whenever a flag is set. |
| Save() | void | Writes all persistent data and global flags to save files, then clears the persistent data. |
| ClearSaveData() | void | Deletes all save data files from the save path. |