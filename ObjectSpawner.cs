using Godot;
using System;

public class ObjectSpawner : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }
	
	
	private void _on_DialogueController_objectSpawnSignal(String name)
	{
	    // Replace with function body.
		GD.Print("make me an object!");
		GD.Print(name);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


