using Godot;
using System;
using System.Collections.Generic;

public partial class Turret_Base : CharacterBody2D, IBuilding, ITakeDamage
{
	[Signal]
	public delegate void OnTurretDestroyedEventHandler();

	int baseDamage = 5;
	float shootIntervalInSeconds = .33f;
	Faction turretOwner = Faction.Player;
	List<PhysicsBody2D> listOfTargets = new();
	PhysicsBody2D currentTarget;

	Vector2 lookDirection;
	bool hasTargets;
	bool timerHasStarted;
	Timer turretShootTimer = new();
	[Export]
	public BuildingStats baseStats;

	int currentHealth {get; set;}

	int totalBuildingValue;
	int upgradePrice {get; set;}
	int buildingLevel = 1;
	int repairPrice {get; set;}


	Node2D turretHead;
	public IDoDamage turretWeapon;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		baseStats = new BuildingStats(10, 100, 100);
		currentHealth = baseStats.BaseHealth;
		baseDamage = baseStats.BaseDamage;
		totalBuildingValue = baseStats.BasePrice;
		upgradePrice = (int)(100 * 1.5f);
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
			currentTarget = null;
			hasTargets = false;
			turretShootTimer.Stop();
			timerHasStarted = false;
		}
		else{

			hasTargets = true;
		}

		if(hasTargets)
		{
			currentTarget = listOfTargets[0];
			TrackTarget();
		}
	}

	public void TryDoAttack()
	{
		GD.Print("Turret has fired");
			turretWeapon?.DoDamage(baseStats.BaseDamage);

	}

	public void OnTargetEnterRange(PhysicsBody2D target)
	{
		if(target is PlayerMovement player)
		{
			//Ignore whatever faction owns the turret
			GD.Print("Owner is in range");
			return;
		}
		listOfTargets.Add(target);
		ReorderTargets();
		if(timerHasStarted == false)
		{
			turretShootTimer.Start(shootIntervalInSeconds);
			timerHasStarted = true;
		}
		GD.Print("Target is added");
		

	}
	public void OnTargetLeaveRange(PhysicsBody2D target)
	{
		if(target is PlayerMovement player)
		{
			return;
		}
		if(listOfTargets.Contains(target))
		{
			listOfTargets.Remove(target);
			if(listOfTargets.Count > 0)
			{
			ReorderTargets();
			}

			GD.Print(target + " removed");
		}

		if(listOfTargets.Count == 0)
		{
			hasTargets = false;
			turretShootTimer.Stop();
			GD.Print(this + " has no targets");

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
		currentTarget = listOfTargets[0];
	}

	public void TrackTarget()
	{
		Vector2 angleToTarget = new();
		angleToTarget = currentTarget.GlobalPosition - this.GlobalPosition;
		turretHead.GlobalRotation = MathF.Atan2(angleToTarget.Y, angleToTarget.X);

	}

    public void UpgradeBuilding()
    {
		buildingLevel++;
		GD.Print("Building is upgraded");
		UpdateBuildingStats();
    }

    public void RepairBuilding()
    {
		GD.Print("Building repaired");
        currentHealth = baseStats.BaseHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
		repairPrice = baseStats.BaseHealth - (baseStats.BaseHealth* (currentHealth/baseStats.BaseHealth));
		if(currentHealth <= 0)
		{
			Kill();
		}
    }

	public void Kill()
	{
			GD.Print("Turret has been destroyed");
			EmitSignal(SignalName.OnTurretDestroyed);
	}

    public void TryUpgrade(int currentMoney)
    {
        if(currentMoney < upgradePrice)
		{
			GD.Print("Not enough money. Price to upgrade =" + upgradePrice);
			return;
		}
		if(currentHealth < baseStats.BaseHealth)
		{
			GD.Print("Building is damaged");
			return;
		}
		if(buildingLevel >= 3)
		{
			return;
		}
		GetTree().CallGroup("Player", "LoseMoney", upgradePrice);
		UpgradeBuilding();
    }

    public void TryRepair(int currentMoney)
    {
        if(currentMoney < repairPrice)
		{
			GD.Print("Not enough money: Price to repair = " + repairPrice);
			return;
		}
		if(currentHealth == baseStats.BaseHealth)
		{
			GD.Print("Building is full health");
			return;
		}
		GetTree().CallGroup("Player", "LoseMoney", repairPrice);
		RepairBuilding();
    }

	public void UpdateBuildingStats()
	{
		switch(buildingLevel)
		{
			case 2:
			baseStats.BaseDamage += 5;
			baseStats.TotalBuildingValue = totalBuildingValue + upgradePrice;
			baseStats.BaseHealth += 100;
			upgradePrice = 275;
			
			break;
			case 3:
			shootIntervalInSeconds *= .5f; //Speed up shooting by 100%
			baseStats.TotalBuildingValue = totalBuildingValue + upgradePrice;
			baseStats.BaseHealth += 100;	

			break;
		}
		currentHealth = baseStats.BaseHealth;
	}
}
