using Godot;
using System;
using System.Collections.Generic;

public partial class Turret_Base : Node2D, IBuilding
{

	int baseDamage = 5;
	float shootIntervalInSeconds = .33f;
	Faction turretOwner = Faction.Player;
	List<PhysicsBody2D> listOfTargets = new();
	PhysicsBody2D currentTarget;

	Vector2 lookDirection;
	bool hasTargets;
	bool timerHasStarted;
	Timer turretShootTimer = new();


	Node2D turretHead;
	public IDoDamage turretWeapon;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turretHead = GetNode<Node2D>("TurretHead");
		turretWeapon = GetNode<Node2D>("TurretHead") as IDoDamage;
		AddChild(turretShootTimer);

		turretShootTimer.Timeout += TryDoAttack;
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(listOfTargets.Count < 1)
		{
			hasTargets = false;
			turretShootTimer.Stop();
		}
		else
		{
			hasTargets = true;
			ReorderTargets();
			currentTarget = listOfTargets[0];
		}

		if(hasTargets)
		{
			TrackTarget();
		}
	}

	public void TryDoAttack()
	{
		GD.Print("Turret has fired");
			turretWeapon?.DoDamage(baseDamage);

	}

    public void PickUpBuilding()
    {
        throw new NotImplementedException();
    }

    public void PlaceBuilding()
    {
        throw new NotImplementedException();
    }

	public void OnTargetEnterRange(PhysicsBody2D target)
	{
		if(target.GetNode<CharacterData>("CharacterData")._characterFaction == turretOwner)
		{
			//Ignore whatever faction owns the turret
			GD.Print("Owner is in range");
			return;
		}
		listOfTargets.Add(target);
		ReorderTargets();
		GD.Print("Target is added");
		

	}
	public void OnTargetLeaveRange(PhysicsBody2D target)
	{
		if(listOfTargets.Contains(target))
		{
			listOfTargets.Remove(target);
			GD.Print("Target removed");
		}

		if(listOfTargets.Count != 0)
		{
			ReorderTargets();
		}
		if(hasTargets == false)
		{
			turretShootTimer.Stop();
		}


	}

	public void ReorderTargets()
	{
		List<PhysicsBody2D> tempList = new();
		foreach (PhysicsBody2D target in listOfTargets)
		{
			tempList.Add(target);
		}
		listOfTargets = tempList;
	}

	public void TrackTarget()
	{
		Vector2 angleToTarget = new();
		angleToTarget = currentTarget.GlobalPosition - this.GlobalPosition;
		turretHead.GlobalRotation = MathF.Atan2(angleToTarget.Y, angleToTarget.X);

		if(timerHasStarted == false)
		{
			turretShootTimer.Start(shootIntervalInSeconds);
			timerHasStarted = true;
		}

	}

}
