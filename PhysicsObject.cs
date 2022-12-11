using Godot;
using System;

public class PhysicsObject : RigidBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	private RigidBody self = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        self = GetNode<RigidBody>(".");
    }
	
	 public override void _IntegrateForces(PhysicsDirectBodyState state){
		self.SetRotation(new Vector3(0,0,0));
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
