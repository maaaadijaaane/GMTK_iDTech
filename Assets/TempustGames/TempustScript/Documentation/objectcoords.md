# Object Coordinates
Both the "move" and "setpos" commands require the word "to" followed by a position argument. There are two ways to give a position for these commands:

## Use an object
You can directly use the name of an object (see [objects](objects.md)) to use the coordinates of that objects transform in unity.
    
    def start_pos
    setpos to start_pos

## Set individual coordinates
You can also set individual coordinates, either relative to an object, or absolute, separated by spaces. Any coordinates not set will remain unchanged.
### Absolute Coordinates

    setpos to x:10 y:-50 z:52
    setpos to y:10 //set one coordinate

### Relative coordinates
You can set individual coordinates as relative to an object, using something like "x:obj_name+1" or "y:obj_name-4"
    
    setpos plr to y:plr+1
    move to x:obj-2 z:obj+1