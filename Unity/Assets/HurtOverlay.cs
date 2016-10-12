using UnityEngine;
using System.Collections;

public class HurtOverlay : MonoBehaviour {
	Vector3 newPosition;
	int counter = -1;
	SpriteRenderer sprRend;
	// Use this for initialization
	void Start () {
		newPosition = new Vector3 (0.09f,-0.54f,transform.position.z);
		sprRend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter >= 0) {
			counter ++;
			if (counter<8) {
				sprRend.color = new Color(1,1,1,counter/20f);
			}
			if (counter>12) {
				sprRend.color = new Color(1,1,1,(18-counter)/20f);
			}
			if (counter>18) {
				transform.position = new Vector3(1000,0,0);
				counter = -1;
			}
		}
	}
	public void showHurtOverlay() {
		if (counter<0) {
			transform.position = newPosition;
			counter = 0;
		}
	}
}
