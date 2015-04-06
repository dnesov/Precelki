using UnityEngine;
using System.Collections;

public class ButtonEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit();
	}

	void OnMouseDown() {
		switch (gameObject.name) {
		case "play":
			Application.LoadLevel ("s1");
			break;
		case "quit":
			Application.Quit();
			break;
		case "MenuButton":
			GameObject.FindObjectOfType<LevelManager>().menuButtonClicked();
			break;
		default:
			break;
		}
	}
}
