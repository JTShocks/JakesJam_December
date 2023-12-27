using Godot;
using System;
using System.Collections.Generic;

public partial class Interactor : Node
{

    Node2D interactTarget;
    [Export]
    public PlayerMovement player;
    public override void _Ready()
    {
        base._Ready();
       

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



    public void OnEnterArea(Node2D target)
    {

        GD.Print(target + " entered the range");
        if(target is Enemy_Base enemy)
        {
            return;
        }
        if(target is CharacterBody2D body)
        {
            interactTarget = target;
        }

    }

    public void OnLeaveArea(Node2D target)
    {
        if(target == interactTarget)
        {
            interactTarget = null;
        }
    }




    
}
