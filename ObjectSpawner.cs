using Godot;
using System;

public class ObjectSpawner : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	private Spatial self = null;
	RandomNumberGenerator rnd = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		self = GetNode<Spatial>(".");
		rnd = new RandomNumberGenerator();
    }
	
	
	private void _on_DialogueController_objectSpawnSignal(String name)
	{
	    // Replace with function body.
		if(name.Equals("test")){
			PackedScene testScene = (PackedScene)ResourceLoader.Load("res://Scenes/PhysicsObject.tscn");
			RigidBody newTest = (RigidBody)testScene.Instance();
			AddChild(newTest);
			newTest.Translate(new Vector3(2.0f-4.0f*rnd.Randf(),0,2.0f*rnd.Randf()));
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


