using Godot;
using System;

public class Main : Node2D
{
	private TileMap tileMap;
	private readonly Random random = new Random(Convert.ToInt32(Time.GetUnixTimeFromSystem()));

	[Export] private readonly int playfieldsize = 64;
	[Export] private readonly float whiteChance = 0.2f;
	[Export] private readonly bool generateLife = true;

	private const float timer = 0.05f;
	private float timeSpend = 0f;
	private float lastUpdate = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() => tileMap = GetNode(new NodePath("TileMap")) as TileMap;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("reload")) tileMap.Clear();

		timeSpend += delta;

		if (timeSpend < lastUpdate + timer) return;

		TileMap tempMap = tileMap.Duplicate() as TileMap;
		lastUpdate = timeSpend;

		for (int x = -playfieldsize; x < playfieldsize; x++)
		{
			for (int y = -playfieldsize; y < playfieldsize; y++)
			{
				tempMap.SetCell(x, y, ShouldLive(x, y));
			}
		}

		RemoveChild(tileMap);
		tileMap.QueueFree();

		AddChild(tempMap);
		tileMap = tempMap;
	}

	private int ShouldLive(int x, int y)
	{
		int tile = tileMap.GetCell(x, y);
		if (tile == -1) return random.NextDouble() < whiteChance && generateLife ? 1 : 0; //This line initializes an empty board

		int cellNeighbours = 0;

		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (i == 0 && j == 0) continue;
				cellNeighbours += tileMap.GetCell(x + i, y + j) == 1 ? 1 : 0;

				if (cellNeighbours > 3) return 0;
			}
		}
		
		if (cellNeighbours == 3 || (tile == 1 && cellNeighbours == 2)) return 1;
		return 0;
	}
}
