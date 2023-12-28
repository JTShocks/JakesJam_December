using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D, ITakeDamage
{
    [Export]
    public CharacterData characterData;

    [Signal]
    public delegate void EnemyKilledEventHandler(int value);

    [Signal]
    public delegate void UpdateWeaponEventHandler(int newDamage);

    [Signal]
    public delegate void OnPlayerDeathEventHandler();



    int moveSpeed {get; set;}
    float maxHealth {get; set;}
    public float currentHealth;
    float baseDamage {get; set;}
    public int playerLevel = 1;
    int levelCap = 6;


    [Export]
    public int playerMoney;

    Vector2 inputDirection;


    public override void _Ready()
    {
        AddToGroup("Player");
        SetStats(characterData);


        EnemyKilled += GetMoney;

    }

    public override void _Process(double delta)
    {
        GetInput();
        this.MoveAndCollide(inputDirection * moveSpeed * (float)delta);   
    }

    public void GetInput()
    {
        inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        //this.Velocity = inputDirection * moveSpeed;

    }

    public void GetMoney(int value)
    {
        playerMoney += value;
    }
    public void LoseMoney(int value)
    {
        playerMoney -= value;
        if(playerMoney < 0)
        {
            playerMoney = 0;
        }
    }

    public void LevelUp()
    {
        if(playerLevel != levelCap)
        {
            
            playerLevel++;
            GD.Print("Player leveled up to: " + playerLevel);
            float newHealth = (float)playerLevel/10 * 100;
            float newDamage =  10 * (1/(float)playerLevel);
            if(playerLevel == 6)
            {
                newDamage = 3;
            }
            characterData.MaxHealth += newHealth;
            characterData.MoveSpeed += 40;
            characterData.BaseDamage += (int)newDamage;
            SetStats(characterData);


        }

    }
    public void SetStats(CharacterData stats)
    {
        moveSpeed = stats.MoveSpeed;
        maxHealth = stats.MaxHealth;
        baseDamage = stats.BaseDamage;
            GD.Print("Max Health: " + maxHealth);
            GD.Print("Movespeed: " + moveSpeed);
            GD.Print("Base Damage: " + baseDamage);
                    currentHealth = maxHealth;
            EmitSignal(SignalName.UpdateWeapon, baseDamage);
            

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Kill();

        }
    }

    public void Kill()
    {
        EmitSignal(SignalName.OnPlayerDeath);
        QueueFree();
    }



}
