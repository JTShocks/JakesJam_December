using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
    [Export]
    public Resource characterData;

    [Signal]
    public delegate void EnemyKilledEventHandler(int value);

    int moveSpeed {get; set;}

    [Export]
    public int playerMoney;

    Vector2 inputDirection;


    public override void _Ready()
    {
        GetTree().CallGroup("Aliens", "SetPlayer", this);

        if(characterData is CharacterData stats)
        {
            moveSpeed = stats.MoveSpeed;
        }
        EnemyKilled += GetMoney;

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

    public void GetMoney(int value)
    {
        playerMoney += value;
    }
}
