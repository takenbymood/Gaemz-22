using Godot;
using System;

public class CharacterMovement : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	private Spatial camera_gimbal = null;
	private Spatial char_jiggler = null;
	private Area char_interact = null;
	private Spatial dialog_controller = null;
	
	[Signal] public delegate void dialogSignal(String name);

	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera_gimbal = GetNode<Spatial>("/root/Scene/CameraGimbal");
		char_jiggler = GetNode<Spatial>("Billboard/Jiggler");
		char_interact = GetNode<Area>("Interact");
		dialog_controller = GetNode<Spatial>("/root/Scene/DialogueController");
		rnd = new RandomNumberGenerator();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	[Export] public int max_speed = 40;
	[Export] public int acceleration = 500;
	
	
	private bool move_lock = false;
	private float speed = 0;
	private int intspeed = 0;
	
	private float jiggle_snap = 0.005f;
	private float jiggle_height = 0.0f;
	private float jiggle_gravity = 1800.0f;
	private float jiggle_speed = 0.0f;
	private float jiggle_accel = 0.0f;
	private float jiggle_jerk = 70.0f;
	private Vector3 jiggle_dir = new Vector3(0,1,0);
	private float jiggle_randomness = 0.5f;
	
	private bool jiggle_left = true;
	
    public Vector3 velocity = new Vector3();
	private bool interacting = false;
	
	private bool control_lock = false;
	

	RandomNumberGenerator rnd = null;


    public void GetInput()
    {
		move_lock = false;
		interacting = false;
        velocity = new Vector3();
		
		if(!control_lock){

	        if (Input.IsActionPressed("move_right")){
				move_lock = true;
	            velocity.x += 1;
			}

	        if (Input.IsActionPressed("move_left")){
				move_lock = true;
	            velocity.x -= 1;
			}

	        if (Input.IsActionPressed("move_down")){
				move_lock = true;
	            velocity.z += 1;
			}

	        if (Input.IsActionPressed("move_up")){
				move_lock = true;
	            velocity.z -= 1;
			}
			
			if (Input.IsActionPressed("interact")){
				interacting = true;
			}
		}
		
		velocity = velocity.Rotated(new Vector3(0,1,0),camera_gimbal.GetRotation().y);
		
        
    }
	
	public void MeshJiggle()
	{
		
	}
	
	public override void _Process(float delta){
		foreach(Area area in char_interact.GetOverlappingAreas()){
			if(area.GetParent().IsInGroup("computer")){
				if(interacting){
					EmitSignal(nameof(dialogSignal),"Test");
				}
			}
		}
	}

    public override void _PhysicsProcess(float delta)
    {
		
        GetInput();
		
				
		if(move_lock){
			speed = speed + ((float) acceleration)*delta;
			if(speed > max_speed){
				speed = max_speed;
			}
		}
		else if(!move_lock&&speed >0.0f){
				speed = speed - ((float) acceleration)*delta;
				if(speed < 0.0f){
					speed = 0.0f;
				}
			}
		else if(!move_lock&&speed < 0.0f){ speed = 0.0f; }
		
		if(jiggle_height < jiggle_snap){
			jiggle_height = 0;
			jiggle_speed = 0.0f;
			jiggle_accel = 0.0f;
		}

		
		if(speed >= max_speed){
			if(jiggle_height < jiggle_snap){
				jiggle_accel = jiggle_jerk;
				float xfac =jiggle_randomness*( (float) rnd.Randf());
				
				if(jiggle_left){
					xfac = -xfac;
					jiggle_left = false;
				}
				else{
					jiggle_left = true;
				}
				jiggle_dir = new Vector3(xfac,1,0).Normalized();
			}
		}
		jiggle_speed = jiggle_speed + jiggle_accel*delta;
		jiggle_height += jiggle_speed*delta;
		jiggle_accel -= jiggle_gravity*delta;
		
		
		char_jiggler.Translate(jiggle_speed*0.1f*jiggle_dir);
		if(char_jiggler.Transform.origin.y < 0.0f){
			char_jiggler.Translate(new Vector3(0,-char_jiggler.Transform.origin.y,0));
			
		}
		
		
		intspeed = (int) Math.Round(speed);
		velocity = velocity.Normalized() * intspeed;
		
		
		
		velocity = MoveAndSlide(velocity);
    }
	
	private void _on_DialogueController_lockControls()
	{
	    control_lock = true;
	}


	private void _on_DialogueController_unlockControls()
	{
	    control_lock = false;
	}

}



