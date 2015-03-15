using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	Vector3 myWorld;

	Transform sphere;

	void Start () {
		rigidbody2D.AddForce (new Vector2 (Random.Range (-150f, 150f), Random.Range (-300f, 300f) * 2));
		//renderer.material.color = ClickHandler.colors [Random.Range (0, 3)];
		
		//Vector3 myScreen = new Vector3(Screen.width,Screen.height,0);
		//myWorld = Camera.main.ScreenToWorldPoint(myScreen);
		//sphere = ((GameObject)Resources.Load("Sphere")).transform;
		//life = Random.Range((int)maxLife-3, (int)maxLife);
		//rigidbody2D.AddForce (new Vector2 (Random.Range (-150f, 150f), Random.Range (-300f, 300f) * 2));
		//count+=1;
		//AdjustColor();
	}

	public void SetColor(Color color)
	{
		renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
//		time += Time.deltaTime;
//		if (time >= 1) {
//			UpdateLife();
//			time -= 1;
//		}
//		AdjustColor ();
	}

	void OnMouseDown()
	{
//		Destroy (this.gameObject);
	}

	void UpdateLife()
	{
//		life-=1;
//		if (life == 0) {
//			int newBalls = Random.Range (2, 4);
//			for (int i = 0; i < newBalls; i++) {
//				SpawnPrefab ();
//			}
//			Destroy (this.gameObject);
//		} 
//		Vector3 newScale = transform.localScale;
//		newScale.x *= 0.95f;
//		newScale.y *= 0.95f;
//		transform.localScale = newScale;

	}

	void AdjustColor()
	{
//		renderer.material.color = new Color (0.5f, life/maxLife, 0);
	}

	void SpawnPrefab()
	{
		//Transform newObject = (Transform)Instantiate (sphere, transform.position, Quaternion.identity);
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Floor") {
			if (rigidbody2D.velocity.y < 10) {
				rigidbody2D.AddForce (new Vector2 (Random.Range (-100f, 100f), Random.Range (500f, 700f)));
			}
		}
	}
}
