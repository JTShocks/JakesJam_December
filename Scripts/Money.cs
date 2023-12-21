using Godot;
using System;

public partial class Money : CharacterBody2D, ICollectable
{

    [Export]
    public int moneyValue {get; set;}

    public void Collect()
    {

        //Send out a call to the "player" group to "add_money" of value moneyValue
        QueueFree(); // Remove the money when collected
    }

}
