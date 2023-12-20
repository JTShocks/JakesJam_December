using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
    [Export]
    public int moveSpeed {get; set;} = 400;

    Vector2 inputDirection;


    public override void _Ready()
    {
        GetTree().CallGroup("Aliens", "SetPlayer", this);

    }

    public override void _Process(double delta)
    {
        GetInput();
        this.MoveAndCollide(inputDirection * moveSpeed * (float)delta);   
    }

    public void GetInput()
    {
        inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        //this.Velocity = inputDirection * moveSpeed;

    }
}
