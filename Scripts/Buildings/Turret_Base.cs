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
		upgradePrice = (int)(baseStats.BasePrice*1.5f);
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
		GD.Print("Target is added");
		

	}
	public void OnTargetLeaveRange(PhysicsBody2D target)
	{
		if(listOfTargets.Contains(target))
		{
			listOfTargets.Remove(target);
			ReorderTargets();
			GD.Print("Target removed");
		}

		if(listOfTargets.Count == 0)
		{
			hasTargets = false;
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
		float newDamage = baseDamage*1.5f;
        BuildingStats newStats = new BuildingStats(((int)newDamage), (int)((float)baseStats.BaseHealth*1.5), totalBuildingValue + upgradePrice);
		baseStats = newStats;
		totalBuildingValue = baseStats.BasePrice;
		upgradePrice = (int)(totalBuildingValue*1.5f);
		currentHealth = baseStats.BaseHealth;
	}
}
