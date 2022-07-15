# Conditional Blocks
One of the more useful features of Tempust Script, conditional blocks change the conversation based on saved [flags](setflag).

## Conditional statements
In its simplest form, an if block is the word if followed by a conditional statement. There are currently two types of conditional statements in Tempust Script: checkflag and checkinv.

### checkflag

A checkflag statement is made by the word checkflag followed by the scope of the flag, then by the flag's name. If the value of the flag is true, the commands in the block will be executed.

**Usage**

    if checkflag local flag_name
    {
        commands to execute
    }

### checkinv

An checkinv statement checks if the player has certain items in their inventory. It consists of the word checkinv, followed by the item, then, optionally, the amount of the item.

    if checkinv coin 10
    {
        commands to execute
    }

The statement has no default implementation. You must first implement it by overriding CheckInventory() in a [custom TSManager](implementation.md). The item name is passed as a string, but it could be also be used as an item id.

## Boolean Operations

Multiple statements can be altered or combined using the words "and", "or", and "not", as well as parentheses. **Currently the not operator cannot be applied to multiple checks surrounded in parentheses.**

### Example

    checkflag local flag_name and checkflag global other_flag or (not checkinv item and not checkinv other_item)
    {
        commands to execute
    }

## Else

An else block can be added after an if to run only if the check fails.

**Usage**

    if checkflag local flag_name
    {
        run commands if true
    }
    else
    {
        run commands if false
    }

## Example

    if not checkflag local sample_taken
    {
        ask["Merchant"] "Would you like a free sample?"
        {
            opt "Yes"
            {
                give sample 5
                setflag local sample_taken
            }
            opt "No"
            {
                say "Stop by if you change your mind!"
            }
        }
    }
    else
    {
        say["Merchant"] "Enjoy your sample!"
    }