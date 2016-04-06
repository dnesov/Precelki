using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	Vector3 myWorld;

	public BallType type;

	Transform sphere;

	void Start () {
		GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (-150f, 150f), Random.Range (-300f, 300f) * 2));
	}
	
	public void SetSprite(Sprite sprite)
	{
		//transform.bounds.size.Set(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		transform.localScale = GetComponent<CircleCollider2D>().transform.localScale;
		//sprite.bounds.size.Set(GetComponent<CircleCollider2D>().bounds.size.x, GetComponent<CircleCollider2D>().bounds.size.y, GetComponent<CircleCollider2D>().bounds.size.z); //= GetComponent<CircleCollider2D>().bounds;
		GetComponent<SpriteRenderer>().sprite = sprite;
	}

	public void SetColor(Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
//		Destroy (this.gameObject);
	}

	void UpdateLife()
	{

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
			if (GetComponent<Rigidbody2D>().velocity.y < 10) {
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (-100f, 100f), Random.Range (500f, 700f)));
			}
		}
	}
}
