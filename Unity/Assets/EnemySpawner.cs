using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public enum SpawnType{Enemy,Brake,SpeedChange,WaitTimer,Boss,Condition};
	public enum EnemyType{AnderBreinholt,Liam,AnderLundMadsen,LarsHjortshoej,SoerenMalling,Medina,MartinBrygmann,HuxiBach,LasseSjoerslev};

	public float t;
	public SpawnType type;
	public int timer;
	public EnemyType enemyType;
	public int enemyLvl;
	public GameObject spawnEnemy;
	SpriteRenderer sprRend;
	public bool hasExecuted = false;
	public GameObject spawnAttack;
	GameObject gameWindow;
	public GameObject boss1;
	GameObject hero;
	// Use this for initialization
	void Start () {
		if (!transform.parent.GetComponent<DayEnemySpawner> ().checkIfActive ()) {
			return;
		}
		transform.parent.GetComponent<DayEnemySpawner> ().addToEnemySpawnList (this);
		gameWindow = transform.parent.GetComponent<DayEnemySpawner> ().mainDay.gameWindow;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnDrawGizmos()
	{
		t = Mathf.Clamp01 (t);
		transform.position = new Vector3 (transform.parent.position.x-1+2*t,
		                                  transform.parent.position.y-1.5f,
		                                  transform.parent.position.z);
		if (type == SpawnType.Enemy) {
			handleEnemySpawn();
		}
		if (type == SpawnType.Brake) {
			handleBrakeSpawn();
		}
		if (type == SpawnType.SpeedChange) {
			handleSpeedChangeSpawn();
		}
		if (type == SpawnType.WaitTimer) {
			handleWaitTimerSpawn();
		}
	}
	void handleEnemySpawn() {
		Gizmos.color = new Color (1, 1, 1);
		Gizmos.DrawLine (new Vector3 (transform.position.x, transform.position.y, transform.position.z),
		                 new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z));
		if (GetComponent<SpriteRenderer> () == null) {
			sprRend = gameObject.AddComponent<SpriteRenderer> ();
		}
		sprRend = GetComponent<SpriteRenderer> ();
		sprRend.sprite = getSprite(enemyType);
		/*if (enemyType == EnemyType.Liam) {
			sprRend.sprite = Resources.Load<Sprite> ("Heads/LiamHead");
		}
		if (enemyType == EnemyType.AnderLundMadsen) {
			sprRend.sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHead");
		}
		if (enemyType == EnemyType.LarsHjortshoej) {
			sprRend.sprite = Resources.Load<Sprite> ("Heads/LarsHjortshoejHead");
		}
		if (enemyType == EnemyType.SoerenMalling) {
			sprRend.sprite = Resources.Load<Sprite> ("Heads/SoerenMallingHead");
		}
		if (enemyType == EnemyType.Medina) {
			sprRend.sprite = Resources.Load<Sprite> ("Heads/MedinaHead");
		}*/
	}
	void handleBrakeSpawn() {
		Gizmos.color = new Color (1, 1, 1);
		Gizmos.DrawCube (transform.position+new Vector3(0,1,0), new Vector3 (0.05f,0.25f,0));
		if (sprRend != null) {
			sprRend.sprite = null;
		}
	}
	void handleSpeedChangeSpawn() {
		Gizmos.color = new Color (1, 233/255f, 122/255f);
		Gizmos.DrawCube (transform.position+new Vector3(0,1,0), new Vector3 (0.05f,0.4f,0));
		if (sprRend != null) {
			sprRend.sprite = null;
		}
	}
	void handleWaitTimerSpawn() {
		Gizmos.color = new Color (1, 88/255f, 88/255f);
		Gizmos.DrawCube (transform.position+new Vector3(0,1,0), new Vector3 (0.05f,0.4f,0));
		if (sprRend != null) {
			sprRend.sprite = null;
		}
	}
	public void executeAction() {
		if (!hasExecuted) {
			if (type == SpawnType.Enemy) {
				GameObject tmpEnemy = (GameObject)Instantiate(spawnEnemy, new Vector3(2,4,-0.45f),Quaternion.identity);
				tmpEnemy.transform.parent = gameWindow.transform;
				tmpEnemy.GetComponent<Enemy>().wallHitbox.SetActive(true);
				tmpEnemy.GetComponent<Enemy>().groundHitbox.SetActive(true);
				tmpEnemy.GetComponent<Enemy>().level = enemyLvl;
				tmpEnemy.transform.localPosition = new Vector3(9,4,-0.75f);
				tmpEnemy.name = "Enemy";
				GameObject tmpAttack = (GameObject)Instantiate(spawnAttack, new Vector3(2,4,-0.45f),Quaternion.identity);
				tmpEnemy.GetComponent<Enemy>().attackHitbox = tmpAttack;
				tmpAttack.transform.parent = gameWindow.transform;
				tmpAttack.name = "Attack";
				string coHostname = getString(enemyType);
				if (PlayerPrefs.HasKey ("hostName")) {
					if (getType (PlayerPrefs.GetString ("hostName")) == enemyType) {
						coHostname = getString(EnemyType.AnderBreinholt);
					}
				}
				/*if (enemyType == EnemyType.Liam) {
					coHostname = "Liam";
				}
				if (enemyType == EnemyType.AnderLundMadsen) {
					coHostname = "AndersLMadsen";
				}
				if (enemyType == EnemyType.LarsHjortshoej) {
					coHostname = "LarsHjortshoej";
				}
				if (enemyType == EnemyType.SoerenMalling) {
					coHostname = "SoerenMalling";
				}
				if (enemyType == EnemyType.Medina) {
					coHostname = "Medina";
				}*/
				tmpEnemy.GetComponent<Enemy>().coHostName = coHostname;
				transform.parent.GetComponent<DayEnemySpawner> ().activeEnemies ++;
			}
			if (type == SpawnType.Brake) {
				transform.parent.GetComponent<DayEnemySpawner> ().brakeIsOn = true;
			}
			if (type == SpawnType.Boss) {
				GameObject tmpBoss = (GameObject)Instantiate(boss1, new Vector3(2,4,-1),Quaternion.identity);
				tmpBoss.transform.parent = gameWindow.transform;
				tmpBoss.transform.localPosition = new Vector3(15,2.5f,-1f);
				tmpBoss.name = "HelicopterShelter";
				transform.parent.GetComponent<DayEnemySpawner> ().mainDay.totalEnemies ++;
				transform.parent.GetComponent<DayEnemySpawner> ().activeEnemies ++;
			}
			if (type == SpawnType.Condition) {
				transform.parent.GetComponent<DayEnemySpawner> ().brakeIsOn = true;
				transform.parent.GetComponent<DayEnemySpawner> ().someFunction = this;
			}
			hasExecuted = true;
		}
	}
	public static Sprite getSprite(EnemyType type) {
		Sprite tmpSprite = Resources.Load<Sprite> ("Heads/AndersBHead");
		tmpSprite = Resources.Load<Sprite> ("Heads/"+getString (type) + "Head");
		return tmpSprite;
	}
	public static string getString(EnemyType type) {
		string tmpString = "Liam";
		if (type == EnemyType.AnderBreinholt) {
			tmpString = "AndersB";
		}
		if (type == EnemyType.Liam) {
			tmpString = "Liam";
		}
		if (type == EnemyType.AnderLundMadsen) {
			tmpString = "AndersLMadsen";
		}
		if (type == EnemyType.LarsHjortshoej) {
			tmpString = "LarsHjortshoej";
		}
		if (type == EnemyType.SoerenMalling) {
			tmpString = "SoerenMalling";
		}
		if (type == EnemyType.Medina) {
			tmpString = "Medina";
		}
		if (type == EnemyType.MartinBrygmann) {
			tmpString = "MartinBrygmann";
		}
		if (type == EnemyType.HuxiBach) {
			tmpString = "HuxiBach";
		}
		if (type == EnemyType.LasseSjoerslev) {
			tmpString = "LasseSjoerslev";
		}
		return tmpString;
	}
	public static EnemyType getType(string strType) {
		EnemyType tmpType = EnemyType.AnderBreinholt;
		if (strType == "AndersB") {
			tmpType = EnemyType.AnderBreinholt;
		}
		if (strType == "Liam") {
			tmpType = EnemyType.Liam;
		}
		if (strType == "AndersLMadsen") {
			tmpType = EnemyType.AnderLundMadsen;
		}
		if (strType == "LarsHjortshoej") {
			tmpType = EnemyType.LarsHjortshoej;
		}
		if (strType == "SoerenMalling") {
			tmpType = EnemyType.SoerenMalling;
		}
		if (strType == "Medina") {
			tmpType = EnemyType.Medina;
		}
		if (strType == "MartinBrygmann") {
			tmpType = EnemyType.MartinBrygmann;
		}
		if (strType == "HuxiBach") {
			tmpType = EnemyType.HuxiBach;
		}
		if (strType == "LasseSjoerslev") {
			tmpType = EnemyType.LasseSjoerslev;
		}
		return tmpType;
	}
	public static string getName(EnemyType type) {
		string tmpString = "Liam";
		if (type == EnemyType.AnderBreinholt) {
			tmpString = "Anders Breinholt";
		}
		if (type == EnemyType.Liam) {
			tmpString = "Liam O'Connor";
		}
		if (type == EnemyType.AnderLundMadsen) {
			tmpString = "Anders Lund \n Madsen";
		}
		if (type == EnemyType.LarsHjortshoej) {
			tmpString = "Lars Hjortshøj";
		}
		if (type == EnemyType.SoerenMalling) {
			tmpString = "Søren Malling";
		}
		if (type == EnemyType.Medina) {
			tmpString = "Medina";
		}
		if (type == EnemyType.MartinBrygmann) {
			tmpString = "Martin Brygmann";
		}
		if (type == EnemyType.HuxiBach) {
			tmpString = "Huxi Bach";
		}
		if (type == EnemyType.LasseSjoerslev) {
			tmpString = "Lasse Sjørslev";
		}
		return tmpString;
	}
	public static int getSelectNumber(EnemyType type) {
		int tmpInt = 0;
		if (type == EnemyType.AnderBreinholt) {
			tmpInt = 0;
		}
		if (type == EnemyType.Liam) {
			tmpInt = 1;
		}
		if (type == EnemyType.AnderLundMadsen) {
			tmpInt = 2;
		}
		if (type == EnemyType.LarsHjortshoej) {
			tmpInt = 3;
		}
		if (type == EnemyType.SoerenMalling) {
			tmpInt = 4;
		}
		if (type == EnemyType.MartinBrygmann) {
			tmpInt = 5;
		}
		if (type == EnemyType.HuxiBach) {
			tmpInt = 6;
		}
		if (type == EnemyType.LasseSjoerslev) {
			tmpInt = 7;
		}
		return tmpInt;
	}
	public static Vector3 getMoveVector(EnemyType type) {
		Vector3 tmpVector = new Vector3(0,0,0);
		if (type == EnemyType.AnderBreinholt) {
			tmpVector = new Vector3(-0.01f,0.02f,0);
		}
		if (type == EnemyType.Liam) {
			//tmpVector = new Vector3(0,0,0);
		}
		if (type == EnemyType.AnderLundMadsen) {
			//tmpVector = new Vector3(0,0,0);
		}
		if (type == EnemyType.LarsHjortshoej) {
			tmpVector = new Vector3(0,-0.02f,0);
		}
		if (type == EnemyType.SoerenMalling) {
			//tmpVector = new Vector3(0,0,0);
		}
		if (type == EnemyType.Medina) {
			tmpVector = new Vector3(0,-0.04f,0);
		}
		if (type == EnemyType.MartinBrygmann) {
			tmpVector = new Vector3(0,-0.02f,0);
		}
		if (type == EnemyType.HuxiBach) {
			tmpVector = new Vector3(0,0.02f,0);
		}
		return tmpVector;
	}
	public bool someFunction1() {
		if (hero == null) {
			hero = GameObject.Find("GameWindow/Hero");
		}
		return hero.transform.localPosition.x > 8;
	}
}
