using Godot;
using System;

public partial class Core : Node, ITakeDamage
{
	//The Core is where the enemies are trying to get to by default
	//1. Takes Damage from enemies
	//2. If it is destroyed, the game is over
	//

	int currentHealth {get; set;}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

}
