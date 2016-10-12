using UnityEngine;
using System.Collections;

public class CoinGraphicFly : MonoBehaviour {
	Vector3 startPos;
	Vector3 destinationPos;
	int counter = -1;
	MainDay mainDay;
	public AudioClip pickupSound;
	// Use this for initialization
	void Start () {
		transform.position += new Vector3 (0,0,-0.1f);
		startPos = transform.position;
		destinationPos = new Vector3 (-2.25f,1.35f,transform.position.z);
		mainDay = GameObject.Find ("MainDay").GetComponent<MainDay>();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < 0) {
			transform.position += 0.03f * (destinationPos - startPos);
		}
		if (destinationPos.x-transform.position.x>=0 && counter<0) {
			GetComponent<Animator>().SetInteger("state",1);
			counter = 0;
		}
		if (counter >= 0) {
			counter++;
			if (counter==20) {
				PlayerPrefs.SetInt ("numberOfCoins",PlayerPrefs.GetInt ("numberOfCoins")+1);
				mainDay.mainAudio.PlayOneShot(pickupSound,0.5f);
				Destroy(gameObject);
			}
		}
	}
}
