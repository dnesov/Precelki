using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Transform background;
	public int initialBalls;

	private ClickHandler clickHandler;
	private Vector3 myWorld;
	private Transform referenceBall;
	private List<BallType> currentBallTypes;
	private BallType backgroundType;
	
	private BallManager ballManager;

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
		Vector3 myScreen = new Vector3(Screen.width, Screen.height, 0);
		myWorld = Camera.main.ScreenToWorldPoint(myScreen);
		background.transform.localScale = new Vector3(myWorld.x*2, myWorld.y*2, 1.0f);
	}

	void CreateBorders()
	{
		Vector2[] points = new Vector2[] { new Vector2 (-myWorld.x, -myWorld.y),
			new Vector2 (myWorld.x, -myWorld.y),
			new Vector2 (myWorld.x, myWorld.y),
			new Vector2 (-myWorld.x, myWorld.y),
			new Vector2 (-myWorld.x, -myWorld.y)};
		gameObject.GetComponent<EdgeCollider2D>().points = points;
	}
}
