using UnityEngine;
using System.Collections;

public class MenuAnimation : MonoBehaviour {

	public GameObject blueFace;
	public GameObject greenFace;
	public GameObject orangeFace;
	public GameObject redFace;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		blueFace.transform.localScale = new Vector3(0.3f, 0.3f, 0) * lerp(1.0f);
		greenFace.transform.localScale = new Vector3(0.3f, 0.3f, 0) * lerp(0.0f);
		orangeFace.transform.localScale = new Vector3(0.3f, 0.3f, 0) * lerp(1.0f);
		redFace.transform.localScale = new Vector3(0.3f, 0.3f, 0) * lerp(0.0f);
	}
	
	private float lerp(float offset) {
		return (Mathf.PingPong(Time.time + offset, 1.0f)) * 0.7f + 0.5f;
	}
}
