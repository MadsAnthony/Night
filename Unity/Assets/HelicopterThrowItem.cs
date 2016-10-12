using UnityEngine;
using System.Collections;

public class HelicopterThrowItem : MonoBehaviour {
	public GameObject Bomb;
	public GameObject Explosion;
	public Animator ExplosionAnim;
	public GameObject Attack;
	bool keepFalling;
	// Use this for initialization
	void Start () {
		Explosion.SetActive(false);
		keepFalling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (ExplosionAnim.GetCurrentAnimatorStateInfo(0).IsName("BlankState")) {
			Destroy(gameObject);
		}
		if (keepFalling) {
			transform.localPosition += new Vector3 (0, -0.1f, 0);
		}
		if (keepFalling && transform.localPosition.y < 0.3f) {
			keepFalling = false;
			Bomb.SetActive(false);
			Explosion.SetActive(true);
			Attack.SetActive (true);
			ExplosionAnim.enabled = true;
		}
	}
}
