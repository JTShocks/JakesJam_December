using Godot;
using System;

public partial class MainCamera : Node2D
{

    [Export]
    public CharacterBody2D followTarget;
    public override void _Ready()
    {
        AddToGroup("Camera");
        var player = GetParent().GetNode<PlayerMovement>("Player");
        SetFollowTarget(player);
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if(followTarget != null)
        {
            this.GlobalPosition = followTarget.GlobalPosition;
        }
        else
        {return;}

    }

    public void SetFollowTarget(CharacterBody2D target)
    {
        followTarget = target;
    }
}
