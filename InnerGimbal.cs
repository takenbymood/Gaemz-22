using Godot;
using System;

public class InnerGimbal : Spatial
{
	[Export]
	public float camera_rotation_speed = 1.0f;
	[Export]
	public float camera_zoom_speed = 1.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
