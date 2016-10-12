using UnityEngine;
using System.Collections;

public class PickUpSignifier : MonoBehaviour {

	public Hero hero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hero.isNearPickup && !hero.hasThrowableItem && hero.canPickupItem) {
			transform.position = hero.transform.position+new Vector3(0.4f*Mathf.Sign(hero.transform.localScale.x),0.6f,0);
		} else {
			transform.position = new Vector3(1000,0,0);
		}
	}
}
