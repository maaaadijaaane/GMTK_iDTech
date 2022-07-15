# Commands without Default Implementation

Some commands don't have default implementation. If wanted, these commands can be implemented in a [custom TSManager.](implementation.md). Some features are not implemented currently, but will be in the future.

## "bgm"

The "bgm" command controls the game's background music. This is overridden in TSManager.OnBGM(). Support for this command will be improved in future updates.

**Usage**

    bgm [options]

### Options

    fadein [trackname]
    fadeout
    hardin [trackname]
    hardout

## "give"

The "give" command gives the specified amount of an item. Excluding an amount will give 1. The item id is given as a string to allow for flexibility in code.

**Usage**:

    give item-id [amount]

## "playsound"

The "playsound" command is used to play sound effects during conversations. This is overridden in TSManager.OnPlaySound(). Support for this command will be improved in future updates.

**Usage**

    playsound sound_name

## "portrait"

The "portrait" command is designed to change the images displayed when characters are talking, such as in many JRPGs. This is overridden in TSManager.OnPortrait. Support for this command will be improved in future updates.

**Usage**

    portrait left image_name
    portrait right image_name