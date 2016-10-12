using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour {
	Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void showFlash(float time) {
		if (transform.position == startPos) {
			transform.position = new Vector3 (0, 0, startPos.z);
			Invoke("hideFlash",time);
		}
	}
	void hideFlash() {
		transform.position = startPos;
	}
}
