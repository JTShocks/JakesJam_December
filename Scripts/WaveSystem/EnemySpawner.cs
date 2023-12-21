using Godot;
using System;

public partial class EnemySpawner : Node
{
	//What is needed for the enemy spawner?
	//1. Recieves the signal to begin spawning enemies
	//2. Can be loaded with any enemy to spawn
	//3. Spawns the enemy in a given area

	Area2D spawnArea; // Area to spawn the enemies within


	//Best way is some kind of queue system?
	//Whenever the count for enemies in the group is under the given amount (determined by the wave)
	//a valid spawner will try to spawn an enemy
	//These cannot happen all at once and it should check the count

	//Spawners can listen for a signal to when the enemies in the group die to then try replacing them

	//The spawner should ONLY listen for the signal to spawn an enemy @ a given position


	public void SpawnEnemy(Vector2 spawnLocation)
	{
		//Spawn an enemy at the given location
	}

}
