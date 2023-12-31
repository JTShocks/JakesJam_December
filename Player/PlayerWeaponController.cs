using Godot;
using System;

public partial class PlayerWeaponController : Node2D
{
	// Called when the node enters the scene tree for the first time.

	RayCast2D playerCast;

	public IDoDamage equippedWeapon;
	public int weaponDamage = 0;

	public float weaponFireRate = .16f;
	Timer weaponShootTimer;

	public override void _Ready()
	{
		equippedWeapon = GetNode<Node2D>("GunWeapon") as IDoDamage;
		weaponShootTimer = new Timer{
			OneShot = true
		};
		AddChild(weaponShootTimer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 lookDirection = GetGlobalMousePosition() - this.GlobalPosition;
		this.GlobalRotation = Mathf.Atan2(lookDirection.Y, lookDirection.X);

		if(Input.IsActionPressed("Shoot"))
		{
			TryToShoot();
		}
	}

	public void TryToShoot()
	{
		if(weaponShootTimer.IsStopped())
		{
			equippedWeapon?.DoDamage(weaponDamage);
			weaponShootTimer.Start(weaponFireRate);
		}

	}

	public void _on_player_update_weapon(int newDamage)
	{
		weaponDamage = newDamage;
	}
}
