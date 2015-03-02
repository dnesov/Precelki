using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	Vector3 myWorld;
	public int maxBalls;
	static int count = 1;
	public float maxLife;
	float life;
	float time = 0;
	Transform sphere;

	// Use this for initialization
	void Start () {
		//Vector3 myScreen = new Vector3(Screen.width,Screen.height,0);
		//myWorld = Camera.main.ScreenToWorldPoint(myScreen);
		sphere = ((GameObject)Resources.Load("Sphere")).transform;
		life = Random.Range((int)maxLife-3, (int)maxLife);
		rigidbody2D.AddForce (new Vector2 (Random.Range (-150f, 150f), Random.Range (-300f, 300f) * 2));
		count+=1;
		AdjustColor();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1) {
			UpdateLife();
			time -= 1;
		}
		AdjustColor ();
	}

	void OnMouseDown()
	{
		Destroy (this.gameObject);
	}

	void UpdateLife()
	{
		life-=1;
		if (life == 0) {
			int newBalls = Random.Range (2, 4);
			for (int i = 0; i < newBalls; i++) {
				SpawnPrefab ();
			}
			Destroy (this.gameObject);
		} 
		Vector3 newScale = transform.localScale;
		newScale.x *= 0.9f;
		newScale.y *= 0.9f;
		transform.localScale = newScale;

	}

	void AdjustColor()
	{
		renderer.material.color = new Color (0.5f, life/maxLife, 0);
	}

	void SpawnPrefab()
	{
		Transform newObject = (Transform)Instantiate (sphere, transform.position, Quaternion.identity);
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Floor") {
			if (rigidbody2D.velocity.y < 5) {
				rigidbody2D.AddForce (new Vector2 (Random.Range (-100f, 100f), Random.Range (500f, 700f)));
			}
		}
	}
}
