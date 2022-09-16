using Godot;

public class CamControl : Camera2D
{
	[Export] private readonly float speed = 1f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
        if (Input.IsActionJustPressed("zoom_in")) Zoom -= new Vector2(0.1f, 0.1f);

        if (Input.IsActionJustPressed("zoom_out")) Zoom += new Vector2(0.1f, 0.1f);

        if (Input.IsActionPressed("ui_up")) Position += new Vector2(0f, -speed * delta);

        if (Input.IsActionPressed("ui_down")) Position += new Vector2(0f, speed * delta);

        if (Input.IsActionPressed("ui_left")) Position += new Vector2(-speed * delta, 0f);

        if (Input.IsActionPressed("ui_right")) Position += new Vector2(speed * delta, 0f);
    }
}
