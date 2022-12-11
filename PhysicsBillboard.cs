using Godot;
using System;

public class PhysicsBillboard : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	private Spatial self = null;
	private RigidBody rigidParent = null;
	private Spatial camera_gimbal = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera_gimbal = GetNode<Spatial>("/root/Scene/CameraGimbal");
		self = GetNode<Spatial>(".");
		rigidParent = GetNode<RigidBody>("..");
		self.SetRotation(camera_gimbal.GetRotation());
		
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		self.SetRotation(camera_gimbal.GetRotation());
  }
}
