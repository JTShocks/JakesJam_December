using Godot;
using System;

public partial class RespawnSystem : Node
{

    //Recieves a signal that the player has died
    //when it does, begin a countdown
    //when the timer stops, respawn the player at the respawn point

    [Export]
    Node2D respawnPoint;
    public Timer respawnTimer;
    float respawnTime = 10f;

    public override void _Ready()
    {
        respawnTimer = new Timer
        {
            OneShot = true
        };
        AddChild(respawnTimer);

        respawnTimer.Timeout += RespawnPlayer;
        base._Ready();
    }

    public void RespawnPlayer()
    {
        //Instantiate the player in the scene
        var player = GD.Load<PackedScene>("res://Player/player.tscn").Instantiate() as PlayerMovement;
        AddChild(player);
        Callable callable = new Callable(this, MethodName.StartRespawnTimer);
        player.Connect("OnPlayerDeath", callable);
        GetTree().CallGroup("Camera", "SetFollowTarget", player);


        
    }

    public void StartRespawnTimer()
    {
        respawnTimer.Start(respawnTime); // Start the respawn timer
    }
}
