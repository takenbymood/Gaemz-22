using Godot;
using System;

public class DialogueController : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	Script dialogic = null;
	private bool chatting = false;
	
	[Signal] public delegate void objectSpawnSignal(String name);
	[Signal] public delegate void lockControls();
	[Signal] public delegate void unlockControls();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dialogic = ResourceLoader.Load("res://addons/dialogic/Other/DialogicClass.gd") as Script;
    }
	
	public void LoadDialog(String name){
		var dialog = (Node) dialogic.Call("start",name);	
		dialog.Connect("timeline_end", this, nameof(_on_timeline_end));
		dialog.Connect("dialogic_signal", this, nameof(_dialogic_event));
		AddChild(dialog);
		EmitSignal(nameof(lockControls));
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void _on_Character_dialogSignal(String name)
	{
		if(!chatting){
	    	LoadDialog(name);
			chatting = true;
		}
	}
	
	private void _spawn_object(String object_name){
		EmitSignal(nameof(objectSpawnSignal),object_name);
	}
	
	private void _dialogic_event(String arg){
		String type = arg.Split("_")[0];
		if(type.Equals("spawnobject")){
			if(arg.Split("_").Length > 1){
				_spawn_object(arg.Split("_")[1]);
			}
		}
	}
	
	private void _on_timeline_end(String name)
	{
		chatting = false;
		EmitSignal(nameof(unlockControls));
	}
	
}


