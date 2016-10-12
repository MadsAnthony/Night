using UnityEngine;
using System.Collections;

public class ClickHitbox : MonoBehaviour {
	public string type;
	public bool isClicked = false;
	public bool isClickedUp = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//isClicked = false;
	}
	void OnMouseDown()
	{
		isClicked = true;
		/*if (type == "button") {
			Application.LoadLevel ("Scene01");
		}*/
	}
	void OnMouseUp()
	{
		isClickedUp = true;
		/*if (type == "button") {
			Application.LoadLevel ("Scene01");
		}*/
	}
}
