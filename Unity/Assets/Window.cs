using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {
	public GameObject player;
	public Vector2 maxCameraX = new Vector2(-4,0);
	public Vector2 maxCameraY = new Vector2(-18,0);
	public MainDay mainDay;
	float defaultStep = 25;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*if (mainDay.isPaused) {
			return;
		}*/
		followX (-player.transform.position,defaultStep);
		//followY (new Vector3 (-10, -6, 0) - player.transform.position,defaultStep);
		transform.position = new Vector3(Mathf.Clamp(transform.position.x,maxCameraX.x,maxCameraX.y),
		                                 Mathf.Clamp(transform.position.y,maxCameraY.x,maxCameraY.y),
		                                 transform.position.z);
	}
	void followX(Vector3 endPoint,float step) {
		float moveX = endPoint.x;//-transform.position.x;
		Vector3 moveVector = new Vector3(moveX,0,0)/step;
		transform.position += moveVector;
	}
	void followY(Vector3 endPoint, float step) {
		Vector3 moveVector = new Vector3(0,endPoint.y+2,0)/step;
		transform.position += moveVector;
	}
}
