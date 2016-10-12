using UnityEngine;
using System.Collections;

public class Reflection : MonoBehaviour {

	Animator anim;
	public GameObject myObject;
	public Animator myObjectAnim;
	public Animator myObjectAnim2;
	public SpriteRenderer head;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (name == "HeroRefl") {
			GetComponent<SpriteRenderer> ().GetComponent<Renderer>().enabled = true;
			transform.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().enabled = false;
			if (PlayerPrefs.HasKey ("hostName")) {
				if (EnemySpawner.getType (PlayerPrefs.GetString ("hostName")) != EnemySpawner.EnemyType.AnderBreinholt) {
					transform.GetComponent<Renderer>().enabled = false;
					transform.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().enabled = true;
					anim = transform.GetChild (0).transform.GetChild (0).GetComponent<Animator> ();
					myObjectAnim = myObjectAnim2;
				}
			}
		}
		if (name == "EnemyRefl") {
			if (head != null) {
				string tmpStringName = myObject.transform.parent.GetComponent<Enemy>().coHostName;
				head.sprite = Resources.Load<Sprite> ("Heads/" + tmpStringName + "Head");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (name == "EnemyRefl") {
			anim.SetInteger ("state", myObjectAnim.GetInteger ("state"));
		}
	}
	void FixedUpdate () {
		if (name != "EnemyRefl") {
			transform.localScale = new Vector3 (-myObject.transform.localScale.x, transform.localScale.y, transform.localScale.z);
			anim.SetInteger ("state", myObjectAnim.GetInteger ("state"));
		}
		//transform.position = new Vector3 (myObject.transform.position.x,(-1.35f-3.65f+-1.35f-myObject.transform.position.y)/2f,transform.position.z);
		if (transform.parent.name == "EnemyReflShelter") {
			//transform.parent.localScale = new Vector3 (myObject.transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
			transform.parent.position = new Vector3 (myObject.transform.parent.position.x,(-1.35f-3.65f+-0.68f-myObject.transform.parent.position.y)/2f,transform.parent.position.z);
			//transform.parent.eulerAngles = new Vector3 (myObject.transform.parent.eulerAngles.x, myObject.transform.parent.eulerAngles.y, 180-myObject.transform.parent.eulerAngles.z);
			transform.eulerAngles = new Vector3 (myObject.transform.eulerAngles.x, myObject.transform.eulerAngles.y, 180 - myObject.transform.eulerAngles.z);
		} else {
			transform.position = new Vector3 (myObject.transform.position.x,(-1.35f-3.65f+-1.35f-myObject.transform.position.y)/2f,transform.position.z);
			transform.eulerAngles = new Vector3 (myObject.transform.eulerAngles.x, myObject.transform.eulerAngles.y, 180 - myObject.transform.eulerAngles.z);
		}
	}
}
