using Godot;
using System;

public partial class PlayerWeaponController : Node2D
{
	// Called when the node enters the scene tree for the first time.

	RayCast2D playerCast;

	public IDoDamage equippedWeapon;

	public override void _Ready()
	{
		equippedWeapon = GetNode<Node2D>("GunWeapon") as IDoDamage;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 lookDirection = GetGlobalMousePosition() - this.GlobalPosition;
		this.GlobalRotation = Mathf.Atan2(lookDirection.Y, lookDirection.X);

		if(Input.IsActionJustPressed("Shoot"))
		{
			TryToShoot();
		}
	}

	public void TryToShoot()
	{
		equippedWeapon?.DoDamage(0);
	}

	public void Shoot()
	{
		GD.Print("Player has shot");
		var coll = playerCast.GetCollider();
		if(coll is ITakeDamage target)
		{
			target.TakeDamage(1);
		}
		else
		{
			GD.Print("Didn't hit anything");
		}

	}
}
