using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {
	Vector3 startRotation;
	float counter = 0;
	// Use this for initialization
	void Start () {
		startRotation = transform.eulerAngles;
		counter = Random.Range (0, 10);
	}
	
	// Update is called once per frame
	void Update () {
		counter += 0.05f;
		transform.eulerAngles = new Vector3 (startRotation.x,
		                                    startRotation.y,
		                                    startRotation.z + 5*Mathf.Cos (counter));
	}
}
