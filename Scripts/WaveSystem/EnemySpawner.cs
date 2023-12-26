using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	//All this does is spawn enemies at a given location

	Timer spawnTimer;
	float spawnRateInSeconds;


	public override void _Ready()
	{
		AddToGroup("Spawners");
	}

	public void SpawnEnemy()
	{
		Random rnd = new Random();
		Vector2 spawnLocation = new Vector2(rnd.Next(-64, 64), rnd.Next(-64, 64)) + GlobalPosition; // Chooses a random spot around the spawner
		var enemy = GD.Load<PackedScene>("res://Enemy/enemy.tscn").Instantiate() as Node2D; // Load in the enemy as an enemy
		AddChild(enemy);
		enemy.GlobalPosition = spawnLocation;
	}

}
