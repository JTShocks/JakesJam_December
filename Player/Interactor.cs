using Godot;
using System;
using System.Collections.Generic;

public partial class Interactor : Node
{
    public void OnEnterArea(PhysicsBody2D target)
    {
        if(target is ICollectable collectable)
        {
            collectable?.Collect();
        }
    }


    
}
