# The "setpos" Command

The "setpos" command is used to instantly set the position of an object. This is most useful in the [initialization region](regions.md), being used to set the position of an object based on flags when the scene is loaded. To make objects move over time, see [here.](move.md)

**Usage**

    setpos [object] to [coords]

The object argument can be omitted, and it will target "obj". For information on objects, see [here.](objects.md)

For information on coordinates, see [here.](objectcoords.md)

## Example

    move to y:obj-1
    say["???"] "Please excuse me..."
    move plr to x:plr+1
    move to y:obj-10
    disable obj

## Unity Implementation

To customize the implementation, override the OnSetPos() method in the TSManager. By default this simply sets the transform position, so it generally doesn't need overridden. However in some cases, such as when using a CharacterController, you may need additional code.