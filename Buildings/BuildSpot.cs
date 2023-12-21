using Godot;
using System;

public partial class BuildSpot : Node2D
{
	//What does the build spot need?
	//Only needs to know that it is empty or filled
	//If empty, it spawns in a building at the space, that is it

	//Cannot upgrade a building if it has lost health, must be full health to be upgraded

	bool spaceOccupied;

	public IBuilding placedBuilding;
	//Buildings need
	// RepairBuilding(), UpgradeBuilding()

	//Subscribes to the events present within the placed building

	public void OnInteract() // Method from the IInteractable
	{
		if(!spaceOccupied)
		{
			PlaceBuilding();
		}
		//Does nothing otherwise

	}

	public void PlaceBuilding()
	{
		var building = GD.Load<PackedScene>("").Instantiate(); // Loads in the turret
		AddChild(building);
		// Space holds a reference to the node
		spaceOccupied = true;
	}
}
