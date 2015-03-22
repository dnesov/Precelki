using UnityEngine;
using System.Collections;

public class NaturalBounceAction : MonoBehaviour {

	enum BounceState { NoBounce, Attack, Decay, Release };
	enum BounceAxis { Horizontal, Vertical };

	public float AlfaToForce = 0.5f;
	public float Hardness = 100.0f;
	public float ContactTime = 1.0f;
	
	Vector2 keptCenterOfMass;
	Vector2 keptVelocity;
	
	float keptAngularVelocity;
	float keptScale;
	
	BounceAxis bounceAxis;
	BounceState curState;
	float maxSqueeze;
	float startTime;
	
	bool isCollExit;
	
	// Use this for initialization
	void Start () {
		curState = BounceState.NoBounce;
		rigidbody2D.AddForce(Vector3.left * 600);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Escape) ) {
			Application.Quit();
		}
		
		Vector3 tmp;
		//Debug.Log (curState + " " + (Time.time - startTime) + " " + ContactTime);
		
		switch(curState) {
			case BounceState.Attack:
			tmp = transform.parent.transform.localScale;
				Debug.Log(bounceAxis);
				if(bounceAxis == BounceAxis.Horizontal)
					tmp.x = Mathf.Lerp(keptScale, maxSqueeze, (Time.time - startTime) / (ContactTime/2.0f));
				else
					tmp.y = Mathf.Lerp(keptScale, maxSqueeze, (Time.time - startTime) / (ContactTime/2.0f));
				
			transform.parent.transform.localScale = tmp;
				
				if( curState == BounceState.Attack && (Time.time - startTime) >= ContactTime / 2.0f ) {
					curState = BounceState.Decay;
					
					startTime = Time.time;
				}
					
				break;
				
			case BounceState.Decay:
			tmp = transform.parent.transform.localScale;
				
				if(bounceAxis == BounceAxis.Horizontal)
					tmp.x = Mathf.Lerp(maxSqueeze, keptScale, (Time.time - startTime) / (ContactTime/2.0f));
				else
					tmp.y = Mathf.Lerp(maxSqueeze, keptScale, (Time.time - startTime) / (ContactTime/2.0f));
				
			transform.parent.transform.localScale = tmp;
				
				if(Time.time - startTime >= ContactTime )
					curState = BounceState.Release;
					
				break;
				
			case BounceState.Release:
				ReleaseEnergy();
				
				if(isCollExit) {
					curState = BounceState.NoBounce;
					isCollExit = false;
				}
				break;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
	
		if(curState != BounceState.NoBounce)
			return;
			
		Debug.Log("Collision Enter");
	
		ConserveEnergy();
	
		startTime = Time.time;
	
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.gravityScale = 0;
				
		curState = BounceState.Attack;
		
		//rigidbody2D.centerOfMass = coll.contacts[0].point;
		
		//Debug.Log(rigidbody2D.centerOfMass);
		
		if( Mathf.Abs(coll.contacts[0].point.x - transform.position.x) > 
		   Mathf.Abs(coll.contacts[0].point.y - transform.position.y) ) {
			 	bounceAxis = BounceAxis.Horizontal;
			keptScale = transform.parent.transform.localScale.y;
			 } else {
				bounceAxis = BounceAxis.Vertical;
			keptScale = transform.parent.transform.localScale.x;
		}
		
		maxSqueeze = 0.5f * keptScale;
	}
	
	void OnCollisionExit2D(Collision2D coll) {
		isCollExit = true;
	}
	
	void ConserveEnergy() {
		keptCenterOfMass = rigidbody2D.centerOfMass;
		keptVelocity = rigidbody2D.velocity;
		keptAngularVelocity = rigidbody2D.angularVelocity;
	}
	
	void ReleaseEnergy() {
		rigidbody2D.centerOfMass = keptCenterOfMass;
		rigidbody2D.angularVelocity = keptAngularVelocity;
		rigidbody2D.gravityScale = 1;
		rigidbody2D.velocity = keptVelocity;
	}
}
