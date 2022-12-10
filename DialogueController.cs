using Godot;
using System;

public class DialogueController : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	Script dialogic = null;
	private bool chatting = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dialogic = ResourceLoader.Load("res://addons/dialogic/Other/DialogicClass.gd") as Script;
    }
	
	public void LoadDialog(String name){
		var dialog = (Node) dialogic.Call("start",name);	
		dialog.Connect("timeline_end", this, nameof(_on_timeline_end));
		AddChild(dialog);
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
	private void _on_timeline_end(String name)
	{
		chatting = false;
	}
	
}


