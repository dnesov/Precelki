using UnityEngine;
using System.Collections;

public class RotationAction : MonoBehaviour {

	float rotDir; // -1; 1
	float rotSpeed;

	float getRndDir() {
		return -1.0f + 2.0f * Random.Range(0.0f, 1.0f);
	}

	// Use this for initialization
	void Start () {
		rotDir = getRndDir();
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rbody = this.GetComponent<Rigidbody2D>();
		if( !rbody )
			return;
			
		this.transform.Rotate(rotDir * Vector3.forward * Time.deltaTime * rbody.velocity.magnitude * rotSpeed);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if( coll.transform.tag != "sphere" ) {
			rotDir = getRndDir();
			rotSpeed = Random.Range(5.0f, 20.0f);
		}
	}
}
