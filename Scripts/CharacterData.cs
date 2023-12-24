using Godot;
using System;

public partial class CharacterData : Resource
{
	[Export]
	public Faction _characterFaction;
	[Export]
	public float MaxHealth {get; set;}
    [Export]
    public int MoveSpeed {get; set;}
    [Export]
    public float BaseDamage {get; set;}


    public CharacterData() : this(0, 400, 0){}

    public CharacterData(float health, int moveSpeed, float baseDamage)
    {
        MaxHealth = health;
        MoveSpeed = moveSpeed;
        BaseDamage = baseDamage;
    }




}
