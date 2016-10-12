using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
	float counter = 0;
	Vector3 startPos;
	Vector3 startEulerAngles;
	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		startEulerAngles = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		counter += 0.2f;
		transform.localPosition = startPos + 0.1f * new Vector3 (0, Mathf.Sin (counter), 0);
		transform.localEulerAngles = startEulerAngles + 2*new Vector3(0,0,Mathf.Cos (counter));
	}
}
