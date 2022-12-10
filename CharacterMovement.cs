using Godot;
using System;

public class CharacterMovement : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	
	private Spatial camera_gimbal = null;
	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera_gimbal = GetNode<Spatial>("/root/Scene/CameraGimbal");
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
	
    public Vector3 velocity = new Vector3();
	
	

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
		
		
		intspeed = (int) Math.Round(speed);
		velocity = velocity.Normalized() * intspeed;
		
		
		
		velocity = MoveAndSlide(velocity);
    }

}
