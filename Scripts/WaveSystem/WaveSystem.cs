using Godot;
using System;

public partial class WaveSystem : Node
{
	[Signal]
	public delegate void OnNewWaveStartEventHandler(int currentWave);
	//Anything that listens for the NewWaveStart has the current wave passed through
	//This is for deciding what enemies to spawn, their numbers, as well as any HUD elements reliant on the current wave

	[Signal]
	public delegate void OnWaveEndedEventHandler();
	//Meant as a reciever from the enemy spawners to determine when the wave ended
	//When all enemies are dead from the new wave, Sends a signal to this


	public Timer waveIntervalTimer = new(); // Timer between each wave
	public int currentWaveCount = 0; // Current wave

	public float waveTimerDurationInSeconds = 120f; // Default 2 minutes between waves
	public override void _Ready()
	{
		AddChild(waveIntervalTimer);
		waveIntervalTimer.Timeout += StartNewWave;
		OnWaveEnded += StartWaveTimer; // When recieving the signal that a wave has ended, start the new timer
		//For testing purposes
		StartWaveTimer();
	}
	public void StartWaveTimer()
	{
		GD.Print("Timer started");
		waveIntervalTimer.Start(waveTimerDurationInSeconds);
	}

	public void StartNewWave()
	{
		GD.Print("New wave started");
		currentWaveCount++;
		EmitSignal(SignalName.OnNewWaveStart, currentWaveCount);
		waveIntervalTimer.Stop();
	}

	//What does the Wave System need?
	//1. Timer to decide when to launch the next wave V
	//2. Keep track of the current wave V
	//3. Send a signal when a new wave starts V
	//4. Recieve a signal when the player has defeated the wave V
}
