using Godot;
using System;


public partial class GameController : Node
{

    //Recieves the signal that the core is damaged and displays the Core Health
    //Recieves when the core is destroyed to load the game over screen

    public int currentCoreHealth;


    public void OnCoreHealthChanged(int newCoreHealth)
    {
        currentCoreHealth = newCoreHealth;
        //The UI will read in the game controller and get this update
        //Show player when the core is being attacked
    }

    public void CoreDestroyed()
    {
        //Load in the game over screen
        //That screen has two buttons, quit or restart
        //Restart = reload the previous main Level
        //Quit = close the application
    }
}
