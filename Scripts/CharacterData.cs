using Godot;
using System;

public partial class CharacterData : Node
{
	[Export]
	public Faction _characterFaction;
	[Export]
	public int maxHealth {get; set;} = 100;


    public override void _Ready()
    {
        base._Ready();
    }




}
