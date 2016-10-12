using UnityEngine;
using System.Collections;

public class MyTrigger : MonoBehaviour {
	
	public Collider collider;
	public string triggerState;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		triggerState = "enter";
		collider = other;
	}
	void OnTriggerStay(Collider other) {
		triggerState = "stay";
		collider = other;
	}
	void OnTriggerExit(Collider other) {
		triggerState = "exit";
		collider = other;
	}
}
