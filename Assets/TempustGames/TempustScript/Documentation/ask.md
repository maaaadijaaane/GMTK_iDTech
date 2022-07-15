# Asking Questions with the Ask Block
The ask block gives a prompt using the format of the [say] command, then allows the player to select an option. Each option consists of a group of commands to be executed.

**Usage**:

    ask["speaker"] "prompt"
    {
        opt "option text"
        {
            commands to be executed
        }
        opt "option text"
        {
            comands to be executed
        }
    }

An ask block must contain at least one option block.

## Example

    title ask_example
    
    say["Town Child"] "Hey mister! I like your shoes!"
    ask "Can I have your shoes?"
    {
        opt "No!"
        {
            say "You're so mean!"
        }
        opt "Sure!"
        {
            say "I'm only joking, I don't want your gross shoes!"
        }
        opt "Why?"
        {
            say "They look good..."
        }
    }
    end

## Unity Customization
To customize the ask block, there are two methods that can be overriden in the TSManager class. The first is OnAsk(). This is a coroutine that displays the text and yields until the player chooses an option. The second is GetAskResult(). This is called immediately after the completion of OnAsk() and returns the zero-based index of the selected option.