using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Transform background;
	public Transform menuBar;
	public Transform menuButton;
	
	public int initialBalls;
	private const float menuBarHeight = 2;
	private bool isPause = false;

	private ClickHandler clickHandler;
	private Transform referenceBall;
	private List<BallType> currentBallTypes;
	private BallType backgroundType;
	
	private BallManager ballManager;
	private Bounds gameBoard;

	// Use this for initialization
	void Start () {
		clickHandler = gameObject.AddComponent<ClickHandler>();
		clickHandler.SphereClicked += new ClickHandler.SphereClickHandler (SphereClicked);
		ballManager = new BallManager();
		referenceBall = ((GameObject)Resources.Load("Sphere")).transform;

		SetupWorldSize ();

		CreateBalls ();
		SetBackgroundColor ();
	}

	void CreateBalls()
	{
		currentBallTypes = new List<BallType>();
		
		for(int i = 0; i < initialBalls; i++)
			SpawnRandomBall();
	}
	
	void SpawnRandomBall() {
		Vector3 ballPos = new Vector3(Random.Range(gameBoard.min.x, gameBoard.max.x), Random.Range(gameBoard.min.y, gameBoard.max.y), 0);
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
		if (isPause)
			return;
		
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
		backgroundType = currentBallTypes [Random.Range (0, currentBallTypes.Count)];
		background.GetComponent<Renderer>().material.color = ballManager.GetBackground(backgroundType);
		background.GetComponent<SpriteRenderer>().sprite = ballManager.GetRndBackImg();
	}

	void SetupWorldSize()
	{
		Vector3 myScreen = new Vector3(Screen.width, Screen.height, 0);
		Vector3 myWorld = Camera.main.ScreenToWorldPoint(myScreen);
		
		gameBoard.SetMinMax(
			new Vector3(-myWorld.x, -myWorld.y, 0),
			new Vector3(myWorld.x, myWorld.y - menuBarHeight, 0));
			
		background.transform.localScale = new Vector3(myWorld.x*2, (myWorld.y - menuBarHeight * 0.5f)*2, 1.0f);
		background.transform.position = new Vector3(0, -(menuBarHeight * 0.5f), 6.5f);
		
		menuBar.transform.localScale = new Vector3(myWorld.x*2, menuBarHeight, 1.0f);
		menuBar.transform.position = new Vector3(0, gameBoard.max.y + menuBarHeight * 0.5f, 6.5f);
		
		menuButton.transform.position = new Vector3(gameBoard.max.x - 1, menuBar.transform.position.y);
		menuButton.transform.localScale = new Vector3(0.2f, 0.2f);
		
		gameObject.GetComponent<EdgeCollider2D>().points = new Vector2[] {
			new Vector2 (gameBoard.min.x, gameBoard.min.y),
			new Vector2 (gameBoard.max.x, gameBoard.min.y),
			new Vector2 (gameBoard.max.x, gameBoard.max.y),
			new Vector2 (gameBoard.min.x, gameBoard.max.y),
			new Vector2 (gameBoard.min.x, gameBoard.min.y)};
	}
	
	public void menuButtonClicked() {
		isPause = !isPause;
		if (isPause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
	
	void OnGUI() {
		if (isPause) {
			Vector3 leftTop = Camera.main.WorldToScreenPoint(new Vector3(gameBoard.min.x, gameBoard.min.y + menuBarHeight));
			Vector3 rightBottom = Camera.main.WorldToScreenPoint(new Vector3(gameBoard.max.x, gameBoard.max.y + menuBarHeight));
			GUI.Window(
				0,
				new Rect(leftTop.x, leftTop.y, rightBottom.x - leftTop.x, rightBottom.y - leftTop.y),
				displayPauseMenu,
				"Pause menu");
		}
	}
	
	void displayPauseMenu(int windowID) {
		//TODO pause menu contents
	}
}
