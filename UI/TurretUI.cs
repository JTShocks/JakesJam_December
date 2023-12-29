using Godot;
using System;

public partial class TurretUI : Control
{

    [Export]
    public Turret_Base turret;

    [Export]
    VBoxContainer turretHUD;
    RichTextLabel health;
    RichTextLabel upgradeCost;
    RichTextLabel turretLevel;

    public override void _Ready()
    {
        base._Ready();
        health = turretHUD.GetNode<RichTextLabel>("Health");
        upgradeCost = turretHUD.GetNode<RichTextLabel>("UpgradeCost");
        turretLevel = turretHUD.GetNode<RichTextLabel>("Level");


    }

    public void UpdateDisplay()
    {
        turretLevel.Text = "Level: " + turret.buildingLevel.ToString();
        health.Text = "Health: " + turret.currentHealth.ToString();
        upgradeCost.Text = "To Upgrade: " + turret.upgradePrice.ToString();

    }

    public void EnableDisplay()
    {
        this.Visible = true;
        UpdateDisplay();
    }
    public void DisableDisplay()
    {
        this.Visible = false;
    }
}
