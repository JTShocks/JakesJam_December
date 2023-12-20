using Godot;
using System;
using System.Collections.Generic;

public partial class Detector : Node
{
    CharacterBody2D _primaryTarget;


    //List of the Characters that enter the radius

    [Signal]

    public delegate void EnemyEnterRangeEventHandler(PhysicsBody2D target);
    [Signal]
    public delegate void EnemyLeaveRangeEventHandler(PhysicsBody2D target);

    public override void _Ready()
    {
        base._Ready();
        
    }

    public void _on_detection_area_body_entered(PhysicsBody2D body)
    {
       GD.Print("Target entered of Faction: " + body.GetNode<CharacterData>("CharacterData")._characterFaction.ToString());

        EmitSignal("EnemyEnterRange", body);
        //Can always put enemies on a different collision layer, so the player is always ignored
    }

    public void _on_detection_area_body_exited(PhysicsBody2D body)
    {
        GD.Print("Target out of range");
        EmitSignal("EnemyLeaveRange", body);
        //Remove the target from the list
    }
}
