using Godot;
using System;

public partial class Gun_Weapon : Node2D, IDoDamage
{

    int baseDamage = 10;
    RayCast2D gunRay;
    public override void _Ready()
    {
        gunRay = GetNode<RayCast2D>("GunRay");
        base._Ready();
    }



    public void DoDamage(int damage)
    {
        if(damage == 0)
        {
            damage = baseDamage;
        }
        var coll = gunRay.GetCollider();
		if(coll is ITakeDamage target)
		{
			target?.TakeDamage(damage);
            if(coll is Enemy_Base enemy)
            {
                enemy.SetTarget(GetParent().GetParent() as CharacterBody2D);
            }
		}
		else
		{
			GD.Print("Didn't hit anything");
		}
        //CharacterData.currentHealth -= damage;
    }

}
