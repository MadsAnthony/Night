using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	string enemyState = "goLeft";
	bool isOnGround = false;
	int dir;
	Vector3 startScale;
	public Animator anim;
	AudioSource myAudio;
	float gravity = 0;
	float counter = 0;
	float hurtCounter = -1;
	int hurtState = 0;
	Vector3 lifeStartScale;
	float hurtXSpeed;
	int hurtOnGroundcounter = 0;
	int hitState = 0;
	float hitDelayCount = 0;
	GameObject hero;
	public MainDay mainDay;
	public float isHittingCounter = -1;

	public float maxLife;
	public float life;
	public int level = 1;
	public int hitPower;

	public GameObject wallHitbox;
	public GameObject groundHitbox;
	public GameObject attackHitbox;
	public GameObject attackDamage;

	public MyUILife UILife;
	public GameObject lifeBar;

	public AudioClip whipSound01;
	public AudioClip whipSound02;
	public AudioClip whipSound03;
	public AudioClip whipSound04;

	public AudioClip impactSound01;
	public AudioClip impactSound02;
	public AudioClip impactSound03;
	public AudioClip impactSound04;

	public AudioClip impactGround01;

	public GameObject spawnEnemy;

	public string coHostName;
	public GameObject headObject;

	int soundCounter = 0;
	int soundLength = 1;
	public AudioClip specialSound01;
	public AudioClip specialSound02;
	public SpriteRenderer headIdle;
	public SpriteRenderer headSpeak;
	public SpriteRenderer headAttack;
	public SpriteRenderer headHurt;
	public SpriteRenderer breast;
	public GameObject coin;
	int numberOfCoinsSpawn;
	int actualCoinSpawn = 0;

	// Use this for initialization
	void Start () {
		breast.GetComponent<Renderer>().enabled = false;
		if (level == 1) {
			maxLife = 30;
			numberOfCoinsSpawn = 2;
			hitPower = 1;
		}
		if (level == 2) {
			maxLife = 60;
			numberOfCoinsSpawn = 3;
			hitPower = 4;
		}
		if (level == 3) {
			maxLife = 80;
			numberOfCoinsSpawn = 5;
			hitPower = 7;
		}
		if (level == 4) {
			maxLife = 100;
			numberOfCoinsSpawn = 6;
			hitPower = 9;
		}
		if (level == 5) {
			maxLife = 120;
			numberOfCoinsSpawn = 7;
			hitPower = 12;
		}
		if (level == 8) {
			maxLife = 200;
			numberOfCoinsSpawn = 7;
			hitPower = 15;
		}
		dir = (int)Mathf.Sign(transform.localScale.x);
		startScale = transform.localScale;
		startScale = new Vector3 (startScale.x * dir, startScale.y, startScale.z);
		UILife = GameObject.Find ("EnemyUI").GetComponent<MyUILife> ();
		lifeBar = GameObject.Find ("EnemyUI/HeadLifeInnerShelter");
		mainDay = GameObject.Find ("MainDay").GetComponent<MainDay> ();
		//anim = GetComponent<Animator> ();
		myAudio = GetComponent<AudioSource> ();
		life = maxLife;
		lifeStartScale = lifeBar.transform.localScale;
		hero = GameObject.Find ("GameWindow/Hero");
		if (headObject != null) {
			if (coHostName == "") {
				int randNu = Random.Range(0,3);
				if (randNu == 0) {
					coHostName = "Liam";
				}
				if (randNu == 1) {
					coHostName = "AndersLMadsen";
				}
				if (randNu == 2) {
					coHostName = "LarsHjortshoej";
				}
			}
			if (coHostName == "LarsHjortshoej") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/LarsHjortshoejHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/LarsHjortshoejHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/LarsHjortshoejHeadHurt");
				headIdle.transform.position += new Vector3(0,-0.02f,0);
				headAttack.transform.position += new Vector3(0,-0.02f,0);
				headHurt.transform.position += new Vector3(0,-0.02f,0);
			}
			if (coHostName == "AndersLMadsen") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHeadHurt");
				specialSound01 = Resources.Load<AudioClip> ("Sounds/AndersLMadsen02");
				specialSound02 = Resources.Load<AudioClip> ("Sounds/AndersLMadsen03");
			}
			if (coHostName == "Liam") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/LiamHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/LiamHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/LiamHeadHurt");
			}
			if (coHostName == "SoerenMalling") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/SoerenMallingHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/SoerenMallingHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/SoerenMallingHeadHurt");
			}
			if (coHostName == "Medina") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/MedinaHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/MedinaHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/MedinaHeadHurt");
				headIdle.transform.position += new Vector3(0,-0.04f,0);
				headAttack.transform.position += new Vector3(0,-0.04f,0);
				headHurt.transform.position += new Vector3(0,-0.04f,0);
				breast.GetComponent<Renderer>().enabled = true;
			}
			if (coHostName == "MartinBrygmann") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/MartinBrygmannHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/MartinBrygmannHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/MartinBrygmannHeadHurt");
				headIdle.transform.position += new Vector3(0,-0.02f,0);
				headAttack.transform.position += new Vector3(0,-0.02f,0);
				headHurt.transform.position += new Vector3(0,-0.02f,0);
			}
			if (coHostName == "HuxiBach") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/HuxiBachHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/HuxiBachHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/HuxiBachHeadHurt");
				headIdle.transform.position += new Vector3(0,0.02f,0);
				headAttack.transform.position += new Vector3(0,0.02f,0);
				headHurt.transform.position += new Vector3(0,0.02f,0);
			}
			if (coHostName == "AndersB") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/AndersBHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/AndersBHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/AndersBHeadHurt");
				headIdle.transform.position += new Vector3(-0.01f,0.02f,0);
				headAttack.transform.position += new Vector3(-0.01f,0.02f,0);
				headHurt.transform.position += new Vector3(-0.01f,0.02f,0);
			}
			if (coHostName == "LasseSjoerslev") {
				headIdle.sprite = Resources.Load<Sprite> ("Heads/LasseSjoerslevHead");
				headAttack.sprite = Resources.Load<Sprite> ("Heads/LasseSjoerslevHeadAttack");
				headHurt.sprite = Resources.Load<Sprite> ("Heads/LasseSjoerslevHeadHurt");
				headIdle.transform.position += new Vector3(-0.01f,-0.02f,0);
				headAttack.transform.position += new Vector3(-0.01f,-0.02f,0);
				headHurt.transform.position += new Vector3(-0.01f,-0.02f,0);
			}
			//headObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHead");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hurtCounter < 0) {
			if (isHittingCounter >= 0) {
				anim.SetInteger ("state", 2 + hitState);
				if (isHittingCounter < 10) {
					transform.position += new Vector3 (0.02f * dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
				}
				isHittingCounter += TimeScript.TimeConstant*Time.deltaTime;
			}
			if (isHittingCounter > 2) {
				attackHitbox.transform.position = new Vector3 (transform.position.x + 0.5f * dir, 5, transform.position.z);
			}
			if (isHittingCounter > 23) {
				isHittingCounter = -1;
			}
		}
		if (soundLength > 0) {
			soundLength --;
			if (soundLength == 0) {
				if (headIdle != null) {
					headIdle.enabled = true;
					headSpeak.enabled = false;
				}
			}
		}
	}
	void FixedUpdate () {
		/*lifeBar.transform.localScale = new Vector3 (lifeStartScale.x*life/maxLife,lifeStartScale.y,lifeStartScale.z);
		if (lifeBar.transform.localScale.x<0) {
			lifeBar.transform.localScale = new Vector3 (0,
			                                            lifeBar.transform.localScale.y,
			                                            lifeBar.transform.localScale.z);
		}*/
		if (isHittingCounter < 0) {
			anim.SetInteger ("state", 0);
			if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") {
				anim.SetInteger ("state", 6);
			}
			counter += TimeScript.TimeConstant*Time.deltaTime;
			if (counter > 100) {
				string tmpState = enemyState;
				if (enemyState == "goLeft" || enemyState == "goRight") {
						tmpState = "wait";
				}
				if (enemyState == "wait") {
						if (dir == 1) {
								tmpState = "goLeft";
						}
						if (dir == -1) {
								tmpState = "goRight";
						}
				}
				enemyState = tmpState;
				counter = 0;
			}
		}
		if (anim.GetInteger("state") == 0 && isWithinBounds()) {
			soundCounter ++;
			if (soundCounter>400) {
				playSpecialSound();
				soundCounter = 0;
			}
		}
		if (enemyState == "goLeft") {
			dir = -1;
			anim.SetInteger("state",1);
		}
		if (enemyState == "goRight") {
			dir = 1;
			anim.SetInteger("state",1);
		}
		if (enemyState == "goLeft" || enemyState == "goRight") {
			transform.position += new Vector3 (0.02f * dir, 0, 0);
		}
		if (!isOnGround) {
			gravity += 0.2f;
		}
		transform.position += new Vector3 (0, -gravity * 0.05f, 0)*TimeScript.TimeConstant*Time.deltaTime;
		transform.localScale = new Vector3 (startScale.x * dir, startScale.y, startScale.z);
		if ((transform.position.y+0.45f*Mathf.Abs(Mathf.Sin (transform.eulerAngles.z*Mathf.Deg2Rad))) < -1.35f) {
			if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") {
				if (gravity>1) {
					myAudio.PlayOneShot(impactGround01,1);
				}
				gravity *= -0.5f;
				hurtXSpeed *= 0.5f;
				if((Mathf.Round(hurtXSpeed*1000)/1000f) == 0) {
					if (dir == 1) {
						transform.eulerAngles = new Vector3(0,0,90);
					} else {
						transform.eulerAngles = new Vector3(0,0,-90);
					}
					enemyState = "hurtOnGround";
				}
			} else {
				gravity = 0;
				transform.eulerAngles = new Vector3(0,0,0);
				isOnGround = true;
			}
			transform.position = new Vector3 (transform.position.x, -1.35f-0.45f*Mathf.Abs(Mathf.Sin (transform.eulerAngles.z*Mathf.Deg2Rad)), transform.position.z);
		}
		if (enemyState == "hurtOnGround") {
			hurtOnGroundcounter ++;
			if (hurtOnGroundcounter > 100) {
				if (life <= 0) {
					/*GameObject tmpEnemy = (GameObject)Instantiate(spawnEnemy, new Vector3(0,3,transform.position.z),Quaternion.identity);
					tmpEnemy.transform.parent = transform.parent;
					tmpEnemy.GetComponent<Enemy>().wallHitbox.SetActive(true);
					tmpEnemy.GetComponent<Enemy>().groundHitbox.SetActive(true);
					tmpEnemy.name = name;
					tmpEnemy.GetComponent<Enemy>().coHostName = "";*/
					/*int randNu = Random.Range(0,2);
					if (randNu == 1) {
						tmpEnemy.GetComponent<Enemy>().coHostName = "LarsHjortshoej";
					} else {
						tmpEnemy.GetComponent<Enemy>().coHostName = "AndersLMadsen";
					}*/
					GameObject.Find ("Day"+ PlayerPrefs.GetInt ("currentLevel")+"Logic").GetComponent<DayEnemySpawner>().activeEnemies --;
					PlayerPrefs.SetInt(coHostName + "Defeated",1);
					Destroy(attackHitbox.gameObject);
					Destroy(gameObject);
				}
				wallHitbox.SetActive(true);
				groundHitbox.SetActive(true);
				enemyState = "wait";
				hurtOnGroundcounter = 0;
			}
		}
		if (enemyState == "hurtInAir" && !isOnGround) {
			wallHitbox.SetActive(false);
			groundHitbox.SetActive(false);
			transform.position += new Vector3(-hurtXSpeed*dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
			transform.eulerAngles += new Vector3(0,0,35*hurtXSpeed*dir);
		}
		if (hurtCounter>=0) {
			anim.SetInteger("state",4+hurtState%2);
			if (hurtCounter<10) {
				transform.position += new Vector3 (-0.01f * dir, 0, 0)*TimeScript.TimeConstant*Time.deltaTime;
			}
			hurtCounter += TimeScript.TimeConstant*Time.deltaTime;
		}
		if (hurtCounter>15) {
			enemyState = "wait";
			hurtCounter = -1;
		}
		if (dir == 1 && hero.transform.position.x>transform.position.x &&
		    hero.transform.position.x<transform.position.x+2) {
			hitDelayCount += TimeScript.TimeConstant*Time.deltaTime;
			if (hitDelayCount>20) {
				startHit();
				hitDelayCount = 0;
			}
		}
		if (dir == -1 && hero.transform.position.x>transform.position.x-2 &&
		    hero.transform.position.x<transform.position.x) {
			hitDelayCount ++;
			if (hitDelayCount>20) {
				startHit();
				hitDelayCount = 0;
			}
		}
	}
	public void startHurt(int damage) {
		if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") return;
		if (hurtCounter < 0) {
			if (hurtState > 4 || life <= 0) {
				gravity = -3;
				isOnGround = false;
				transform.position += new Vector3(0.02f*dir, 0, 0);
				hurtXSpeed = 0.06f;
				enemyState = "hurtInAir";
				hurtState = 0;
			} else {
				enemyState = "hurt";
				hurtState ++;
				hurtCounter = 0;
			}
			if (life <= 0) {
				//mainDay.defeatedEnemies ++;
				//hero.GetComponent<Hero>().xp += 2;
				return;
			}
			int randN = Random.Range(0,2);
			if (hurtState%2 == 0) {
				if (randN == 0) {
					myAudio.PlayOneShot(impactSound01,1);
				} else {
					myAudio.PlayOneShot(impactSound02,2);
				}
			}
			if (hurtState%2 == 1) {
				if (randN == 0) {
					myAudio.PlayOneShot(impactSound03,1);
				} else {
					myAudio.PlayOneShot(impactSound04,2);
				}
			}
			GameObject tmpAttackDamage = (GameObject)Instantiate(attackDamage,new Vector3 (transform.position.x,transform.position.y,transform.position.z-1), Quaternion.identity);
			int tmpDamage = damage;//1+Random.Range(0,2);
			life -= tmpDamage;
			tmpAttackDamage.GetComponent<AttackDamage>().damage = tmpDamage;
			tmpAttackDamage.transform.parent = transform.parent;
			UILife.makeVisible(this);


			float blockHealth = maxLife/numberOfCoinsSpawn;
			int tmpCoinSpawnInt = Mathf.RoundToInt((maxLife-Mathf.Clamp(life,0,maxLife))/blockHealth);
			while (tmpCoinSpawnInt > actualCoinSpawn) {
				GameObject tmpcoin = (GameObject)Instantiate(coin,new Vector3 (transform.position.x,transform.position.y,transform.position.z-1), Quaternion.identity);
				float coinRandomSpeed = Random.Range(120,200)*0.0001f;
				tmpcoin.GetComponent<Coin>().xSpeed = -dir*coinRandomSpeed;
				tmpcoin.transform.parent = transform.parent;
				actualCoinSpawn ++;
			}

			if (life <= 0 && (enemyState == "hurt" || enemyState == "hurtInAir")) {
				hurtCounter = -1;
				hurtState = 5;
				mainDay.defeatedEnemies ++;
				hero.GetComponent<Hero>().xp += level;
				startHurt(damage);
			}
		}
	}
	public void startHit() {
		if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") return;
		if (hurtCounter < 0) {
			if (isHittingCounter < 0) {
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
	}
	void playSpecialSound() {
		if (headIdle == null) {  
			return;
		}
		if (coHostName != "Liam" && coHostName != "AndersLMadsen") {
			return;
		}
		if (coHostName == "Liam") {
			headIdle.enabled = false;
			headSpeak.enabled = true;
		}
		int randN = Random.Range(0,2);

		AudioClip tmpSound;
		if (randN == 0) {
			tmpSound = specialSound01;
		} else {
			tmpSound = specialSound02;
		}
		soundLength = Mathf.FloorToInt(tmpSound.length * 60);
		myAudio.PlayOneShot(tmpSound,4);
	}
	bool isWithinBounds() {
		return (transform.position.x > -4.4 && transform.position.x < 4.4);
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
			if (other.transform.parent.name == "Item") {
				other.transform.parent.GetComponent<Item>().hit();
			}
			startHurt(other.GetComponent<AttackHitbox>().damage);
		}
	}
	void OnTriggerStay(Collider other) {
		if (other.name == "Hero" || other.name == "Enemy") {
			if (enemyState == "goLeft" || enemyState == "goRight" || enemyState == "wait") {
				if (dir == 1 && other.transform.position.x>transform.position.x) {
					enemyState = "wait";
					/*hitDelayCount ++;
					if (other.name == "Hero" && hitDelayCount>20) {
						startHit();
						hitDelayCount = 0;
					}*/
				}
				if (dir == -1 && other.transform.position.x<transform.position.x) {
					enemyState = "wait";
					/*hitDelayCount ++;
					if (other.name == "Hero" && hitDelayCount>20) {
						startHit();
						hitDelayCount = 0;
					}*/
				}
			}
		}
		if (other.name == "Wall") {
			int wallDir = -1;
			if (other.transform.position.x<transform.position.x) {
				wallDir = 1;
			}
			if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") {
				//hurtXSpeed *= -1;
			}
			transform.position = new Vector3(other.transform.position.x+(other.bounds.size.x/2f+transform.GetComponent<Collider>().bounds.size.x/2f+0.05f)*wallDir,
			                                 transform.position.y,
			                                 transform.position.z);
		}
		if (other.name == "Ground" && gravity>0) {
			gravity = 0;
			if (enemyState == "hurtInAir" || enemyState == "hurtOnGround") {
				if (gravity>1) {
					myAudio.PlayOneShot(impactGround01,1);
				}
				//gravity *= -0.5f;
				gravity = 35*-hurtXSpeed;
				hurtXSpeed *= 0.6f;
				if((Mathf.Round(hurtXSpeed*1000)/1000f) == 0) {
					if (dir == 1) {
						transform.eulerAngles = new Vector3(0,0,90);
					} else {
						transform.eulerAngles = new Vector3(0,0,-90);
					}
					enemyState = "hurtOnGround";
				}
			} else {
				transform.eulerAngles = new Vector3(0,0,0);
				isOnGround = true;
			}
			transform.position = new Vector3(transform.position.x,
			                                 other.transform.position.y+other.bounds.size.y/2f+(GetComponent<Collider>().bounds.size.y/2f-(transform.GetComponent<Collider>().bounds.center.y-transform.position.y
			                                                                                              +0.1f*Mathf.Abs(Mathf.Sin (transform.eulerAngles.z*Mathf.Deg2Rad)))),
			                                 transform.position.z);
			//isOnGround = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.name == "Ground") {
			isOnGround = false;
		}
	}
}
