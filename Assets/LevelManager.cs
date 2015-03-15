using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Color[] colors;
	public Transform background;
	public int initialBalls;

	private ClickHandler clickHandler;
	int currentBalls;
	Vector3 myWorld;
	Transform ball;

	int[] colorsCounter;

	// Use this for initialization
	void Start () {
		clickHandler = gameObject.AddComponent<ClickHandler>() as ClickHandler;
		clickHandler.SphereClicked += new ClickHandler.SphereClickHandler (SphereClicked);

		ball = ((GameObject)Resources.Load("Sphere")).transform;

		SetupWorldSize ();
		CreateBorders ();

		colorsCounter = new int[colors.Length];
		CreateBalls ();
		SetBackgroundColor ();
		//background.renderer.material.color = colors[Random.Range(0,colors.Length)];
	}

	void CreateBalls()
	{

		float minX = - myWorld.x + ball.collider2D.bounds.size.x / 2;
		float maxX = myWorld.x - ball.collider2D.bounds.size.x / 2;

		float minY = - myWorld.y + ball.collider2D.bounds.size.y / 2;
		float maxY = myWorld.y - ball.collider2D.bounds.size.y / 2;
		for(int i = 0; i < initialBalls; i++)
		{
			Vector3 ballPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
			Transform newBall = (Transform)Instantiate (ball, ballPos, Quaternion.identity);
			int newColor = Random.Range(0, colors.Length);
			colorsCounter[newColor]++;
			(newBall.GetComponent<Clickable>() as Clickable).SetColor(colors[newColor]);
		}
		currentBalls = initialBalls;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SphereClicked(GameObject sphere)
	{
		if (sphere.renderer.material.color == background.renderer.material.color) {
			colorsCounter[System.Array.IndexOf(colors, sphere.renderer.material.color)]--;
			Destroy(sphere);
			currentBalls--;
			if(currentBalls > 0)
				SetBackgroundColor();

		}

		print ("bah!");
	}

	void SetBackgroundColor()
	{
		int newColorIndex;// =;
		do {
			newColorIndex = Random.Range (0, colorsCounter.Length);
		} while(colorsCounter[newColorIndex] == 0);
		background.renderer.material.color = colors [newColorIndex];
	}

	void SetupWorldSize()
	{
		Vector3 myScreen = new Vector3(Screen.width,Screen.height,0);
		myWorld = Camera.main.ScreenToWorldPoint(myScreen);
	}

	void CreateBorders()
	{
		float verticalHeight = myWorld.y * 2;
		float horizontalHeight = myWorld.x * 2;
		float width = 1;

		Transform border = ((GameObject)Resources.Load("Border")).transform;

		Vector3 leftPos = new Vector3 (-myWorld.x - width/2, 0, 0);
		Vector3 rightPos = new Vector3 (myWorld.x + width/2, 0, 0);
		Vector3 upPos = new Vector3 (0, myWorld.y + width/2, 0);
		Vector3 downPos = new Vector3 (0, -myWorld.y - width/2, 0);

		Transform left = (Transform)Instantiate (border, leftPos, Quaternion.identity);
		Transform right = (Transform)Instantiate (border, rightPos, Quaternion.identity);
		Transform up = (Transform)Instantiate (border, upPos, Quaternion.identity);
		Transform down = (Transform)Instantiate (border, downPos, Quaternion.identity);

		Vector3 verticalSize = new Vector3 (width, verticalHeight, 1);
		Vector3 horizontalSize = new Vector3 (width, horizontalHeight, 1);

		left.localScale = verticalSize;
		right.localScale = verticalSize;
		up.localScale = horizontalSize;
		down.localScale = horizontalSize;

		up.Rotate(new Vector3(0,0,90));
		down.Rotate(new Vector3(0,0,90));






		//left.renderer.bounds.size = verticalSize;

	}

//	public Color BackgroundColor
//	{
//		get 
//		{
//			return background.renderer.material.color;
//		}
//
//		set
//		{
//			background.renderer.material.color = value;
//		}
//	}
//	public static Color getBackgroundColor()
//	{
//		return background.renderer.material.color;
//	}
}
