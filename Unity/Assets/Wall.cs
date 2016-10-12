using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {

	public static List<GameObject> wallList;
	// Use this for initialization
	void Start () {
		if (wallList == null) {
			wallList = new List<GameObject>();
		}
		wallList.Add (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
