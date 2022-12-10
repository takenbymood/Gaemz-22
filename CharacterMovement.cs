using Godot;
using System;

public class CharacterMovement : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	private Spatial camera_gimbal = null;
	private Spatial char_jiggler = null;
	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera_gimbal = GetNode<Spatial>("/root/Scene/CameraGimbal");
		char_jiggler = GetNode<Spatial>("Billboard/Jiggler");
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
	
	

	Random rnd = new Random();


    public void GetInput()
    {
		move_lock = false;
        velocity = new Vector3();

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
		
		velocity = velocity.Rotated(new Vector3(0,1,0),camera_gimbal.GetRotation().y);
		
        
    }
	
	public void MeshJiggle()
	{
		
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
				float xfac =jiggle_randomness*( (float) rnd.NextDouble());
				
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

}
