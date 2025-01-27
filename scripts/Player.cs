using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private Vector2 currentVelocity;
	[Export]
	private int speed = 100;
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		handleInput();

		Velocity = currentVelocity;
		MoveAndSlide();

	}

	private void handleInput()
	{
		currentVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		currentVelocity *= speed;
	}
}
