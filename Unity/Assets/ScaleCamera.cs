using UnityEngine;
using System.Collections;

public class ScaleCamera : MonoBehaviour {

	Camera camera;
	Transform target;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		target = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = camera.WorldToScreenPoint(target.position);
		if (Mathf.Round(screenPos.x*1000)*0.001f != 0) {
			camera.orthographicSize = Mathf.Abs (target.position.x) / (camera.pixelWidth / (float)camera.pixelHeight);
		}
	}
}
