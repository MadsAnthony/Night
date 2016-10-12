using UnityEngine;
using System.Collections;

public class LevelSelectBlock : MonoBehaviour {
	MainMenu mainMenu;
	public int dayNumber;
	public ClickHitbox myHitbox;
	public SpriteRenderer blockActive;
	public SpriteRenderer blockInActive;
	public TypogenicText text;
	public TypogenicText text2;
	public GameObject checkMark;
	// Use this for initialization
	void Start () {
		checkMark.GetComponent<Renderer>().enabled = false;
		if (PlayerPrefs.GetInt("day"+dayNumber+"Completed") == 1) {
			checkMark.GetComponent<Renderer>().enabled = true;
		}
		mainMenu = GameObject.Find ("MainMenu").GetComponent<MainMenu>();
		mainMenu.initLevelSelectBlockArray ();
		mainMenu.levelSelectBlockArray [dayNumber] = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (dayNumber <= mainMenu.levelProgress) {
			blockActive.enabled = true;
			blockInActive.enabled = false;
		} else {
			blockActive.enabled = false;
			blockInActive.enabled = true;
		}
		if (dayNumber != 6) {
			text.Text = "Day " + dayNumber;
		} else {
			text.Text = "Day ";
			text2.Text = "6";
		}
		if (myHitbox.isClicked && blockActive.enabled) {
			mainMenu.prepareLevel = true;

			mainMenu.gotoLevel = dayNumber;
			myHitbox.isClicked = false;
		}
	}
}
