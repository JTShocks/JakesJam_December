using Godot;
using System;

public partial class LevelSwitcher : Node
{

    int startValue = 0;
    Level currentLevel;
    Callable levelChangeCallable;
    public override void _Ready()
    {
        currentLevel = GD.Load<PackedScene>("res://Level/main_level.tscn").Instantiate() as Level;
        AddChild(currentLevel);
        //currentLevel = GetNode<Level>("main_Level");
        Callable levelChangeCallable = new Callable(this, MethodName.HandleLevelChanged);

        currentLevel.Connect("ChangeLevel", levelChangeCallable);
    }

    public void HandleLevelChanged()
    {
        Level nextLevel;
        //string nextLevelName;

        //GD.Print("Old level was:" + currentLevelName);
        if(startValue == 0)
        {
            nextLevel = GD.Load<PackedScene>("res://Level/lose_screen.tscn").Instantiate() as Level;
            startValue = 1;
        }
        else
        {
            nextLevel = GD.Load<PackedScene>("res://Level/main_level.tscn").Instantiate() as Level;
            startValue = 0;

        }

        AddChild(nextLevel);
        nextLevel.Connect("ChangeLevel", new Callable(this, "HandleLevelChanged"));
        currentLevel.QueueFree();
        currentLevel = nextLevel;

    }
}
