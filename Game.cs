using Godot;
using System.Collections.Generic;

public partial class Game : Node
{
	private readonly Dictionary<int, PlayerComponents> _players = new Dictionary<int, PlayerComponents>();

	public override void _Ready()
	{
		GD.Print("I am ready!!!!");
		// Initialize player components
		_players[1] = new PlayerComponents(
			GetNode<Viewport>("HBoxContainer/SubViewportContainer/SubViewport"),
			GetNode<Camera2D>("HBoxContainer/SubViewportContainer/SubViewport/Camera2D"),
			GetNode<Node2D>("HBoxContainer/SubViewportContainer/SubViewport/Level/Player1")
		);

		_players[2] = new PlayerComponents(
			GetNode<Viewport>("HBoxContainer/SubViewportContainer2/SubViewport"),
			GetNode<Camera2D>("HBoxContainer/SubViewportContainer2/SubViewport/Camera2D"),
			GetNode<Node2D>("HBoxContainer/SubViewportContainer2/SubViewport/Level/Player2")
		);

		// Share the World2D between viewports
		_players[2].Viewport.World2D = _players[1].Viewport.World2D;

		// Add RemoteTransform2D to player nodes
		foreach (var player in _players.Values)
		{
			var remoteTransform = new RemoteTransform2D
			{
				RemotePath = player.Camera.GetPath()
			};
			player.PlayerNode.AddChild(remoteTransform);
		}
	}

	private class PlayerComponents
	{
		public Viewport Viewport { get; }
		public Camera2D Camera { get; }
		public Node2D PlayerNode { get; }

		public PlayerComponents(Viewport viewport, Camera2D camera, Node2D playerNode)
		{
			Viewport = viewport;
			Camera = camera;
			PlayerNode = playerNode;
		}
	}
}
