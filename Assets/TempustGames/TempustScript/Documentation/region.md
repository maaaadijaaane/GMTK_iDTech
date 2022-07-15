# Regions
Regions are named groups of blocks and commands that can be executed. Each region is given a name, and can be executed with the [goto](goto.md) command. The names "default" and "init" are reserved for special cases, as explained below.

## The Default Region
By default, all blocks and commands are in a region called "default". Anything not placed inside another region will be included. This region is then executed when the script is run.

### Default Region Example

    title regions_overview
    say "This in the default region!"

    region new_region
    say "This is in a region called new_region."
    endregion

    say "This is back to the default region"
    
## Init Region
The initialization region is executed at Unity's Start(). This is used for setting the state of an object based on flags. For example, moving an npc blocking a door when the player has set a certain flag

### Init Region Example

    region init
        if checkflag local moved
        {
            setpos 1 2 2
        }
    endregion

    if not checkflag global has_sword
    {
        say["Guard"] "I can't let you through without a sword."
        end
    }

    say["Guard"] "You have a sword? You should be safe out there."
    walk to z:obj+1
    face x:obj-1
    setflag local moved true
    say "I wish you luck!"
    disable obj
    end

## Creating Regions
New regions can be created with the "region" and "endregion" commands. All lines after a "region" and before an "endregion" are grouped together. Failing to end a region will cause a compilation error.

**Usage**

    region region_name
    //Region elements go here
    endregion

## Region Execution
Unless specifically stated, only the default region will be executed. To end the current region and execute another, use the [goto](goto.md) command.