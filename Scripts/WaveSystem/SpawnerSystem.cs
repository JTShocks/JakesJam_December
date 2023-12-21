using Godot;
using System;

public partial class SpawnerSystem : Node
{
	// Handles all the sending of signals to spawn the enemies for the wave
	// Has a personal count of the given wave and what to spawn in (use a simple switch statement)

	private int enemiesToSpawn = 5;
	private int maxEnemiesAtOneTime;
	private int currentWaveDeathTotal;

	public override void _Ready()
	{
		//Should recieve a signal for on new wave start
		// Something for Connect to this as a callable reference
		//maxEnemiesAtOneTime = some ratio between the # of active spawners and the enemiesToSpawn
		// perhaps a max of 15 per spawner
	}

	public void SetCurrentWaveValues(int currentWave)
	{
		//Waves increment on base 5, should deal with MOD to determine when to increase
		int waveMOD = currentWave%10;
		switch(waveMOD) // based on the MOD of the current wave, can change certain attributes of the following waves
		// In theory, should make the game mostly infinite until it is physically impossible to beat
		{   
			case 1: // Waves 1 and 11, should probably not do anything
			enemiesToSpawn += 5 + (currentWave/2); // 5 enemies on wave 1, +10 enemies on wave 11
			break;
			case 2:

			break;


			default:
			break;
		}
	}

	public void TryToSpawnEnemy()
	{
		//Function to try to spawn an enemy if the requirements are meant.
		//If yes, send a signal to an attached spawner to spawn a new enemy
		// The enemmies to 
	}

	public void OnEnemyDied() //Called whenever an enemy spawned from the wave group is killed
	{
		currentWaveDeathTotal++; // Increment the death counter
		CheckRemainingEnemies(); // See if the wave should be over
	}

	public void CheckRemainingEnemies()
	{
		if(currentWaveDeathTotal == enemiesToSpawn)
		{
			//EmitSignal(SignalName.OnWaveEnded);
			//Signal that the current wave has ended
		}
	}


}
