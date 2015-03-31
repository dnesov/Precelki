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
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

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
		
		minX = - myWorld.x + referenceBall.GetComponent<Collider2D>().bounds.size.x / 2;
		maxX = myWorld.x - referenceBall.GetComponent<Collider2D>().bounds.size.x / 2;
		
		minY = - myWorld.y + referenceBall.GetComponent<Collider2D>().bounds.size.y / 2;
		maxY = myWorld.y - referenceBall.GetComponent<Collider2D>().bounds.size.y / 2;
		
		for(int i = 0; i < initialBalls; i++)
		{
			SpawnRandomBall();
		}
	}
	void SpawnRandomBall() {
		Vector3 ballPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
		BallType type = ballManager.GetRndType();
		SpawnBall (ballPos, type);
	}
	
	void SpawnBall(Vector3 position, BallType type) {
		Transform newBall = (Transform)Instantiate (referenceBall, position, Quaternion.identity);
		
		newBall.GetComponent<Clickable>().type = type;
		newBall.GetComponent<Clickable>().SetSprite(ballManager.GetRndFace(type));
		currentBallTypes.Add(type);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel ("main_menu");
	}

	void SphereClicked(GameObject sphere)
	{
		Vector3 position = sphere.transform.position;
		BallType type = sphere.GetComponent<Clickable>().type;
		currentBallTypes.Remove(type);
		Destroy(sphere);
		
		if (type == backgroundType) {
			if(currentBallTypes.Count > 0)
				SetBackgroundColor();
			else
				Application.LoadLevel ("main_menu");
		} else {
			SpawnBall(position, type);
			SpawnBall(position, type);
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
