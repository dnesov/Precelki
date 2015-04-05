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
		blueFace.transform.localScale = newScale(1.0f);
		greenFace.transform.localScale = newScale(0.0f);
		orangeFace.transform.localScale = newScale(1.0f);
		redFace.transform.localScale = newScale(0.0f);
	}
	
	private Vector3 newScale(float offset) {
		return new Vector3(0.3f, 0.3f, 0) * lerp(offset);
	}
	
	private float lerp(float offset) {
		return (Mathf.PingPong(Time.time + offset, 1.0f)) * 0.7f + 0.5f;
	}
}
