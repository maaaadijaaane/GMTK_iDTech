# Setting up a Project for Tempust Script
This guide will go over how to set up a project to use Tempust Script.

## Installing the Package
After purchasing Tempust Script from the asset store, it can be installed in Unity from the package manager (Window > Package Manager, make sure "My Assets" is selected). The package contains three folders: The asset, a demo scene, and offline documentation.

The package includes a VS Code extension package (.vsix) to make writing scripts easier. To install the package, open the extension menu, select the three dots at the top (Views and More Actions), choose install from vsix, then select the package.

## The Tempust Script Window
The Tempust Script Window can be opened from Window > Tempust Script. It contains a tool for generating encryption keys, the paths to your scripting and compile directories, and the "Compile Scripts" button. Each are explained below.

## Setting up Scripting and Compile Directories
The scripting directory is where you will save the scripts you write. This can be in any folder on your computer, as the plain-text scripts will not be included in the build. The compile directory is the path in the project's "Assets" folder that contains compiled scripts.

* When you click "Compile Scripts", any scripts in the scriptory directory and its subdirectories will compiled and copied to your unity project. Compiled scripts are .bytes files, so they can be easily used as unity TextAssets.

## Replacing the encryption key
In the folder containing the asset, open the file named "TSEncryption.cs". Near the top of the file, note the following line:

    private static string key = ""

To add protection to your files, you will want to update the encryption key. In the Tempust Script Window, click the "generate key" button to generate a new key. Select and copy the key, or press the copy button, then paste the new key in the TSEncryption file.

Changing the encryption key will require scripts to be recompiled and save data deleted.

## Adding a Text Box
The "say" command, "say" block, and "ask" block require a text box. Tempust Script includes a text box prefab that is ready to use.

A custom text box can be made using the TextBoxController component after the fashion of the included one, or a custom TSManager can override the OnSay(), OnAsk(), and GetAskResult() methods to use a different text box.

## The Tempust Script Manager
The last step is to create a GameObject to hold the the manager components. Create a new empty gameobject, then add the TSManager and GameStateManager components. If using a custom TSManager, attach that component instead.

Now the project is ready for Tempust Script. Add the ScriptHolder component to any object, assign the script in the inspector, then call ScriptHolder.Execute() to run the script.