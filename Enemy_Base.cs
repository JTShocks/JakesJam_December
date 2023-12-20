using Godot;
using System;

public partial class Enemy_Base : CharacterBody2D, ITakeDamage
{
    const int moveSpeed = 100;

    CharacterBody2D player;
    HealthSystem healthSystem;

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
