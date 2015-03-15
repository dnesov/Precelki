using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {

	float radius = 0.45f;
	//
	public event SphereClickHandler SphereClicked;
	public delegate void SphereClickHandler(GameObject sphere);
		
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D col = Physics2D.OverlapCircle (new Vector2 (pos.x, pos.y), radius);
			if(col && col.tag == "Sphere")
			{
		//if(col.renderer.material.color == LevelManager.BackgroundColor)
				SphereClicked(col.gameObject);
					//Destroy (col.gameObject);
					//	background.renderer.material.color = colors[Random.Range(0,3)];
	
			}
		}
	}
}
