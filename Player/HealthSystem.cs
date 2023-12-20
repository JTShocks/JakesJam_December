using Godot;
using System;

public partial class HealthSystem : Node
{
    [Signal]
    public delegate void OnTakeDamageEventHandler();
    [Export]
    public int _maxHealth {get; private set;} = 100;
    
    public int _currentHealth;

    public override void _Ready()
    {
        base._Ready();
        _currentHealth = _maxHealth;

    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        EmitSignal(SignalName.OnTakeDamage);
        GD.Print(_currentHealth);
    }

}
