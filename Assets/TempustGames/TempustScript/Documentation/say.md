# The "Say" Command and Block
There are two ways to display text in Tempust Script. The first is the say command. This is for single line statements.

**Usage**

    say["speaker"] "text"

Not specifying a speaker will return an empty string, which tells the game to use the last speaker.

## The Say Block
The second way is the say block. Unlike other blocks, this block does not contain other commands. Instead it is a multi-line say, used for large blocks of text. Each new line will require a button press to continue.

**Usage**

    say["speaker"]
    {
        "Text line 1
        Text line 2
        Text line 3"
    }

Quotation marks surround the entire contents of the block.

## The TSDialogue Shortcut
Many times you may want to create scripts that contain nothing but text. To make this easier, TempustScript provides the TSDialogue component. The TSDialogue allows a speaker and message to be assigned in the inspector, and creates a new script at runtime.

## Example (Command)

    say["Receptionist"] "Welcome to the hotel lobby."
    say "Let me know if you need anything."

## Example (Block)

    say["Receptionist"]
    {
        "Welcome to the hotel lobby.
        Let me know if you need anything."
    }