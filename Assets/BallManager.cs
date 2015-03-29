using UnityEngine;
using System.Collections.Generic;
using System;


public enum BallType {
	Red, Blue, Green, Orange
}

class BallData {
	public Sprite[] faces {get; set;}
	public Color background {get; set;}
	
	public BallData(string spriteName, Color backCol) {
		faces = Resources.LoadAll<Sprite> (spriteName);
		background = backCol;
	}
}

public class BallManager
{
	private Dictionary<BallType, BallData> ColorMap;

	public BallManager() {
		ColorMap = new Dictionary<BallType, BallData>();
		Load (BallType.Red, "Sprites/faces/red_faces", 0xff9c8f);
		Load (BallType.Blue, "Sprites/faces/blue_faces", 0x7fb6d9);
		Load (BallType.Green, "Sprites/faces/green_faces", 0xb1f3c3);
		Load (BallType.Orange, "Sprites/faces/orange_faces", 0xffcd8f);
	}
	
	public void Load(BallType key, string spriteName, int backCol) {
		Color color = new Color((backCol >> 16) / 256.0f, ((0x00ff00 & backCol) >> 8) / 256.0f, (0x0000ff & backCol) / 256.0f);
		ColorMap[key] = new BallData(spriteName, color);
	}
	
	public Color GetBackground(BallType type) {
		return ColorMap[type].background;
	}
	
	public Sprite GetRndFace(BallType type) {
		int index = UnityEngine.Random.Range(0, ColorMap[type].faces.Length);
		return ColorMap[type].faces[index];
	}
	
	public BallType GetRndType()
	{
		var enumValues = Enum.GetValues(typeof(BallType));
		return (BallType)enumValues.GetValue(UnityEngine.Random.Range(0, enumValues.Length));
	}
}
