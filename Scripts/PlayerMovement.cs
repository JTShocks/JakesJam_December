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



    int moveSpeed {get; set;}
    float maxHealth {get; set;}
    public float currentHealth;
    float baseDamage {get; set;}
    public int playerLevel = 1;
    int levelCap = 6;

    bool isKnockedDown = false;


    [Export]
    public int playerMoney;

    Vector2 inputDirection;
    Timer getUpTimer;
    Camera2D playerCamera;


    public override void _Ready()
    {
        AddToGroup("Player");
        playerCamera = GetNode<Camera2D>("Camera2D");
        SetStats(characterData);
        getUpTimer = new Timer{
            OneShot = true
        };
        AddChild(getUpTimer);
        getUpTimer.Timeout += GetUp; // when the timer times out, the player gets back up


        EnemyKilled += GetMoney;

    }

    public override void _Process(double delta)
    {
        if(!isKnockedDown)
        {
        GetInput();
        this.MoveAndCollide(inputDirection * moveSpeed * (float)delta); 
        }
        Vector2 mousePos = GetGlobalMousePosition();
        playerCamera.GlobalPosition = this.GlobalPosition.Lerp(mousePos, 0.15f);
  
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
      
        if(currentHealth > 0)
        {
        currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            KnockDown();

        }
    }

    public void KnockDown()
    {

        getUpTimer.Start(10);
        isKnockedDown = true;

    }

    public void GetUp()
    {
        isKnockedDown = false;
        currentHealth = maxHealth/2;
    }



}
