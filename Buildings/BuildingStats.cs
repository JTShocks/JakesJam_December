using Godot;
using System;

public partial class BuildingStats : Resource
{

    [Export]
    public int BaseDamage {get; set;}
    [Export]
    public int BaseHealth {get; set;}

    public BuildingStats() : this(0, 0) {}

    public BuildingStats(int damage, int health)
    {
        BaseDamage = damage;
        BaseHealth = health;
    }



}
