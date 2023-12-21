using Godot;
using System;

public partial class CharacterData : Resource
{
	[Export]
	public Faction _characterFaction;
	[Export]
	public int MaxHealth {get; set;}
    [Export]
    public int MoveSpeed {get; set;}


    public CharacterData() : this(0, 400, 0){}

    public CharacterData(int health, int moveSpeed, Faction faction)
    {
        MaxHealth = health;
        MoveSpeed = moveSpeed;
        _characterFaction = faction;
    }




}
