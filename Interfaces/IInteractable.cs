using Godot;
using System;

public interface IInteractable
{
    void Interact();
    void Interact(Interactor interactor);
}
