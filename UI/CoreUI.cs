using Godot;
using System;

public partial class CoreUI : Control
{

    public void _on_core_mouse_entered()
    {
        Visible = true;
    }

    public void _on_core_mouse_exited()
    {
        Visible = false;
    }

}
