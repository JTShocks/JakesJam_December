using Godot;
using System;

public partial class BuildingStats : Resource
{

    [Export]
    public int BaseDamage {get; set;}
    [Export]
    public int BaseHealth {get; set;}
    [Export]
    public int BasePrice {get; set;}
    [Export]
    public int TotalBuildingValue {get; set;}

    public BuildingStats() : this(0, 0, 0) {}

    public BuildingStats(int damage, int health, int price)
    {
        BaseDamage = damage;
        BaseHealth = health;
        BasePrice = price;
    }



}
