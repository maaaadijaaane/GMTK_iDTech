# The "enable" and "disable" Commands

The enable and disable commands take a single [object](objects.md) as a parameter, then set the object's active state to true or false.

**Usage**

    enable obj_name
    disable obj_name

## Example

    def barricade

    region init
    if checkflag global road_fixed
    {
        disable barricade
    }
    endregion