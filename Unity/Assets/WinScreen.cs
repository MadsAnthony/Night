using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {
	bool isSpinning = false;
	int spinCounter;
	float spinSpeed;
	public ClickHitbox clickHitbox;
	public GameObject wheelPins;
	public GameObject wheelBack;
	public GameObject wheelArrowPart1;
	float arrowGravity = 2;
	float rotationPinVar = 10;
	AudioSource myAudio;
	public AudioClip tick01;
	public AudioClip tick02;
	string state = "winScreen";
	public GameObject DayText;
	public GameObject spinScreen;
	public GameObject spinText;
	public AudioClip clip01;
	public AudioClip clip02;
	public AudioClip clip03;
	int doneSpinningCounter = 0;
	int myActiveCounter = -1;
	float fadeAlpha = 0;
	public SpriteRenderer fadeBack;
	public GameObject timeSpent;
	public GameObject timeSpentValue;
	public GameObject coinsCollected;
	public GameObject coinsCollectedValue;
	public GameObject transitionReverse;
	public AudioSource breakerAudio;
	public AudioClip kortNyt;
	public GameObject KliphjuletIconFail1;
	public GameObject KliphjuletIconFail2;
	public GameObject KliphjuletIconOk1;
	public GameObject KliphjuletIconOk2;
	public GameObject KliphjuletIconJackpot1;
	public GameObject KliphjuletIconJackpot2;
	public GameObject CoinGraphicFly;
	GameObject tmpIcon;
	int reward;
	int currentJackpot = 10;
	int currentOk = 5;
	int startNumberOfCoins;
	int timeCounter = 0;
	int seconds;
	int minute;
	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource> ();
		DayText.SetActive(true);
		spinScreen.SetActive (false);
		DayText.GetComponent<Renderer>().enabled = false;
		timeSpent.GetComponent<Renderer>().enabled = false;
		timeSpentValue.GetComponent<Renderer>().enabled = false;
		coinsCollected.GetComponent<Renderer>().enabled = false;
		coinsCollectedValue.GetComponent<Renderer>().enabled = false;
		DayText.GetComponent<TypogenicText> ().Text = "Day "+ PlayerPrefs.GetInt ("currentLevel") +" completed!";
		startNumberOfCoins = PlayerPrefs.GetInt ("numberOfCoins");
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter ++;
		if (timeCounter>=60) {
			seconds += 1;
			timeCounter = 0;
		}
		if (seconds>=60) {
			minute += 1;
			seconds = 0;
		}
		if (myActiveCounter>=0) {
			if (myActiveCounter == 0) {
				myAudio.PlayOneShot(kortNyt,2);
			}
			myActiveCounter ++;
			fadeAlpha += 0.05f;
			fadeAlpha = Mathf.Clamp(fadeAlpha,0,170/255f);
			fadeBack.color = new Color(fadeBack.color.r,fadeBack.color.g,fadeBack.color.b,fadeAlpha);
			if (myActiveCounter>40) {
				DayText.GetComponent<Renderer>().enabled = true;
			}
			if (myActiveCounter>80) {
				timeSpent.GetComponent<Renderer>().enabled = true;
			}
			if (myActiveCounter>140) {
				if (myActiveCounter==141) {
					string minuteStr = ""+minute;
					if (minute<=9) {
						minuteStr = "0"+minute;
					}
					string secondsStr = ""+seconds;
					if (seconds<=9) {
						secondsStr = "0"+seconds;
					}
					timeSpentValue.GetComponent<TypogenicText>().Text = minuteStr+":"+secondsStr;
				}
				timeSpentValue.GetComponent<Renderer>().enabled = true;
			}
			if (myActiveCounter>200) {
				coinsCollected.GetComponent<Renderer>().enabled = true;
			}
			if (myActiveCounter>260) {
				coinsCollectedValue.GetComponent<TypogenicText>().Text = ""+(PlayerPrefs.GetInt ("numberOfCoins")-startNumberOfCoins);
				coinsCollectedValue.GetComponent<Renderer>().enabled = true;
			}
			if (myActiveCounter==350) {
				state = "showSpinScreen";
				spinScreen.transform.position = new Vector3(0,-5,spinScreen.transform.position.z);
			}
		}
		if (clickHitbox.isClicked) {
			if (myActiveCounter>260 && myActiveCounter<350) {
				myActiveCounter = 349;
			}
			if (myActiveCounter>200 && myActiveCounter<260) {
				myActiveCounter = 260;
			}
			if (myActiveCounter>140 && myActiveCounter<200) {
				myActiveCounter = 200;
			}
			if (myActiveCounter>80 && myActiveCounter<140) {
				myActiveCounter = 140;
			}
			if (myActiveCounter>40 && myActiveCounter<80) {
				myActiveCounter = 80;
			}
			/*if (myActiveCounter<40) {
				myActiveCounter = 40;
			}*/
			/*if (state == "winScreen") {
				state = "showSpinScreen";
				spinScreen.transform.position = new Vector3(0,-5,spinScreen.transform.position.z);
			}*/
			if (state == "spinScreen") {
				spin();
			}
			clickHitbox.isClicked = false;
		}
		if (state == "doneSpinning") {
			if (reward>0 && doneSpinningCounter%10 == 0) {
				Instantiate(CoinGraphicFly,tmpIcon.transform.position,Quaternion.identity);
				reward --;
			}
			doneSpinningCounter ++;
			if (reward>0 && doneSpinningCounter==60*3-2) {
				doneSpinningCounter = 0;
			}
			if (doneSpinningCounter==60*3-1) {
				transitionReverse.GetComponent<Animator> ().SetInteger ("state", 1);
			}
			if (doneSpinningCounter==60*3) {
				transitionReverse.GetComponent<Animator>().SetInteger ("state",0);
				transitionReverse.transform.position = new Vector3 (1, -0.95f, transitionReverse.transform.position.z);
			}
			if (doneSpinningCounter==60*3+10) {
				breakerAudio.Play();
			}
			if (doneSpinningCounter==60*3+200) {
				PlayerPrefs.SetInt("day"+PlayerPrefs.GetInt("currentLevel")+"Completed",1);
				PlayerPrefs.SetString("MenuGotoScreen","levelSelect");
				if (PlayerPrefs.GetInt ("levelProgress")==PlayerPrefs.GetInt("currentLevel")) {
					PlayerPrefs.SetInt("levelProgress",PlayerPrefs.GetInt("currentLevel")+1);
				}
				Application.LoadLevel ("Menu");
			}
		}
		if (state == "showSpinScreen") {
			DayText.SetActive(false);
			timeSpent.SetActive(false);
			timeSpentValue.SetActive(false);
			coinsCollected.SetActive(false);
			coinsCollectedValue.SetActive(false);
			spinScreen.SetActive (true);
			spinScreen.transform.position += 0.08f*(new Vector3(0,0,0)-new Vector3(spinScreen.transform.position.x,spinScreen.transform.position.y,0));
			if (Mathf.Abs (spinScreen.transform.position.y)<0.05f) {
				spinText.SetActive(true);
				state = "spinScreen";
			}
		}
		if (isSpinning) {
			spinCounter ++;
			if (spinCounter>60) {
				spinSpeed *= 0.985f;
			}
			if (rotationPinVar>20 && wheelArrowPart1.transform.eulerAngles.z<20) {
				arrowGravity = 8;
				int randI = Random.Range(0,3);
				if (randI == 0) {
					myAudio.PlayOneShot(tick01,4);
				} else {
					myAudio.PlayOneShot(tick02,4);
				}
				rotationPinVar %= 20;
			}
			arrowGravity -= 1.5f;
			arrowGravity = Mathf.Clamp(arrowGravity,-4,10);
			wheelArrowPart1.transform.eulerAngles += new Vector3(0,0,arrowGravity);
			if (wheelArrowPart1.transform.eulerAngles.z>180) {
				wheelArrowPart1.transform.eulerAngles = new Vector3(wheelArrowPart1.transform.eulerAngles.x,
				                                                    wheelArrowPart1.transform.eulerAngles.y,
				                                                    0);
			}
			wheelArrowPart1.transform.eulerAngles = new Vector3(wheelArrowPart1.transform.eulerAngles.x,
			                                                    wheelArrowPart1.transform.eulerAngles.y,
			                                                    Mathf.Clamp(wheelArrowPart1.transform.eulerAngles.z,0,90));
			wheelPins.transform.eulerAngles += new Vector3(0,0,spinSpeed);
			wheelBack.transform.eulerAngles += new Vector3(0,0,spinSpeed);
			rotationPinVar += -spinSpeed;
			if (Mathf.Abs(spinSpeed)<0.05) {
				float tmpRot = wheelBack.transform.eulerAngles.z%360;
				tmpIcon = KliphjuletIconJackpot2;
				AudioClip tmpClip = clip01;
				if (tmpRot>=0 && tmpRot<60) {
					tmpIcon = KliphjuletIconJackpot2;
					tmpClip = clip01;
					reward = currentJackpot;
				}
				if (tmpRot>=60 && tmpRot<120) {
					tmpIcon = KliphjuletIconOk2;
					tmpClip = clip02;
					reward = currentOk;
				}
				if (tmpRot>=120 && tmpRot<180) {
					tmpIcon = KliphjuletIconFail2;
					tmpClip = clip03;
					reward = 0;
				}
				if (tmpRot>=180 && tmpRot<240) {
					tmpIcon = KliphjuletIconJackpot1;
					tmpClip = clip01;
					reward = currentJackpot;
				}
				if (tmpRot>=240 && tmpRot<300) {
					tmpIcon = KliphjuletIconOk1;
					tmpClip = clip02;
					reward = currentOk;
				}
				if (tmpRot>=300 && tmpRot<360) {
					tmpIcon = KliphjuletIconFail1;
					tmpClip = clip03;
					reward = 0;
				}
				tmpIcon.transform.localScale *= 1.2f;
				myAudio.PlayOneShot(tmpClip,3);
				doneSpinningCounter = 0;
				isSpinning = false;
				state = "doneSpinning";
			}
		}
	}
	void spin() {
		if (!isSpinning) {
			spinCounter = 0;
			spinSpeed = -Random.Range(1,8);
			isSpinning = true;
		}
	}
	public void activateWinScreen() {
		if (myActiveCounter < 0) {
			myActiveCounter = 0;
			transform.position = new Vector3 (0, 0, transform.position.z);
		}
	}
}
