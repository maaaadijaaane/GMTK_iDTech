# Ending a Script
The "end" command stops the execution of the current script and closes the textbox.
## Example

    region init
        check local moved
        {
            setpos 1 2 2
        }
    endregion

    checknot global has_sword
    {
        say["Guard"] "I can't let you through without a sword."
        end
    }

    say["Guard"] "You have a sword? You should be safe out there."
    walk west 1
    face south
    setflag local moved true
    say "I wish you luck!"
    disable obj