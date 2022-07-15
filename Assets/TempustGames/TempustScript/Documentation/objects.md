# Objects

In Tempust Script, objects are a way of representing game object as strings. These are then assigned in the Unity editor. Several commands use objects as arguments.

## Defining Objects

For objects to be used in scripts, they need to be defined. While this can be done anywhere in the script, it is generally done in the first few lines. To define an object, type the keyword "def", followed by the name of the object:

    def obj_name

## Special Objects

There are two special objects that do not need to be defined: "obj" and "plr". "obj" is used to refer to the game object which has the ScriptHolder component. "plr" is used to refer to the game object that activates the script.

## Overriding the GetObject Method

The TSManager class contains a method that for getting game objects from scripts. By default this calls TSScript.GetObject(), but it can be customized if you need it to assign game objects dynamically.

    public override void GetObject(TSScript script, string obj) { }

## Assing Objects in the Unity Editor

After attaching a ScriptHolder component to a game object and assigning a text asset, you will need to update the object list.

Click the drop down menu for the component, then from the menu select "Update Object List". This will allow you to drag and drop game objects from the scene.