using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	float gravity;
	bool isOnGround = false;
	AudioSource myAudio;
	public bool hasSpeed = true;
	public GameObject attackHitbox;
	public GameObject pickupHitbox;
	public int dir;
	public string itemName = "Cup";

	public AudioClip impactGround01;
	public AudioClip impactGround02;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Items/"+itemName);
		myAudio = GetComponent<AudioSource>();
		if (hasSpeed) {
			gravity = -0.05f;
		}
		attackHitbox.GetComponent<AttackHitbox> ().damage = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasSpeed) {
			transform.eulerAngles += new Vector3 (0, 0, 10 * -dir)*TimeScript.TimeConstant*Time.deltaTime;
			transform.position += new Vector3 (0.1f * dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
		}
		if (!isOnGround) {
			gravity += 0.003f;
			attackHitbox.SetActive(true);
			pickupHitbox.SetActive(false);
		} else {
			attackHitbox.SetActive(false);
			pickupHitbox.SetActive(true);
		}
		if (!hasSpeed) {
			attackHitbox.SetActive(false);
			pickupHitbox.SetActive(true);
		}
		//gravity = Mathf.Clamp (gravity, -0.5f,0.02f);
		transform.position += new Vector3 (0,-gravity,0)*TimeScript.TimeConstant*Time.deltaTime;
		if (transform.position.y<-1.35f-0.45f && gravity>0) {
			if (itemName == "Cup") {
				myAudio.PlayOneShot(impactGround02,0.4f);
			} else {
				myAudio.PlayOneShot(impactGround01,1);
			}
			isOnGround = true;
			hasSpeed = false;
			transform.position = new Vector3 (transform.position.x, -1.35f-0.45f, transform.position.z);
			gravity = 0;
		}
		
		if (transform.localPosition.x<-3.8 || transform.localPosition.x>14.5f) {
			GameObject.Find("MainDay").GetComponent<MainDay>().nejNejNej.playNejNejNej();
			Destroy (gameObject);
		}
	}
	public void hit() {
		//GameObject.Find("MainDay").GetComponent<MainDay>().noShitSherlock.playNoShitSherlock();
		Destroy (gameObject);
	}
}
