using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
    [Export]
    public Resource characterData;

    int moveSpeed {get; set;}

    Vector2 inputDirection;


    public override void _Ready()
    {
        GetTree().CallGroup("Aliens", "SetPlayer", this);
        if(characterData is CharacterData stats)
        {
            moveSpeed = stats.MoveSpeed;
        }

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
