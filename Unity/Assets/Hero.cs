using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	bool leftArrowPressed;
	bool rightArrowPressed;
	bool upArrowPressed;
	bool downArrowPressed;
	bool downArrowPressedOld;
	bool spacePressed;
	bool rButtonPressed;
	int dir;
	Vector3 startScale;
	public SpriteRenderer mySprite;
	public Animator anim;
	AudioSource myAudio;
	float gravity = 0;
	bool isOnGround = true;
	int hitState = 0;

	public HurtOverlay hurtOverlay;
	float hurtCounter = -1;
	int hurtState = 0;

	float maxLife = 20;
	public float life;
	public int level = 1;
	public int hitPower = 1;
	Vector3 lifeStartScale;

	public float maxXp = 4;
	public float xp;
	Vector3 xpStartScale;

	public bool hasThrowableItem = false;
	public GameObject item;
	string itemName;

	public float isHittingCounter = -1;

	public bool leftJoystick = false;
	public bool rightJoystick = false;
	public bool downJoystick = false;
	public bool downJoystickOld = false;
	public bool rJoystick = false;
	public GameObject attackHitbox;
	public GameObject attackDamage;

	public GameObject lifeBar;

	public AudioClip whipSound01;
	public AudioClip whipSound02;
	public AudioClip whipSound03;
	public AudioClip whipSound04;

	public AudioClip impactSound01;
	public AudioClip impactSound02;
	public AudioClip impactSound03;
	public AudioClip impactSound04;

	public AudioClip jumpSound;

	public Sprite newHead;
	public GameObject headObject;
	public GameObject itemHeld;
	SpriteRenderer itemHeldSpr;
	bool canReleaseItem;
	public bool canPickupItem = true;
	public GameObject overlay;

	public MainDay mainDay;
	public bool isNearPickup;
	public TypogenicText uiLevel;
	public Animator otherAnimator;
	Vector3 attackStartSize;
	Vector3 weaponBoxSize;
	
	// Use this for initialization
	void Start () {
		EnemySpawner.EnemyType hostName = EnemySpawner.EnemyType.AnderBreinholt;
		if (PlayerPrefs.HasKey ("hostName")) {
			hostName = EnemySpawner.getType (PlayerPrefs.GetString ("hostName"));
		}
		if (PlayerPrefs.HasKey (EnemySpawner.getString (hostName) + "Xp")) {
			xp = PlayerPrefs.GetFloat (EnemySpawner.getString (hostName) + "Xp");
		} else {
			PlayerPrefs.SetInt(EnemySpawner.getString (hostName) + "Xp",1);
			xp = PlayerPrefs.GetFloat (EnemySpawner.getString (hostName) + "Xp");
		}
		if (PlayerPrefs.HasKey (EnemySpawner.getString (hostName) + "Lvl")) {
			level = PlayerPrefs.GetInt (EnemySpawner.getString (hostName) + "Lvl");
		} else {
			PlayerPrefs.SetInt(EnemySpawner.getString (hostName) + "Lvl",1);
			level = PlayerPrefs.GetInt (EnemySpawner.getString (hostName) + "Lvl");
		}
		setNewStatsByLevel ();
		dir = 1;
		startScale = transform.localScale;
		mySprite.enabled = true;
		//anim = GetComponent<Animator> ();
		otherAnimator.gameObject.SetActive(false);
		if (PlayerPrefs.HasKey ("hostName")) {
			if (EnemySpawner.getType(PlayerPrefs.GetString("hostName")) != EnemySpawner.EnemyType.AnderBreinholt) {
				mySprite.enabled = false;
				anim = otherAnimator;
				otherAnimator.gameObject.SetActive(true);
			}
		}
		myAudio = GetComponent<AudioSource> ();
		life = maxLife;
		lifeStartScale = lifeBar.transform.localScale;
		headObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Heads/LiamHead");
		headObject.GetComponent<SpriteRenderer>().enabled =  false;
		itemHeldSpr = itemHeld.GetComponent<SpriteRenderer> ();
		attackStartSize = attackHitbox.GetComponent<BoxCollider> ().size;
		weaponBoxSize = new Vector3 (2, attackStartSize.y, attackStartSize.z);
	}
	public void hideItem() {
		itemHeldSpr.enabled = false;
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rightArrowPressed = true;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			rightArrowPressed = false;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			leftArrowPressed = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			leftArrowPressed = false;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			upArrowPressed = true;
			jump ();
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			upArrowPressed = false;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			downArrowPressed = true;
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			downArrowPressed = false;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			spacePressed = true;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			spacePressed = false;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PlayerPrefs.SetString("MenuGotoScreen","splashScreen");
			Application.LoadLevel("Menu");
		}
		/*if (Input.GetKeyDown (KeyCode.A)) {
			mainDay.activateWinScreen();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			mainDay.levelUpScreen.activateScreen();
		}*/
		if (Input.GetKeyDown (KeyCode.R)) {
			rButtonPressed = true;
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			rButtonPressed = false;
		}
		if (rButtonPressed || rJoystick) {
			Application.LoadLevel ("Scene01");
		}
		if (mainDay.isPaused) {
			return;
		}
		if (isHittingCounter>=0) {
			if (!hasThrowableItem || itemName != "Pencil") {
				anim.SetInteger("state",2+hitState);
			} else {
				anim.SetInteger("state",22+hitState);
			}
			if (isHittingCounter<10) {
				transform.position += new Vector3 (0.02f * dir, 0, 0);
			}
			isHittingCounter += TimeScript.TimeConstant*Time.deltaTime;
		}
		if (!hasThrowableItem || itemName != "Pencil") {
			if (attackHitbox.GetComponent<BoxCollider> ().size != attackStartSize) {
				attackHitbox.GetComponent<BoxCollider> ().size = attackStartSize;
			}
		} else {
			if (attackHitbox.GetComponent<BoxCollider> ().size != weaponBoxSize) {
				attackHitbox.GetComponent<BoxCollider> ().size = weaponBoxSize;
			}
		}
		if (isHittingCounter>2) {
			attackHitbox.transform.position = new Vector3 (transform.position.x + 0.5f * dir, 5, transform.position.z);
		}
		if (isHittingCounter>23) {
			isHittingCounter = -1;
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (mainDay.isPaused) {
			return;
		}
		isNearPickup = false;
		//xp += 0.01f;
		lifeBar.transform.localScale = new Vector3 (lifeStartScale.x*life/maxLife,lifeStartScale.y,lifeStartScale.z);
		if (lifeBar.transform.localScale.x<0) {
			lifeBar.transform.localScale = new Vector3 (0,
			                                            lifeBar.transform.localScale.y,
			                                            lifeBar.transform.localScale.z);
		}
		anim.SetInteger("state",0);
		if (life <= 0) {
			return;
		}
		if (spacePressed) {
			startHit();
		}
		if (isHittingCounter < 0 && hurtCounter < 0) {
			if (rightArrowPressed || rightJoystick) {
				dir = 1;
				anim.SetInteger("state",1);
			}
			if (leftArrowPressed || leftJoystick) {
				dir = -1;
				anim.SetInteger("state",1);
			}
			if (!isOnGround) {
				gravity += 0.2f;
			}
			if (name == "Hero") {
				transform.position += new Vector3 (0, -gravity * 0.05f, 0)*TimeScript.TimeConstant*Time.deltaTime;
				if (transform.position.y < -1.35f) {
					isOnGround = true;
					transform.position = new Vector3 (transform.position.x, -1.35f, transform.position.z);
				}
			}
			transform.localScale = new Vector3 (startScale.x * dir, startScale.y, startScale.z);
			if (rightArrowPressed || leftArrowPressed || rightJoystick || leftJoystick) {
				transform.position += new Vector3 (0.05f * dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
			}
		}
		if (hurtCounter>=0) {
			anim.SetInteger("state",4+hurtState%2);
			if (hurtCounter<10) {
				transform.position += new Vector3 (-0.01f * dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
			}
			hurtCounter += TimeScript.TimeConstant*Time.deltaTime;
		}
		if (hurtCounter>15) {
			//enemyState = "wait";
			hurtCounter = -1;
		}
		if ((!downArrowPressed && downArrowPressedOld) ||
		    (!downJoystick && downJoystickOld)) {
			if (hasThrowableItem) {
				canReleaseItem = true;
			}
			if (!hasThrowableItem) {
				canPickupItem = true;
			}
		}
		if (hasThrowableItem) {
			if (isHittingCounter>=0) {
				itemHeldSpr.enabled = false;
			} else {
				if (!itemHeldSpr.enabled) {
					itemHeldSpr.sprite = Resources.Load<Sprite>("Items/"+itemName);
					itemHeldSpr.enabled = true;
					setItemRotationAndPos();
				}
			}
			if (canReleaseItem) {
				if (downArrowPressed || downJoystick) {
					putItemDown();
				}
			}
		} else {
			itemHeldSpr.enabled = false;
		}
		downArrowPressedOld = downArrowPressed;
		downJoystickOld = downJoystick;
	}
	void setItemRotationAndPos() {
		if (itemName == "Cup") {
			itemHeldSpr.transform.localPosition = new Vector3 (0, 0, 0);
			itemHeldSpr.transform.localEulerAngles = new Vector3 (0, 0, 0);
		}
		if (itemName == "TableFrame") {
			itemHeldSpr.transform.localPosition = new Vector3 (0, 0, 0);
			itemHeldSpr.transform.localEulerAngles = new Vector3 (0, 0, 0);
		}
		if (itemName == "Pencil") {
			itemHeldSpr.transform.localPosition = new Vector3 (0, 0.3f, 0);
			itemHeldSpr.transform.localEulerAngles = new Vector3 (0, 0, 85);
		}
	}
	public void jump() {
		if (isOnGround) {
			myAudio.PlayOneShot(jumpSound,4);
			gravity = -3;
			isOnGround = false;
		}
	}
	public void startHit() {
		if (life <= 0) {
			return;
		}
		if (hasThrowableItem) {
			if (itemName != "Pencil") {
				startThrow();
				return;
			}
		}
		if (isHittingCounter < 0 && hurtCounter < 0) {
			attackHitbox.transform.position = new Vector3 (transform.position.x+0.3f*dir,transform.position.y,transform.position.z);
			attackHitbox.GetComponent<AttackHitbox>().damage = hitPower + Random.Range (0, 2);
			hitState ++;
			hitState = hitState % 2;
			int randN = Random.Range(0,2);
			if (hitState == 0) {
				if (randN == 0) {
					myAudio.PlayOneShot(whipSound01,1);
				} else {
					myAudio.PlayOneShot(whipSound02,2);
				}
			}
			if (hitState == 1) {
				if (randN == 0) {
					myAudio.PlayOneShot(whipSound03,1);
				} else {
					myAudio.PlayOneShot(whipSound04,2);
				}
			}
			isHittingCounter = 0;
		}
	}
	public void startThrow() {
		GameObject tmpItem = (GameObject)Instantiate (item, transform.position + new Vector3 (0.4f*dir, 0.1f, 0.1f), Quaternion.identity);
		tmpItem.transform.localScale = new Vector3 (dir*tmpItem.transform.localScale.x,
		                                            tmpItem.transform.localScale.y,
		                                            tmpItem.transform.localScale.z);
		tmpItem.transform.parent = transform.parent;
		tmpItem.GetComponent<Item> ().dir = dir;
		tmpItem.GetComponent<Item> ().itemName = itemName;
		tmpItem.name = "Item";
		hasThrowableItem = false;
		canPickupItem = true;
		canReleaseItem = false;
	}
	public void putItemDown() {
		GameObject tmpItem = (GameObject)Instantiate (item, transform.position + new Vector3 (0, -0.25f, 0.1f), Quaternion.identity);
		tmpItem.transform.localScale = new Vector3 (dir*tmpItem.transform.localScale.x,
		                                            tmpItem.transform.localScale.y,
		                                            tmpItem.transform.localScale.z);
		tmpItem.transform.parent = transform.parent;
		tmpItem.GetComponent<Item> ().dir = dir;
		tmpItem.GetComponent<Item> ().itemName = itemName;
		tmpItem.name = "Item";
		tmpItem.GetComponent<Item> ().hasSpeed = false;
		hasThrowableItem = false;
		canPickupItem = false;
		canReleaseItem = false;
	}
	public void startHurt(int damage) {
			/*if (enemyState == "hurtInAir" || enemyState == "hurtOnGround")
			return;*/
			if (hurtCounter < 0) {
				if (life <= 0) {
					//GameObject.Find ("GameWindow/Hero").GetComponent<Hero> ().xp += 2;
				}
				/*if (hurtState > 4 || life <= 0) {
					gravity = -3;
					isOnGround = false;
					transform.position += new Vector3 (0.02f * dir, 0, 0);
					hurtXSpeed = 0.06f;
					enemyState = "hurtInAir";
					hurtState = 0;
				} else {*/
					//enemyState = "hurt";
					hurtState ++;
					hurtCounter = 0;
				//}
				int randN = Random.Range (0, 2);
				if (hurtState % 2 == 0) {
						if (randN == 0) {
								myAudio.PlayOneShot (impactSound01, 1);
						} else {
								myAudio.PlayOneShot (impactSound02, 2);
						}
				}
				if (hurtState % 2 == 1) {
						if (randN == 0) {
								myAudio.PlayOneShot (impactSound03, 1);
						} else {
								myAudio.PlayOneShot (impactSound04, 2);
						}
				}
				int tmpDamage = damage;//1 + Random.Range (0, 2);
				life -= tmpDamage;
			hurtOverlay.showHurtOverlay();
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.name == "Attack" && other.gameObject != attackHitbox) {
			int wallDir = -1;
			if (other.transform.position.x<transform.position.x) {
				wallDir = 1;
			}
			dir = -wallDir;
			//gravity = -3;
			//isOnGround = false;
			//transform.position += new Vector3(0.02f*wallDir, 0, 0);
			//enemyState = "hurtInAir";
			startHurt(other.GetComponent<AttackHitbox>().damage);
		}
	}
	void OnTriggerStay(Collider other) {
		if (other.name == "Pickup") {
			if (!hasThrowableItem) {
				if (canPickupItem) {
					isNearPickup = true;
					if (downArrowPressed || downJoystick) {
						if (other.transform.parent.name == "Item") {
							itemName = other.transform.parent.GetComponent<Item>().itemName;
						} else {
							itemName = other.transform.parent.name;
						}
						hasThrowableItem = true;
						Destroy(other.transform.parent.gameObject);
					}
				}
			}
		}
		if (other.name == "Wall") {
			int wallDir = -1;
			if (other.transform.position.x<transform.position.x) {
				wallDir = 1;
			}
			transform.position = new Vector3(other.transform.position.x+(other.bounds.size.x/2f+transform.GetComponent<Collider>().bounds.size.x/2f)*wallDir,
			                                 transform.position.y,
			                                 transform.position.z);
		}
		if (other.name == "Ground" && gravity>0) {
			if (other.transform.parent.name == "Enemy") {
				if (downArrowPressed || downJoystick) {
					mainDay.noShitSherlock.playNoShitSherlock();
				}
			}
			transform.position = new Vector3(transform.position.x,
			                                 other.transform.position.y+other.bounds.size.y/2f+(GetComponent<Collider>().bounds.size.y/2f-(transform.GetComponent<Collider>().bounds.center.y-transform.position.y)),
			                                 transform.position.z);
			isOnGround = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.name == "Ground") {
			isOnGround = false;
		}
	}
	public void setNewStatsByLevel() {
		if (level == 1) {
			maxLife = 30;
			hitPower = 1;
			maxXp = 2*level;
		}
		if (level == 2) {
			maxLife = 60;
			hitPower = 4;
			maxXp = 4*level;
		}
		if (level == 3) {
			maxLife = 80;
			hitPower = 6;
			maxXp = 4*level;
		}
		if (level>3) {
			maxLife = 40+20*level;
			hitPower = 1+2*level;
			maxXp = 5*level;
		}
	}
}
