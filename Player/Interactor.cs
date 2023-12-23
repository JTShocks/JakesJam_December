using Godot;
using System;
using System.Collections.Generic;

public partial class Interactor : Node
{

    PhysicsBody2D interactTarget;
    public PlayerMovement player;

    public override void _Ready()
    {
        base._Ready();
        player = GetParent().GetParent().GetNode<PlayerMovement>("Player");

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Input.IsActionJustPressed("Interact"))
        {
            if(interactTarget is IBuilding building)
            {
                building?.TryUpgrade(player.playerMoney);
            }
            if(interactTarget is IInteractable interactable)
            {
                interactable?.Interact(this);
            }
        }

        if(Input.IsActionJustPressed("Repair"))
        {
            if(interactTarget is IBuilding building)
            {
                building?.TryRepair(player.playerMoney);
            }
        }
    }



    public void OnEnterArea(PhysicsBody2D target)
    {
        if(target is Enemy_Base enemy)
        {
            return;
        }
        interactTarget = target;
    }

    public void OnLeaveArea(PhysicsBody2D target)
    {
        if(target == interactTarget)
        {
            interactTarget = null;
        }
    }




    
}
