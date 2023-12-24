using Godot;
using System;

public partial class UpgradeStation : CharacterBody2D, IInteractable
{
    public void Interact()
    {
        throw new NotImplementedException();
    }

    public void Interact(Interactor interactor)
    {
        if(interactor.player.playerMoney < 100 * interactor.player.playerLevel)
        {
            return;
        }
        GetTree().CallGroup("Player", "LoseMoney", 100* interactor.player.playerLevel);
        interactor.player.LevelUp();
    }

}
