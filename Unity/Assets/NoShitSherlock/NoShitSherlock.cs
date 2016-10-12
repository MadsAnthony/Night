using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoShitSherlock : MonoBehaviour {
	int counter = -1;
	AudioSource myAudio;
	Vector3 newPosition;

	List<AudioClip> soundList;
	public AudioClip noShitSherlock1;
	public AudioClip noShitSherlock2;
	public AudioClip noShitSherlock3;
	public AudioClip noShitSherlock4;
	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource> ();
		newPosition = transform.position;
		transform.position = new Vector3(1000,0,0);
		soundList = new List<AudioClip> ();
		soundList.Add (noShitSherlock1);
		soundList.Add (noShitSherlock2);
		soundList.Add (noShitSherlock3);
		soundList.Add (noShitSherlock4);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == 0) {
			myAudio.PlayOneShot(soundList[Random.Range(0,4)],4);
			transform.position = newPosition;
		}
		if (counter >= 0) {
			counter ++;
		}
		if (counter>150) {
			counter = -1;
			transform.position = new Vector3(1000,0,0);
		}
	}
	public void playNoShitSherlock() {
		if (counter < 0) {
			counter = 0;
		}
	}
}
