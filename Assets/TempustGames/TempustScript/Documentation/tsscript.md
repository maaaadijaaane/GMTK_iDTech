# TSScript files

When compiled, a .tmpst file is made into a c# TSScript object. These objects contain information about the commands, blocks, regions, and objects needed for Unity. The TSScript class has methods for accessing this information during gameplay.

## Public Properties
| Name | Type | Notes |
| ---- | ----------- | ----- |
| holder | MonoBehaviour | The behaviour that holds the script and runs the coroutine. |
| isRunning | bool | Returns whether the script is currently running. |
| objects | List<string> | The list of objects defined in the script. Used to get the objects to assign in the script holder. |

## Public Methods:

| Name | Return Type | Notes |
| ---- | ----------- | ----- |
| AddObject(string obj) | void | Adds the string *obj* to the list of objects requested from the script holder. This is called automatically when scripts are compiled. |
| AddRegion(Region region) | void | Adds the given region to the script. This throws an error if there is already a script with the same name. |
| Assign Objects(GameObject holder, List<GameObject> objects) | void | This assigns the game objects for each object in the object list and assigns *holder* as the "obj" . It is used to assign the objects from the ScriptHolder to the script. This should generally not be used. |
| ExecuteRegion(Region region) | void | Executes the given region. This should be used over GetRegion().Execute(), as it prevents problems from the script executing multiple times and allows StopExecution() to work properly. |
| GetGlobalFlag(string flag) | bool | Gets the global flag with the given name. |
| GetLocalFlag(string flag) | bool | Gets the local flag with the given name. |
| GetObject(string obj) | GameObject | Gets the object named obj assigned from the script holder component. |
| SetGlobalFlag(string flag, bool value) | void | Sets the global flag *flag* to *value*. If the flag is not in the dictionary, it is added. |
| SetGlobalReference(Dictionary<string, bool> flags) | void | Sets the reference to the global flag dictionary. |
| SetLocalFlag(string flag, bool value) | void | Sets the local flag *flag* to *value*. If the flag is not in the dictionary, it is added. |
| SetLocalReference(Dictionary<string, bool> flags) | void | Sets the reference to the local flag dictionary. |
| SetPlayer(GameObject plr) | void | Sets the scripts "plr" object to the given game object. |
| StopExecution() | void | Stops the script execution and calls TSManager.OnScriptEnd(). |