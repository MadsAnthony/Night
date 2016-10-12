using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public GameObject actualCoin;
	public GameObject refl;
	public float xSpeed;
	public AudioClip pickupSound;
	public AudioClip coinDrop;
	bool firstTimeHitFloor = false;
	MainDay mainDay;
	float gravity = 0;
	float reflPosY;
	bool isTaken = false;
	int counter = 0;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		gravity = Random.Range(-120,-100)*0.001f;//-0.1f;
		mainDay = GameObject.Find ("MainDay").GetComponent<MainDay> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTaken) {
			gravity += 0.008f;
			gravity = Mathf.Clamp (gravity, -0.2f, 0.2f);
			transform.position += new Vector3 (xSpeed, -gravity, 0);
			if (transform.position.y < -1.85f && gravity > 0) {
					gravity *= -0.8f;
					xSpeed *= 0.9f;
					transform.position = new Vector3 (transform.position.x, -1.85f, transform.position.z);
					if (!firstTimeHitFloor && Mathf.Abs (gravity) > 0.05f) {
							audio.PlayOneShot (coinDrop, 8);
							firstTimeHitFloor = true;
					}
			}
			reflPosY = -actualCoin.transform.position.y - 1.85f - 1.85f - 0.2f;
			reflPosY = Mathf.Clamp (reflPosY, -10f, -1.85f);
			refl.transform.position = new Vector3 (refl.transform.position.x,
                               reflPosY,
                               refl.transform.position.z);
		}
		if (isTaken) {
			counter ++;
			if (counter>120) {
				Destroy (gameObject);
			}
		}
	}
	void OnTriggerStay(Collider other) {
		if (other.name == "Hero" && !isTaken) {
			PlayerPrefs.SetInt ("numberOfCoins",PlayerPrefs.GetInt ("numberOfCoins")+1);
			mainDay.mainAudio.PlayOneShot(pickupSound,0.5f);
			actualCoin.GetComponent<Animator>().SetInteger("state",1);
			refl.GetComponent<Animator>().SetInteger("state",1);
			isTaken = true;
		}
	}
}
