using Godot;
using System;
using System.Collections.Generic;


public partial class SpawnerSystem : Node
{
	//Calls all spawners in the group to spawn enemies, provided the group is not at the "max" enemies
	int enemyPopTarget; //The target population of enemies to maintain throughout the wave, also known as the density
	float waveThreshold = .5f; // The % of enemies that must be killed before the spawners send more creatures to the base.
	Timer spawnCycle = new();
	float waveInterval = 30f; // Time between waves in seconds
	int waveSize;

	//What does this system need to do?
	//1. Update the strength based on the current wave
	//2. Keep up the production of waves as the game progresses and the player gets stronger
	//3. Attack from various directions in groups, not just randomly





	public override void _Ready()
	{
		AddChild(spawnCycle); // Create a timer for the spawner system to decide when to send a new wave
		spawnCycle.Timeout += TryToSpawnWave; //When it times out, try to spawn a wave

	}
	public void ActivateSpawners(int currentWave)
	{
		SetDensityOfSpawns(currentWave);
		SpawnWave(waveSize);
		spawnCycle.Start(waveInterval);
		GetTree().CallGroup("Core", "SetAsTarget");
	}

	public void DeactivateSpawners()
	{
		spawnCycle.Stop();
	}

	public void TryToSpawnWave() //Tries to spawn an enemy from the spawners at a given time
	{
		var enemies = GetTree().GetNodesInGroup("Aliens").Count; //Get a count of all enemies currently in the scene
		if(enemies < .5 * enemyPopTarget) //If the number of enemies is below the population target
		{
			SpawnWave(waveSize); //Spawn a wave to make up the difference
		}
		
	}

	public void SpawnWave(int size)
	{
		//Choose a spawner to spawn a new wave from (only 4 spawners, so 1 of 4 directions)
		Random rnd = new();
		var spawners = GetTree().GetNodesInGroup("Spawners");
		List<EnemySpawner> tempList = new();
		foreach(EnemySpawner spawner in spawners)
		{ //Get the spawners from the group
			tempList.Add(spawner); //Add them all to a temporary list
		}

		EnemySpawner currentWaveSpawner = tempList[rnd.Next(0, tempList.Count)];
		for(int i = 0; i <= size; i++)
		{
			currentWaveSpawner.SpawnEnemy();
		}
		GetTree().CallGroup("Core", "SetAsTarget");
		GD.Print("Wave has spawned of size: " + size);
		//Wave comes from a random direction
		//Spawns a stream of enemies that 
	}

	public void SetDensityOfSpawns(int currentWave)
	{
		if(currentWave%10 == 0)
		{
			waveInterval -= 10f;
			if(waveInterval < 10)
			{
				waveInterval = 10;
			}
		}
		if(currentWave%5 == 0)
		{
			waveThreshold += .1f;
		}
		waveSize += 4 * currentWave;
		enemyPopTarget += waveSize/2; // the population allowed is always increased
		
	}



}
