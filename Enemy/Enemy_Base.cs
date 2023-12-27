using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy_Base : CharacterBody2D, ITakeDamage
{
    int moveSpeed {get; set;}
    float currentHealth {get; set;}
    float baseDamage {get; set;}
    RayCast2D attackRay;
    bool canAttack;


    float attackSpeed = 1f;


    CharacterBody2D target;
    List<Enemy_Base> alliesInRange;
    Timer attackTimer;
    public IDoDamage enemyAttack;

    [Export]
    public Resource characterStats;
    //Move toward target
    //Get the closest building/ valid target
    //That can be the player OR a turret

    //Once they get a target, they should focus on it. 
    //When they spawn, they should seek out the Core
    //If they get shot, a subsection of the bugs (X in a radius), should group up to attack it


    public override void _Ready()
    {
        AddToGroup("Aliens");
        attackTimer = new Timer();
        AddChild(attackTimer);
        attackRay = GetNode<RayCast2D>("AttackRay");
        if(characterStats is CharacterData stats)
        {
            moveSpeed = stats.MoveSpeed;
            currentHealth = stats.MaxHealth;
            baseDamage = stats.BaseDamage;
        }
        attackTimer.OneShot = true;
        GetTree().CallGroup("Core", "SetAsTarget");
        
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

        var coll = attackRay.GetCollider();
        if(coll is ITakeDamage validT)
        {
            TryToAttack(validT);

        }
        if(attackTimer.IsStopped())
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }



        base._PhysicsProcess(delta);
    }

    public void SetTarget(CharacterBody2D t)
    {
        target = t;
    }
    public void TryToAttack(ITakeDamage t)
    {
        if(canAttack == true)
        {
            AttackTarget(t);
            attackTimer.Start(attackSpeed);
        }

    }
    public void AttackTarget(ITakeDamage t)
    {
        t?.TakeDamage((int)baseDamage);
        


    }

    public void Kill()
    {
        GetTree().CallGroup("Player", "GetMoney", 5);
        QueueFree();
    }

    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      //Set out a pulse to all allies in a range around them to converge on the same target
      if(currentHealth <= 0)
      {

        Kill();
      }

    }

    public void OnBodyEnterRange(PhysicsBody2D body)
    {
        if(body is CharacterBody2D character)
        {
            if(target != null)
            {
                return;
            }
            SetTarget(character);
        }
    }
    public void OnBodyLeaveRange(PhysicsBody2D body)
    {
        if(body is CharacterBody2D character)
        {
            if(character == target)
            {
                SetTarget(GetTree().GetFirstNodeInGroup("Core") as CharacterBody2D);
            }
        }

    }
}
