using Godot;
using System;

public partial class Gun_Weapon : Node2D, IDoDamage
{

    [Signal]
    public delegate void OnWeaponFiredEventHandler();
    int baseDamage = 10;
    [Export]
    public CpuParticles2D gunshot;
    [Export]
    public CpuParticles2D onHiteffect;
    RayCast2D gunRay;
    public override void _Ready()
    {
        gunRay = GetNode<RayCast2D>("GunRay");
        base._Ready();
    }



    public void DoDamage(int damage)
    {
        EmitSignal(SignalName.OnWeaponFired);
        gunshot.Emitting = true;
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
