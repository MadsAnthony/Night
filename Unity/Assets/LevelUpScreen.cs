using UnityEngine;
using System.Collections;

public class LevelUpScreen : MonoBehaviour {
	Vector3 heroOldPos;
	Vector3 heroOldScale;
	int activateCounter = -1;
	AudioSource myAudio;
	public Hero hero;
	public SpriteRenderer BG;
	public SpriteRenderer grid;
	public GameObject hand1;
	public GameObject hand2;
	public GameObject hand3;
	public GameObject hand4;
	public GameObject hand5;
	public GameObject hand6;
	public AudioClip levelUpMusic;
	public GameObject levelUp;
	public GameObject levelText;
	public LightFlash lightFlash;

	bool hasFlashed;
	float myAlpha = 0;
	// Use this for initialization
	void Start () {
		grid.color = new Color(grid.color.r,grid.color.g,grid.color.b,0);
		BG.color = new Color(BG.color.r,BG.color.g,BG.color.b,0);
		myAudio = GetComponent<AudioSource> ();
		levelUp.GetComponent<Renderer>().enabled = false;
		levelText.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (activateCounter >= 0) {
			activateCounter ++;
			if (activateCounter<10) {
				hasFlashed = false;
				hero.GetComponent<Hero> ().anim.SetInteger ("state", 7);
			}
			if (activateCounter==15) {
				hero.GetComponent<Hero> ().anim.SetInteger ("state", -1);
			}
			if (activateCounter<60*5) {
				myAlpha += 0.03f;
			}
			if (hero.GetComponent<Hero> ().anim.GetCurrentAnimatorStateInfo (0).IsName ("ManLevelUp2Idle")) {
				hero.transform.localPosition = new Vector3 (heroOldPos.x-0.03f, heroOldPos.y-0.03f, -9);
			}
			if (activateCounter<60*6+10) {
				if (activateCounter>60*2-4 && !hasFlashed) {
					lightFlash.showFlash(0.1f);
					hasFlashed = true;
				}
				if (activateCounter>60*2) {
					levelUp.GetComponent<Renderer>().enabled = true;
					levelText.GetComponent<Renderer>().enabled = true;
					hand1.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand1.transform.localPosition);
					//hand2.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand2.transform.localPosition);
					hand3.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand3.transform.localPosition);
					//hand4.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand4.transform.localPosition);
					hand5.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand5.transform.localPosition);
					hand6.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand6.transform.localPosition);
				}
				if (activateCounter>60*2+10) {
					hand2.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand2.transform.localPosition);
					hand4.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand4.transform.localPosition);
				}
			}
			if (activateCounter>60*5) {
				myAlpha -= 0.03f;
				if (hero.mainDay.isPaused) {
					hero.GetComponent<Hero> ().anim.SetInteger ("state", 0);
				}
			}
			if (activateCounter==60*5+40) {
				hero.transform.localPosition = new Vector3 (heroOldPos.x, heroOldPos.y, heroOldPos.z);
				hero.transform.localScale = heroOldScale;
			}
			if (activateCounter==60*5+50) {
				GameObject.Find ("Day"+ PlayerPrefs.GetInt ("currentLevel")+"Logic").GetComponent<DayEnemySpawner>().timeOutCounter = 60*3;
				hero.mainDay.isPaused = false;
			}
			if (activateCounter>60*7) {
				levelUp.GetComponent<Renderer>().enabled = false;
				levelText.GetComponent<Renderer>().enabled = false;
				hand2.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand2.transform.localPosition);
				hand4.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand4.transform.localPosition);
			}
			if (activateCounter>60*7+10) {
				hand1.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand1.transform.localPosition);
				//hand2.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand2.transform.localPosition);
				hand3.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand3.transform.localPosition);
				//hand4.transform.localPosition += 0.05f*(new Vector3(0,0,0)-hand4.transform.localPosition);
				hand5.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand5.transform.localPosition);
				hand6.transform.localPosition += 0.01f*(new Vector3(0,-4,0)-hand6.transform.localPosition);
			}
			if (activateCounter>60*9+10) {
				transform.position = new Vector3(1000,0,transform.position.z);
				activateCounter = -1;
			}
			myAlpha = Mathf.Clamp(myAlpha,0,1);
			grid.color = new Color(grid.color.r,grid.color.g,grid.color.b,myAlpha);
			BG.color = new Color(BG.color.r,BG.color.g,BG.color.b,myAlpha);
		}
	}
	public void activateScreen() {
		if (activateCounter < 0) {
			levelText.GetComponent<TypogenicText>().Text = "LEVEL "+hero.level;
			heroOldPos = hero.transform.localPosition;
			heroOldScale = hero.transform.localScale;
			transform.position = new Vector3 (0, 0, transform.position.z);
			hero.GetComponent<Hero> ().anim.SetInteger ("state", 7);
			hero.mainDay.isPaused = true;
			hero.transform.localScale = new Vector3 (Mathf.Abs (heroOldScale.x), heroOldScale.y, heroOldScale.z);
			hero.transform.localPosition = new Vector3 (heroOldPos.x - 0.02f, heroOldPos.y - 0.02f, -9);
			hero.GetComponent<Hero> ().hideItem();
			myAudio.PlayOneShot(levelUpMusic,1);
			activateCounter = 0;
		}
	}
}
