using Godot;
using System;

public partial class WaveSystem : Node
{
	[Signal]
	public delegate void OnNewWaveStartEventHandler(int currentWave);
	[Signal]
	public delegate void OnWaveEndedEventHandler();

	//Player activates the waves from the Core
	//The timer for the duration of the wave is based on the current wave
	//The longer the game goes on, the longer the timer becomes
	//easy method, check the MOD of the currentWave & increment the wave duration based on that
	//1 minute until reaching the cap of 8 minutes



	public Timer waveIntervalTimer = new(); // Timer between each wave
	public int currentWaveCount = 0; // Current wave

	public float waveTimerDurationInSeconds = 120f; // Default 2 minutes between waves
	public override void _Ready()
	{
		AddChild(waveIntervalTimer);
		waveIntervalTimer.Timeout += StartNewWave;
		OnWaveEnded += StopTimer; // When recieving the signal that a wave has ended, start the new timer
		//For testing purposes
		//StartWaveTimer();
	}
	public void StopTimer()
	{
		GD.Print("Timer started");
		waveIntervalTimer.Stop();
	}

	public void StartNewWave()
	{
		GD.Print("New wave started");
		currentWaveCount++;
		EmitSignal(SignalName.OnNewWaveStart, currentWaveCount);
		float waveDuration = waveTimerDurationInSeconds + (60f*(currentWaveCount/2));
		if(waveDuration > 480f)
		{
			waveDuration = 480f;
		}
		waveIntervalTimer.Start(waveDuration);
	}

}
