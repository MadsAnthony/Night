using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	int state = 0;
	int gotoLevelCounter = -1;
	float fadeUpAlpha = 1;
	public ClickHitbox clickHitbox;
	public ClickHitbox clickLevel;
	public ClickHitbox clickHitboxBack;
	public ClickHitbox clickHitboxNext;
	public ClickHitbox clickHitboxBack2;
	public ClickHitbox clickHitboxPlay;
	public ClickHitbox clickHitboxOptions;
	public ClickHitbox clickHitboxDelete;
	public ClickHitbox clickHitboxBackOptions;
	public SpriteRenderer fadeUp;
	public GameObject transitionReverse;
	public GameObject startMenu;
	public GameObject characterSelect;
	public GameObject levelSelect;
	public int levelProgress;
	public bool prepareLevel;
	public int currentLevel = 1;
	public int gotoLevel;
	public GameObject pressToPlay;
	Vector3 playButtonParScale;
	Vector3 pressToPlayStartScale;
	Vector3 optionsButtonParScale;
	Vector3 pressToPlayStartPos;
	public TypogenicText playButton;
	public TypogenicText optionsButton;
	public TypogenicText deleteButton;
	public TypogenicText backOptionsButton;
	Vector3 playButtonParPos;
	Vector3 optionsButtonParPos;
	Vector3 backOptionsButtonParScale;
	Vector3 deleteButtonParScale;
	float myAlpha = 0;
	float myAlpha2 = 1;
	int tmpGotoLevel;
	public GameObject[] levelSelectBlockArray;
	public GameObject levelSelectHero;
	public GameObject levelSelectHero2;
	public CharacterSelectLogic characterSelectLogic;
	public SpriteRenderer characterHead;
	public SpriteRenderer characterBreast;
	Animator levelSelectHeroAnim;
	Vector3 characterHeadStartPos;
	Vector3 heroBlockPos;
	Vector3 heroStartScale;
	bool secondFrame = true;
	AudioSource myAudio;
	// Use this for initialization
	void Start () {
		fadeUp.gameObject.transform.position = new Vector3 (0, -0.6f, fadeUp.gameObject.transform.position.z);
		initLevelSelectBlockArray ();
		if (!PlayerPrefs.HasKey("levelProgress")) {
			PlayerPrefs.SetInt("levelProgress",1);
		}
		levelProgress = PlayerPrefs.GetInt("levelProgress");
		if (!PlayerPrefs.HasKey("currentLevel")) {
			PlayerPrefs.SetInt("currentLevel",1);
		}
		currentLevel = PlayerPrefs.GetInt("currentLevel");
		tmpGotoLevel = currentLevel;
		heroStartScale = levelSelectHero.transform.localScale;
		if (PlayerPrefs.HasKey("MenuGotoScreen")) {
			if (PlayerPrefs.GetString ("MenuGotoScreen") == "levelSelect") {
				state = 3;
			}
		}
		pressToPlayStartScale = pressToPlay.transform.localScale;
		pressToPlayStartPos = pressToPlay.transform.localPosition;
		playButtonParScale = playButton.transform.parent.localScale;
		playButtonParPos = playButton.transform.parent.localPosition;
		optionsButtonParPos = optionsButton.transform.parent.localPosition;
		optionsButtonParScale = optionsButton.transform.parent.localScale;
		backOptionsButtonParScale = backOptionsButton.transform.parent.localScale;
		deleteButtonParScale = deleteButton.transform.parent.localScale;
		myAudio = GetComponent<AudioSource> ();
		characterHeadStartPos = characterHead.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (secondFrame) {
			levelSelectHero.transform.position = new Vector3(levelSelectBlockArray[currentLevel].transform.position.x,
			                                                 levelSelectBlockArray[currentLevel].transform.position.y+0.6f,
			                                                 levelSelectHero.transform.position.z);
			secondFrame = false;
		}
		if (characterSelectLogic.selectedBlock.characterName == EnemySpawner.EnemyType.AnderBreinholt) {
			levelSelectHero.GetComponent<Renderer>().enabled = true;
			levelSelectHero2.SetActive(false);
			levelSelectHeroAnim = levelSelectHero.GetComponent<Animator>();
			//character1.SetActive(true);
			//character2.SetActive(false);
		} else {
			if (characterSelectLogic.selectedBlock.characterName == EnemySpawner.EnemyType.Medina) {
				characterBreast.GetComponent<Renderer>().enabled = true;
			} else {
				characterBreast.GetComponent<Renderer>().enabled = false;
			}
			characterHead.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(characterSelectLogic.selectedBlock.characterName)+"Head");
			characterHead.transform.localPosition = characterHeadStartPos+EnemySpawner.getMoveVector(characterSelectLogic.selectedBlock.characterName);
			levelSelectHero.GetComponent<Renderer>().enabled = false;
			levelSelectHero2.SetActive(true);
			levelSelectHeroAnim = levelSelectHero2.GetComponentInChildren<Animator>();
		}
		if (fadeUp != null) {
			fadeUpAlpha -= 0.01f;
			fadeUp.color = new Color (fadeUp.color.r, fadeUp.color.g, fadeUp.color.b, fadeUpAlpha);
			if (fadeUpAlpha <= 0) {
				Destroy (fadeUp.gameObject);
			}
		}
		if (prepareLevel) {
			Vector3 moveVector = levelSelectBlockArray[tmpGotoLevel].transform.position-levelSelectHero.transform.position;
			if (Mathf.Abs(moveVector.x)<0.05f) {
				heroBlockPos = levelSelectHero.transform.position;
				if (gotoLevel==tmpGotoLevel) {
					levelSelectHeroAnim.SetInteger("state",0);
					playLevel();
				}
				if (gotoLevel<tmpGotoLevel) {
					levelSelectHeroAnim.SetInteger("state",1);
					levelSelectHero.transform.localScale = new Vector3(-1*heroStartScale.x,
					                                                   levelSelectHero.transform.localScale.y,
					                                                   levelSelectHero.transform.localScale.z);
					tmpGotoLevel -= 1;
				}
				if (gotoLevel>tmpGotoLevel) {
					levelSelectHeroAnim.SetInteger("state",1);
					levelSelectHero.transform.localScale = heroStartScale;
					tmpGotoLevel += 1;
				}
			}
			moveVector = levelSelectBlockArray[tmpGotoLevel].transform.position-heroBlockPos;
			moveVector = new Vector3 (moveVector.x,moveVector.y+0.6f,0);
			levelSelectHero.transform.position += moveVector*0.05f;
		}
		/*if (clickLevel.isClicked) {
			playLevel();
			clickLevel.isClicked = false;
		}*/
		if (gotoLevelCounter >= 0) {
			if (gotoLevelCounter==1) {
				transitionReverse.GetComponent<Animator>().SetInteger ("state",0);
			}
			gotoLevelCounter++;
			if (gotoLevelCounter==60) {
				PlayerPrefs.SetString("hostName",EnemySpawner.getString(characterSelectLogic.selectedBlock.characterName));
				Application.LoadLevel ("Scene01");
			}
		}
		if (clickHitbox.isClicked) {
			if (!PlayerPrefs.HasKey("firstTimePlayed")) {
				PlayerPrefs.SetInt("firstTimePlayed",1);
				gotoLevel = 1;
				playLevel();
			} else {
				state = 1;
			}
			clickHitbox.isClicked = false;
		}
		if (clickHitboxPlay.isClicked) {
			clickHitboxPlay.transform.parent.localScale = new Vector3 (playButtonParScale.x * 0.8f,
                                               			  playButtonParScale.y * 0.8f,
                                                          playButtonParScale.z);
		} else {
			clickHitboxPlay.transform.parent.localScale = playButtonParScale;
		}
		if (clickHitboxPlay.isClickedUp) {
			state = 2;
			clickHitboxPlay.isClickedUp = false;
			clickHitboxPlay.isClicked = false;
		}
		if (clickHitboxOptions.isClicked) {
			clickHitboxOptions.transform.parent.localScale = new Vector3 (optionsButtonParScale.x * 0.8f,
			                                                           optionsButtonParScale.y * 0.8f,
			                                                           optionsButtonParScale.z);
		} else {
			clickHitboxOptions.transform.parent.localScale = optionsButtonParScale;
		}
		if (clickHitboxOptions.isClickedUp) {
			state = -1;
			clickHitboxOptions.isClickedUp = false;
			clickHitboxOptions.isClicked = false;
		}
		//

		if (clickHitboxBackOptions.isClicked) {
			clickHitboxBackOptions.transform.parent.localScale = new Vector3 (backOptionsButtonParScale.x * 0.8f,
			                                                                  backOptionsButtonParScale.y * 0.8f,
			                                                                  backOptionsButtonParScale.z);
		} else {
			clickHitboxBackOptions.transform.parent.localScale = optionsButtonParScale;
		}
		if (clickHitboxBackOptions.isClickedUp) {
			state = 1;
			clickHitboxBackOptions.isClickedUp = false;
			clickHitboxBackOptions.isClicked = false;
		}

		//

		if (clickHitboxDelete.isClicked) {
			clickHitboxDelete.transform.parent.localScale = new Vector3 (deleteButtonParScale.x * 0.8f,
			                                                             deleteButtonParScale.y * 0.8f,
			                                                             deleteButtonParScale.z);
		} else {
			clickHitboxDelete.transform.parent.localScale = optionsButtonParScale;
		}
		if (clickHitboxDelete.isClickedUp) {
			PlayerPrefs.DeleteAll();
			Application.LoadLevel("Menu");
			clickHitboxDelete.isClickedUp = false;
			clickHitboxDelete.isClicked = false;
		}

		//
		if (clickHitboxBack.isClicked) {
			state = 0;
			clickHitboxBack.isClicked = false;
		}
		if (clickHitboxNext.isClicked) {
			state = 3;
			clickHitboxNext.isClicked = false;
		}
		if (clickHitboxBack2.isClicked) {
			state = 2;
			clickHitboxBack2.isClicked = false;
		}
		if (state == 1) {
			pressToPlay.GetComponent<Animator> ().enabled = false;
			if (pressToPlay.transform.localScale.y > 0) {
				pressToPlay.transform.localScale += new Vector3 (0.0025f, -0.005f, 0);
			} else {
				pressToPlay.transform.position = new Vector3 (1000, 0, 0);
			}
			myAlpha += 0.03f;
			if (myAlpha>=1) {
				clickHitboxPlay.gameObject.SetActive (true);
				clickHitboxOptions.gameObject.SetActive (true);
			}
			myAlpha = Mathf.Clamp01 (myAlpha);
			playButton.ColorTopLeft = new Color (playButton.ColorTopLeft.r, playButton.ColorTopLeft.g, playButton.ColorTopLeft.b, myAlpha);
			playButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, myAlpha));
			optionsButton.ColorTopLeft = new Color (optionsButton.ColorTopLeft.r, optionsButton.ColorTopLeft.g, optionsButton.ColorTopLeft.b, myAlpha);
			optionsButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, myAlpha));
			playButton.transform.parent.localPosition += 0.05f * ((playButtonParPos + new Vector3 (0, 0.3f, 0)) - playButton.transform.parent.localPosition);
			optionsButton.transform.parent.localPosition += 0.05f * ((optionsButtonParPos + new Vector3 (0, -0.3f, 0)) - optionsButton.transform.parent.localPosition);

		} else {
			clickHitboxPlay.gameObject.SetActive(false);
			clickHitboxOptions.gameObject.SetActive(false);
		}
		if (state == -1) {
			optionsButton.transform.parent.localPosition += 0.05f * ((optionsButtonParPos + new Vector3 (0, 0.8f, 0)) - optionsButton.transform.parent.localPosition);
			myAlpha2 -= 0.06f;
			if (myAlpha2<=0) {
				clickHitboxDelete.gameObject.SetActive(true);
				clickHitboxBackOptions.gameObject.SetActive(true);
			}
			myAlpha2 = Mathf.Clamp01 (myAlpha2);
			playButton.ColorTopLeft = new Color (playButton.ColorTopLeft.r, playButton.ColorTopLeft.g, playButton.ColorTopLeft.b, myAlpha2);
			playButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, myAlpha2));
			deleteButton.ColorTopLeft = new Color (deleteButton.ColorTopLeft.r, deleteButton.ColorTopLeft.g, deleteButton.ColorTopLeft.b, 1 - myAlpha2);
			deleteButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 1 - myAlpha2));
			backOptionsButton.ColorTopLeft = new Color (backOptionsButton.ColorTopLeft.r, backOptionsButton.ColorTopLeft.g, backOptionsButton.ColorTopLeft.b, 1 - myAlpha2);
			backOptionsButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 1 - myAlpha2));
		} else {
			myAlpha2 = 1;
			deleteButton.ColorTopLeft = new Color (optionsButton.ColorTopLeft.r, optionsButton.ColorTopLeft.g, optionsButton.ColorTopLeft.b, 0);
			deleteButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 0));
			backOptionsButton.ColorTopLeft = new Color (backOptionsButton.ColorTopLeft.r, backOptionsButton.ColorTopLeft.g, backOptionsButton.ColorTopLeft.b, 0);
			backOptionsButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 0));
			clickHitboxDelete.gameObject.SetActive(false);
			clickHitboxBackOptions.gameObject.SetActive(false);
		}
		if (state == 2) {
			characterSelect.transform.position += (new Vector3 (0, 0, characterSelect.transform.position.z)-characterSelect.transform.position) * 0.1f;
		}
		if (state < 2) {
			characterSelect.transform.position += (new Vector3 (10, 0, characterSelect.transform.position.z)-characterSelect.transform.position) * 0.1f;
		}
		if (state == 3) {
			startMenu.transform.position += (new Vector3 (0, 5, startMenu.transform.position.z)-startMenu.transform.position) * 0.1f;
			levelSelect.transform.position += (new Vector3 (0, 0, levelSelect.transform.position.z)-levelSelect.transform.position) * 0.1f;
			characterSelect.transform.position += (new Vector3 (-10, 0, characterSelect.transform.position.z)-characterSelect.transform.position) * 0.1f;
		}
		if (state != 3) {
			levelSelect.transform.position += (new Vector3 (10, 0, levelSelect.transform.position.z)-levelSelect.transform.position) * 0.1f;
		}
		if (state == 0) {
			clickHitbox.gameObject.SetActive (true);
			pressToPlay.transform.localScale = pressToPlayStartScale;
			pressToPlay.transform.localPosition = pressToPlayStartPos;
			playButton.transform.parent.localPosition = playButtonParPos;
			optionsButton.transform.parent.localPosition = optionsButtonParPos;
			pressToPlay.GetComponent<Animator> ().enabled = true;
			playButton.ColorTopLeft = new Color (playButton.ColorTopLeft.r, playButton.ColorTopLeft.g, playButton.ColorTopLeft.b, 0);
			playButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 0));
			optionsButton.ColorTopLeft = new Color (optionsButton.ColorTopLeft.r, optionsButton.ColorTopLeft.g, optionsButton.ColorTopLeft.b, 0);
			optionsButton.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, 0));
		} else {
			clickHitbox.gameObject.SetActive(false);
		}
		if (state == -1 || state == 0 || state == 1) {
			startMenu.transform.position += (new Vector3 (0, 0, startMenu.transform.position.z) - startMenu.transform.position) * 0.1f;
		} else {
			startMenu.transform.position += (new Vector3 (0, 5, startMenu.transform.position.z)-startMenu.transform.position) * 0.1f;
		}
	}
	public void initLevelSelectBlockArray () {
		if (levelSelectBlockArray.Length == 0) {
			levelSelectBlockArray = new GameObject[10];
		}
	}
	void playLevel() {
		if (gotoLevelCounter<0) {
			myAudio.Stop();
			transitionReverse.GetComponent<Animator> ().SetInteger ("state", 1);
			transitionReverse.transform.position = new Vector3 (1, -0.95f, -2);
			gotoLevelCounter = 0;
			PlayerPrefs.SetInt("currentLevel",gotoLevel);
		}
	}
}
