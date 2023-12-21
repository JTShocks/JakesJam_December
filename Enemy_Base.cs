using Godot;
using System;

public partial class Enemy_Base : CharacterBody2D, ITakeDamage
{
    const int moveSpeed = 100;

    CharacterBody2D player;
    HealthSystem healthSystem;
    //Enemy behavior
    //Should first converge towards the core (the center of the map where things are built)
    //If they get in range of a turret/building, they move toward that building
    //If they are not in range of a building, they move towards the most recent source of damage

    //Example: If the player shoots them outside the range of the turrets, they move towards the player
    //If they are already under attack from a turret, they should prioritize based on distance between the most recent sources of damage
    //

    public override void _Ready()
    {
        AddToGroup("Aliens");
        healthSystem = GetNode<HealthSystem>("HealthSystem");
        base._Ready();
        
    }

    public override void _PhysicsProcess(double delta)
    {
        if(player == null)
        {
            return;
        }
        Vector2 playerDirection = player.GlobalPosition - GlobalPosition;
        GlobalRotation = Mathf.Atan2(playerDirection.Y, playerDirection.X);
        MoveAndCollide(playerDirection.Normalized() * moveSpeed * (float)delta);



        base._PhysicsProcess(delta);
    }

    public void SetPlayer(CharacterBody2D p)
    {
        player = p;
    }

    public void Kill()
    {
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

}
