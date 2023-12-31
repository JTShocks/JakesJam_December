using Godot;
using System;

public partial class Core : CharacterBody2D, ITakeDamage, IInteractable
{
	//The Core is where the enemies are trying to get to by default
	//1. Takes Damage from enemies
	//2. If it is destroyed, the game is over
	//

	//Get a reference to the wave controller to start a new wave
	//When the player interacts with the Core ,start the next wave

	[Signal]
	public delegate void OnCoreInteractEventHandler();
	[Signal]
	public delegate void OnCoreTakeDamageEventHandler();
	[Signal]
	public delegate void OnCoreDestroyedEventHandler();

	[Export]
	public int currentHealth {get; set;}
	public int maxHealth = 500;
	public override void _Ready()
	{
		currentHealth = maxHealth;
		SetAsTarget();
	}


    public void TakeDamage(int damage)
    {
		int reducedDamage = damage - 3;
		if(currentHealth > 0)
		{
        currentHealth -= reducedDamage;
		EmitSignal(SignalName.OnCoreTakeDamage);
		}

		if(currentHealth <= 0)
		{
			EmitSignal(SignalName.OnCoreDestroyed);
		}
    }

    public void Interact()
    {
		EmitSignal(SignalName.OnCoreInteract);
		GD.Print("Core interacted");
    }

    public void Interact(Interactor interactor)
    {
        Interact();
    }

	public void SetAsTarget()
	{
		GetTree().CallGroup("Aliens", "SetTarget", this);
	}
}
