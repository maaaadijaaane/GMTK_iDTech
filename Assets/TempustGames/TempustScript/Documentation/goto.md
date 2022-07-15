# The "goto" Command
The goto command stops the current region and executes the specified one. This can be used to organize large amounts of text, create loops, and merge branches of dialogue. For more information on regions, see [Regions](region.md).

**Usage**

    goto region_name

## Example

    say["none"] "There are instructions on the back."

    region instruction
    say
    {
        "To use, hold Y.
        Slowly spin the left stick in a circle...
        Then release Y for awesome damage!"

        ask "Read instructions again?"
        {
            opt "Yes"
            {
                goto instruction
            }
            opt "No"
            {
                end
            }
        }
    }
    endregion