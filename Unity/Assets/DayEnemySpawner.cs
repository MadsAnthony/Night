using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayEnemySpawner : MonoBehaviour {

	public float t;
	public float tStartSpeed = 0.01f;
	public float tSpeed;
	public List<EnemySpawner> enemySpawnList;
	public int activeEnemies = 0;
	public bool brakeIsOn = false;
	public int timeOutCounter;
	public MainDay mainDay;
	public EnemySpawner someFunction;
	// Use this for initialization
	void Start () {
		checkIfActive ();
		tSpeed = tStartSpeed;
		//mainDay = GameObject.Find ("MainDay").GetComponent<MainDay>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mainDay.isPaused) {
			return;
		}
		if (brakeIsOn && activeEnemies == 0 && someFunction == null) {
			brakeIsOn = false;
		}
		if (someFunction != null) {
			if (someFunction.someFunction1()) {
				brakeIsOn = false;
				someFunction = null;
			}
		}
		if (brakeIsOn) {
			return;
		}
		t += tSpeed;
		foreach (EnemySpawner enemySpawn in enemySpawnList) {
			if (t>enemySpawn.t) {
				enemySpawn.executeAction();
				if (!enemySpawn.hasExecuted && enemySpawn.type == EnemySpawner.SpawnType.Brake)  {
					t = enemySpawn.t;
				}
			}
		}
		if (timeOutCounter > 0) {
			timeOutCounter --;
		}
		if (t >= 1 && timeOutCounter == 0) {
			mainDay.activateWinScreen();
		}
	}
	void OnDrawGizmos()
	{
		t = Mathf.Clamp01 (t);
		Gizmos.DrawLine(new Vector3(transform.position.x-1, transform.position.y-0.5f, transform.position.z),
		                new Vector3(transform.position.x+1, transform.position.y-0.5f, transform.position.z));
		Gizmos.color = new Color (1,88/255f,88/255f);
		Gizmos.DrawSphere(new Vector3(transform.position.x-1, transform.position.y-0.5f, transform.position.z),0.05f);
		Gizmos.color = new Color (88/255f,1,88/255f);
		Gizmos.DrawSphere(new Vector3(transform.position.x+1, transform.position.y-0.5f, transform.position.z),0.05f);
		Gizmos.color = new Color (1,1,1);
		Gizmos.DrawSphere(new Vector3(transform.position.x-1+2*t, transform.position.y-0.5f, transform.position.z),0.05f);
	}
	public void addToEnemySpawnList(EnemySpawner enemySpawner) {
		checkIfActive ();
		if (!gameObject.activeSelf) {
			return;
		}
		if (mainDay == null) {
			mainDay = GameObject.Find ("MainDay").GetComponent<MainDay>();
		}
		if (enemySpawnList == null) {
			enemySpawnList = new List<EnemySpawner>();
		}
		if (enemySpawner.type == EnemySpawner.SpawnType.Enemy) {
			mainDay.totalEnemies ++;
		}
		enemySpawnList.Add (enemySpawner);
	}
	public bool checkIfActive() {
		if (name != "Day" + PlayerPrefs.GetInt ("currentLevel") + "Logic") {
			gameObject.SetActive(false);
			return false;
		}
		return true;
	}
}
