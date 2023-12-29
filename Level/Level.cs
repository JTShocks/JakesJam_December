using Godot;
using System;

public partial class Level : Node2D
{
    [Signal]
    public delegate void ChangeLevelEventHandler();
    //Changing to a level requires the name of the level you want to change to

    [Export]
    public string level_name = "level";


    public void _on_level_transition_try_to_change_level()
    {
        GD.Print("Level Recieved, going to Switcher");
        EmitSignal(SignalName.ChangeLevel);

    }
}
