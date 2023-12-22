using Godot;
using System;

public partial class EnemySpawner : Node
{
	//What is needed for the enemy spawner?
	//1. Recieves the signal to begin spawning enemies
	//2. Can be loaded with any enemy to spawn
	//3. Spawns the enemy in a given area

	Area2D spawnArea; // Area to spawn the enemies within


	public override void _Ready()
	{
		//Add to a group of spawners
	}

	public void SpawnEnemy(Vector2 spawnLocation)
	{
		var enemy = GD.Load<PackedScene>("res://Enemy/enemy.tscn").Instantiate() as Node2D;
		AddChild(enemy);
		enemy.GlobalPosition = spawnLocation;
	}

}
