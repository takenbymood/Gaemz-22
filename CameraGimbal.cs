using Godot;
using System;

public class CameraGimbal : Spatial
{
	[Export]
	public float max_camera_rotation_speed = 0.1f;
	[Export]
	public float max_camera_zoom_speed = 1.0f;
	[Export]
	public float rotation_snap = 0.005f;
	[Export]
	public float zoom_snap = 0.05f;
	[Export]
	public float camera_rotation_accel = 50.0f;
	[Export]
	public float camera_move_speed = 1.0f;
	[Export]
	public float camera_move_snap = 0.005f;
	
	private float camera_rotation_speed = 0.0f;

	private float inv_camera_rotation_accel = 1.0f;
	private float target_camera_rotation_speed = 0.0f;
	
	
	private float _t = 0.0f;
	
	private bool left_lock = false;
	private bool right_lock = false;
	private float lock_time = 0.0f;
	
	private Spatial self = null;
	private Spatial player = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		self = GetNode<Spatial>(".");
		player = GetNode<Spatial>("/root/Scene/Character");
		
		inv_camera_rotation_accel = 1.0f/camera_rotation_accel;
		
        Vector3 v = player.GetGlobalTransform().origin;
		self.Translation = v;
    }
	
		
	public override void _Input(InputEvent inputEvent)
	{
	    if (inputEvent.IsActionPressed("ui_left"))
	    {
	        left_lock = true;
			lock_time = _t;
			target_camera_rotation_speed = -max_camera_rotation_speed;
	    }
		if (inputEvent.IsActionReleased("ui_left"))
	    {
	        left_lock = false;
			lock_time = _t;
			target_camera_rotation_speed = right_lock ? max_camera_rotation_speed : 0.0f;
	    }
		if (inputEvent.IsActionPressed("ui_right"))
	    {
	        right_lock = true;
			lock_time = _t;
			target_camera_rotation_speed = max_camera_rotation_speed;
	    }
		if (inputEvent.IsActionReleased("ui_right"))
	    {
	        right_lock = false;
			lock_time = _t;
			target_camera_rotation_speed = left_lock ? -max_camera_rotation_speed : 0.0f;
	    }
	}
	
	public override void _PhysicsProcess(float delta)
	{
		float a_t = _t + camera_rotation_accel*(delta * 0.4f);
		_t += delta * 0.4f;
		
		camera_rotation_speed = camera_rotation_speed + (target_camera_rotation_speed - camera_rotation_speed)*(a_t-lock_time);
		
		
		if(camera_rotation_speed < -max_camera_rotation_speed)
		{
			camera_rotation_speed = - max_camera_rotation_speed;
		}
		if(camera_rotation_speed > max_camera_rotation_speed)
		{
			camera_rotation_speed = max_camera_rotation_speed;
		}
		if(Math.Abs(camera_rotation_speed) < rotation_snap) {
			camera_rotation_speed = 0.0f;
		}
		
		RotateY(camera_rotation_speed);
		
		Vector3 v = player.GetGlobalTransform().origin;
		
		
		
		self.Translation = v;
		
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	
  }
}
