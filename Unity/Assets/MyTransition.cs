using UnityEngine;
using System.Collections;

public class MyTransition : MonoBehaviour {
	int counter = 0;
	Vector3 newPos;
	// Use this for initialization
	void Start () {
		newPos = new Vector3 (1, -0.95f, -2);
		transform.position = newPos;
		/*transform.localScale = new Vector3 (transform.localScale.x*-1,
		                                    transform.localScale.y,
		                                    transform.localScale.z);*/
	}
	
	// Update is called once per frame
	void Update () {
		counter ++;
		if (counter > 65) {
			Destroy(gameObject);
		}
	}
}
