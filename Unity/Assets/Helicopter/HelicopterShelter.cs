using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelicopterShelter : MonoBehaviour {
	Vector3 startPos;
	Vector3 targetPos;
	Vector3 dirVec;
	float counter;
	float throwCounter = -1;
	bool followHero;
	float gotoRotZ;
	bool pauseHelicopter;
	public float life;
	public float maxLife;

	bool isDying = false;
	int isDyingCounter = 0;
	GameObject hero;
	AudioSource myAudio;

	public AudioClip hurtSound;
	public AudioClip JanniOgLasseMusic;

	public SpriteRenderer helicopter;
	public SpriteRenderer helicopterTop;
	public SpriteRenderer helicopterTail;
	public SpriteRenderer helicopterWindshield;
	public SpriteRenderer helicopterBack;
	public SpriteRenderer helicopterLady;
	public GameObject item;
	public MyUILife UILife;
	Animator helicopterLadyAnim;
	public List<GameObject> explosionList;
	// Use this for initialization
	void Start () {
		maxLife = 600;
		life = maxLife;
		startPos = transform.localPosition;
		targetPos = new Vector3 (2, startPos.y, startPos.z);
		counter = 0;
		hero = GameObject.Find ("GameWindow/Hero");
		myAudio = GetComponent<AudioSource> ();
		helicopterLadyAnim = helicopterLady.GetComponent<Animator> ();
		myAudio.PlayOneShot (JanniOgLasseMusic,0.8f);
		gotoHero();
		GameObject.Find ("BreakingNews").GetComponent<BreakingNews> ().showBreaking ();
		UILife = GameObject.Find ("EnemyUI").GetComponent<MyUILife> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDying) {
			isDyingCounter ++;
			if (isDyingCounter<150) {
				int explosionI = Mathf.FloorToInt(isDyingCounter/20)%(explosionList.Count);
				explosionI = Mathf.Clamp(explosionI,0,explosionList.Count-1);
				explosionList[explosionI].SetActive(true);
				explosionList[explosionI].GetComponent<Animator>().enabled = true;
			}
			foreach (var explosion in explosionList) {
				if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BlankState")) {
					explosion.SetActive(false);
				}
			}
			if (isDyingCounter>200) {
				Destroy(gameObject);
				GameObject.Find ("Day"+ PlayerPrefs.GetInt ("currentLevel")+"Logic").GetComponent<DayEnemySpawner>().activeEnemies --;
				PlayerPrefs.SetInt("helicopter" + "Defeated",1);
			}
			pauseHelicopter = true;
		}
		if (pauseHelicopter) return;
		dirVec = 0.05f * ((targetPos - transform.localPosition));
		dirVec = new Vector3 (Mathf.Clamp(dirVec.x,-0.05f,0.05f),dirVec.y,dirVec.z);
		transform.localPosition += dirVec;
		transform.localEulerAngles = new Vector3 (0, 0, Mathf.LerpAngle (transform.localEulerAngles.z, gotoRotZ, 0.1f));
		/*transform.localEulerAngles = new Vector3 (0,0,15);
		transform.localPosition += new Vector3 (-0.04f,0,0);*/
		counter ++;
		if (counter > 200) {
			gotoHero();
			counter = 0;
		}
		if (followHero) {
			targetPos = new Vector3 (hero.transform.localPosition.x+0.5f, startPos.y, startPos.z);
			if (Mathf.Abs (transform.localPosition.x - hero.transform.localPosition.x) <= 1f) {
				helicopterLadyAnim.SetTrigger("throwThing");
				throwCounter = 0;
				followHero = false;
			}
			gotoRotZ = -20 * Mathf.Sign (dirVec.x);
		} else {
			gotoRotZ = 0;
		}
		if (throwCounter >= 0) {
			throwCounter++;
			if (throwCounter>10) {
				throwItem();
				throwCounter = -1;
			}
		}
	}
	void gotoHero() {
		followHero = true;
	}
	void throwItem() {
		GameObject tmpItem = (GameObject)Instantiate (item, transform.position + new Vector3 (-0.5f, 0f, -0.2f), Quaternion.identity);
		tmpItem.transform.parent = transform.parent;
	}
	void OnTriggerEnter(Collider other) {
		if (other.name == "Attack") {
			//gravity = -3;
			//isOnGround = false;
			//transform.position += new Vector3(0.02f*wallDir, 0, 0);
			//enemyState = "hurtInAir";
			if (other.transform.parent.name == "Item") {
				other.transform.parent.GetComponent<Item>().hit();
			}
			//startHurt(other.GetComponent<AttackHitbox>().damage);
			shineUp(0.1f);
			startHurt(other.GetComponent<AttackHitbox>().damage);
		}
	}
	public void startHurt(int damage) {
		if (isDying) return;
		//GameObject tmpAttackDamage = (GameObject)Instantiate(attackDamage,new Vector3 (transform.position.x,transform.position.y,transform.position.z-1), Quaternion.identity);
		int tmpDamage = damage;//1+Random.Range(0,2);
		life -= tmpDamage;
		//tmpAttackDamage.GetComponent<AttackDamage>().damage = tmpDamage;
		//tmpAttackDamage.transform.parent = transform.parent;
		UILife.makeVisible(this);
		if (life <= 0) {
			GameObject.Find ("MainDay").GetComponent<MainDay> ().defeatedEnemies ++;
			hero.GetComponent<Hero>().xp += 5;
			isDying = true;
		}
	}
	void shineUp(float time) {
		helicopter.material.SetColor("_Brightness",new Color(1,1,1,0));
		helicopterTop.material.SetColor("_Brightness",new Color(1,1,1,0));
		helicopterTail.material.SetColor("_Brightness",new Color(1,1,1,0));
		helicopterWindshield.material.SetColor("_Brightness",new Color(1,1,1,0));
		helicopterBack.material.SetColor("_Brightness",new Color(1,1,1,0));
		helicopterLady.material.SetColor("_Brightness",new Color(1,1,1,0));
		pauseHelicopter = true;
		myAudio.PlayOneShot(hurtSound,1);
		Invoke ("stopShining",time);
	}
	void stopShining() {
		pauseHelicopter = false;
		helicopter.material.SetColor("_Brightness",new Color(0,0,0,0));
		helicopterTop.material.SetColor("_Brightness",new Color(0,0,0,0));
		helicopterTail.material.SetColor("_Brightness",new Color(0,0,0,0));
		helicopterWindshield.material.SetColor("_Brightness",new Color(0,0,0,0));
		helicopterBack.material.SetColor("_Brightness",new Color(0,0,0,0));
		helicopterLady.material.SetColor("_Brightness",new Color(0,0,0,0));
	}
}
