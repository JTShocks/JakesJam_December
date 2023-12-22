using Godot;
using System;

public partial class BuildSpot : Node2D
{
	//What does the build spot need?
	//Only needs to know that it is empty or filled
	//If empty, it spawns in a building at the space, that is it

	//Cannot upgrade a building if it has lost health, must be full health to be upgraded

	bool spaceOccupied;

	public Turret_Base placedBuilding;

    //Subscribes to the events present within the placed building

    public override void _Ready()
    {
        base._Ready();
		OnInteract();
    }

    public void OnInteract() // Method from the IInteractable
	{
		GD.Print("Build spot activated");
		if(!spaceOccupied)
		{
			PlaceBuilding();
		}
		//Does nothing otherwise

	}

	public void PlaceBuilding()
	{
		placedBuilding = GD.Load<PackedScene>("res://Buildings/turret.tscn").Instantiate() as Turret_Base; // Loads in the turret
		AddChild(placedBuilding);
		Callable buildingDestroyed = new Callable(this, MethodName.OnBuildingDestroyed);
		placedBuilding.Connect("OnTurretDestroyed", buildingDestroyed);

		// Space holds a reference to the node
		spaceOccupied = true;
	}

	public void OnBuildingDestroyed()
	{
		placedBuilding.QueueFree();
		spaceOccupied = false;
	}
}
