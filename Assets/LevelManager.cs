using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Transform background;
	public int initialBalls;

	private ClickHandler clickHandler;
	Vector3 myWorld;
	Transform referenceBall;
	private List<BallType> currentBallTypes;
	private BallType backgroundType;
	
	BallManager ballManager;

	// Use this for initialization
	void Start () {
		clickHandler = gameObject.AddComponent<ClickHandler>();
		clickHandler.SphereClicked += new ClickHandler.SphereClickHandler (SphereClicked);
		ballManager = new BallManager();
		referenceBall = ((GameObject)Resources.Load("Sphere")).transform;

		SetupWorldSize ();
		CreateBorders ();

		CreateBalls ();
		SetBackgroundColor ();
	}

	void CreateBalls()
	{
		currentBallTypes = new List<BallType>();
		float minX = - myWorld.x + referenceBall.GetComponent<Collider2D>().bounds.size.x / 2;
		float maxX = myWorld.x - referenceBall.GetComponent<Collider2D>().bounds.size.x / 2;

		float minY = - myWorld.y + referenceBall.GetComponent<Collider2D>().bounds.size.y / 2;
		float maxY = myWorld.y - referenceBall.GetComponent<Collider2D>().bounds.size.y / 2;
		for(int i = 0; i < initialBalls; i++)
		{
			Vector3 ballPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
			Transform newBall = (Transform)Instantiate (referenceBall, ballPos, Quaternion.identity);
			BallType newType = ballManager.GetRndType();
			newBall.GetComponent<Clickable>().type = newType;
			newBall.GetComponent<Clickable>().SetSprite(ballManager.GetRndFace(newType));
			currentBallTypes.Add(newType);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel ("main_menu");
	}

	void SphereClicked(GameObject sphere)
	{
		if (sphere.GetComponent<Clickable>().type == backgroundType) {
			Destroy(sphere);
			currentBallTypes.Remove(backgroundType);
			if(currentBallTypes.Count > 0)
				SetBackgroundColor();
			else
				Application.LoadLevel ("main_menu");
		}
	}

	void SetBackgroundColor()
	{
		backgroundType = currentBallTypes [Random.Range (0, currentBallTypes.Count - 1)];
		background.GetComponent<Renderer>().material.color = ballManager.GetBackground(backgroundType);
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
