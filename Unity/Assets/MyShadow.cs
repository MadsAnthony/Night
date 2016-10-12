using UnityEngine;
using System.Collections;

public class MyShadow : MonoBehaviour {

	public Animator anim;
	Vector3 startScale;
	public SpriteRenderer sprRend;
	float alpha;
	float startAlpha;
	public GameObject myObject;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();
		startScale = transform.localScale;
		//sprRend = GetComponent<SpriteRenderer> ();
		startAlpha = sprRend.color.a;
		alpha = startAlpha;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger("state",myObject.GetComponent<Hero> ().anim.GetInteger ("state"));
		if (name != "EnemyRefl") {
			transform.localScale = new Vector3 (startScale.x*Mathf.Sign(myObject.transform.localScale.x),
			                                    startScale.y,
			                                    startScale.z);
		}
		transform.position = new Vector3 (myObject.transform.position.x-0.1f,Mathf.Clamp(myObject.transform.position.y+0.2f,-0.7f,2),transform.position.z);
		transform.eulerAngles = new Vector3(myObject.transform.eulerAngles.x,myObject.transform.eulerAngles.y,myObject.transform.eulerAngles.z);
		if (myObject.transform.localPosition.x < 0f) {
			alpha = (0.5f + myObject.transform.localPosition.x) / 10f;
		}
		if (myObject.transform.localPosition.x > 3) {
			alpha = (3.8f - myObject.transform.localPosition.x)/20f;
		}
		alpha = Mathf.Clamp (alpha, 0, startAlpha);
		sprRend.color = new Color (sprRend.color.r, sprRend.color.g, sprRend.color.b, alpha);
	}
}
