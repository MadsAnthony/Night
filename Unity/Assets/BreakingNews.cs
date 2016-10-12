using UnityEngine;
using System.Collections;

public class BreakingNews : MonoBehaviour {
	Vector3 startPos;
	Vector3 upPos;
	Vector3 targetPos;
	bool isShowingBreaking;

	public GameObject text;
	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		upPos = new Vector3 (startPos.x,-2,startPos.z);
		targetPos = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(targetPos.y-transform.localPosition.y)>0.01f || isShowingBreaking) {
			transform.localPosition += 0.05f * (targetPos - transform.localPosition);
			text.transform.localPosition += new Vector3 (-0.02f, 0, 0);
		}
	}
	public void showBreaking() {
		targetPos = upPos;
		isShowingBreaking = true;
		Invoke("hideBreaking",12);
	}
	void hideBreaking() {
		isShowingBreaking = false;
		targetPos = startPos;
	}
}
