using UnityEngine;
using System.Collections;

public class ClockLogic : MonoBehaviour {

	int counter = 0;
	int hour = 0;
	int minute = 0;
	int seconds = 0;

	public GameObject hourArm;
	public GameObject minuteArm;
	public GameObject secondArm;

	// Use this for initialization
	void Start () {
		seconds = Random.Range (0, 60 * 60 * 12);
	}
	
	// Update is called once per frame
	void Update () {
		counter ++;
		if (counter>30) {
			seconds ++;
			minute = (seconds/60);
			hour = (minute/60);
			hourArm.transform.eulerAngles = new Vector3 (hourArm.transform.eulerAngles.x, hourArm.transform.eulerAngles.y, hour/12f*-360);
			minuteArm.transform.eulerAngles = new Vector3 (minuteArm.transform.eulerAngles.x, minuteArm.transform.eulerAngles.y, minute/60f*-360);
			secondArm.transform.eulerAngles = new Vector3 (secondArm.transform.eulerAngles.x, secondArm.transform.eulerAngles.y, seconds/60f*-360);
			counter = 0;
		}
	}
}
