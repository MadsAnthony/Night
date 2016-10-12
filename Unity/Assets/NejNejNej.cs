using UnityEngine;
using System.Collections;

public class NejNejNej : MonoBehaviour {
	float counter = -1;
	AudioSource myAudio;
	Vector3 newPosition;
	GameObject mainCamera;
	Vector3 mainCameraPos;

	public AudioClip sound1;
	public GameObject nej1;
	public GameObject nej2;
	public GameObject nej3;
	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource> ();
		newPosition = new Vector3 (0, -0.75f, -1.5f);
		mainCamera = GameObject.Find ("Main Camera");
		mainCameraPos = mainCamera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == 0) {
			myAudio.PlayOneShot (sound1, 1.5f);
			transform.position = newPosition;
		}
		if (counter >= 0) {
			counter += TimeScript.TimeConstant*Time.deltaTime;
		}
		if (counter> 0 && counter < 30) {
			mainCamera.transform.position += new Vector3(0.1f*Mathf.Cos(counter),0.1f*Mathf.Sin(counter),0)*TimeScript.TimeConstant*Time.deltaTime;
		}
		if (counter > 30) {
			mainCamera.transform.position = mainCameraPos;
			nej1.transform.localPosition += new Vector3 (0, (-nej1.transform.localPosition.y - 1.77f) * 0.1f, 0);
		}
		if (counter > 60) {
			nej2.transform.localPosition += new Vector3 (0, (-nej2.transform.localPosition.y - 2.27f) * 0.1f, 0);
		}
		if (counter > 90) {
			nej3.transform.localPosition += new Vector3 (0, (-nej3.transform.localPosition.y - 1.97f) * 0.1f, 0);
		}
		if (counter > 200) {
			counter = -1;
			nej1.transform.localPosition += new Vector3 (0, -7, 0);
			nej2.transform.localPosition += new Vector3 (0, -7, 0);
			nej3.transform.localPosition += new Vector3 (0, -7, 0);
			transform.position = new Vector3(1000,0,0);
		}
		/*nej1.transform.localPosition += new Vector3 (0,0.1f,0);
		nej1.transform.localPosition = new Vector3 (nej1.transform.localPosition.x,
		                                            Mathf.Clamp(nej1.transform.localPosition.y,-10,-1.77f),
		                                            nej1.transform.localPosition.z);*/
	}
	public void playNejNejNej() {
		if (counter < 0) {
			counter = 0;
		}
	}
}
