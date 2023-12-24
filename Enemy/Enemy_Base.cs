using Godot;
using System;

public partial class Enemy_Base : CharacterBody2D, ITakeDamage
{
    int moveSpeed {get; set;}

    CharacterBody2D target;
    HealthSystem healthSystem;

    [Export]
    public Resource characterStats;
    //Enemy behavior
    //Should first converge towards the core (the center of the map where things are built)
    //If they get in range of a turret/building, they move toward that building
    //If they are not in range of a building, they move towards the most recent source of damage

    //Example: If the target shoots them outside the range of the turrets, they move towards the target
    //If they are already under attack from a turret, they should prioritize based on distance between the most recent sources of damage
    //

    public override void _Ready()
    {
        AddToGroup("Aliens");
        if(characterStats is CharacterData stats)
        {
            moveSpeed = stats.MoveSpeed;
        }
        healthSystem = GetNode<HealthSystem>("HealthSystem");
        base._Ready();
        
    }

    public override void _PhysicsProcess(double delta)
    {
        if(target == null)
        {
            return;
        }
        Vector2 targetDirection = target.GlobalPosition - GlobalPosition;
        GlobalRotation = Mathf.Atan2(targetDirection.Y, targetDirection.X);
        MoveAndCollide(targetDirection.Normalized() * moveSpeed * (float)delta);



        base._PhysicsProcess(delta);
    }

    public void SetTarget(CharacterBody2D t)
    {
        target = t;
    }

    public void Kill()
    {
        GetTree().CallGroup("Player", "GetMoney", 25);
        QueueFree();
    }

    public void TakeDamage(int damage)
    {
      healthSystem.TakeDamage(damage);   
      if(healthSystem._currentHealth <= 0)
      {

        Kill();
      }

    }

    public void DropMoney()
    {
        GD.Load<PackedScene>("res://Collectables/money.tscn").Instantiate();
    }

}
