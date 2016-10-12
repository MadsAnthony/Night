using UnityEngine;
using System.Collections;

public class MainDay : MonoBehaviour {
	public bool isPaused = false;
	public TypogenicText nCohostText;
	public TypogenicText nCohost;
	public int defeatedEnemies;
	public NoShitSherlock noShitSherlock;
	public NejNejNej nejNejNej;
	public TypogenicText dayText;
	public GameObject winScreen;
	public int totalEnemies = 0;
	public HeroUI HeroUI;
	public GameObject startDayText;
	public LevelUpScreen levelUpScreen;
	public GameObject gameWindow;
	public AudioSource mainAudio;
	public GameObject pencil;

	bool iscoHostDefeatedShown = false;
	float coHostDefeatedAlpha = -1;
	bool coHostDefeatedTransition = true;
	float dayTextAlpha = -1;
	bool isdayTextShown = false;
	int counter = 0;
	
	Vector3 newStartDayPos;
	//float outlineAlpha = 1;
	// Use this for initialization
	void Start () {
		defeatedEnemies = 0;
		dayText.Text = "Day " + PlayerPrefs.GetInt ("currentLevel");
		startDayText.GetComponent<TypogenicText>().Text = "Day " + PlayerPrefs.GetInt ("currentLevel");
		newStartDayPos = new Vector3 (-0.5f, 0.5f, startDayText.transform.position.z);
		ShouldShowPencil ();
	}

	void ShouldShowPencil() {
		int tmpLevelId = PlayerPrefs.GetInt ("currentLevel");
		bool condition = (EnemySpawner.getType(PlayerPrefs.GetString("hostName")) == EnemySpawner.EnemyType.AnderBreinholt);
		pencil.SetActive (tmpLevelId>2&&condition);
	}
	// Update is called once per frame
	void Update () {
		nCohost.Text = defeatedEnemies+"/"+totalEnemies;
		if (counter >= 0) {
			counter ++;
		}
		if (counter >= 30 &&counter < 60*2) {
			startDayText.transform.position +=  0.1f*(newStartDayPos-startDayText.transform.position);
		}
		if (counter >60*2) {
			//newStartDayPos = new Vector3 (6, 0.5f, startDayText.transform.position.z);
			startDayText.transform.position += new Vector3(0.1f,0,0);
			startDayText.transform.position +=  0.1f*(startDayText.transform.position-newStartDayPos);
		}
		if (counter > 60 * (2)) {
			HeroUI.showHeroUI();
		}
		if (counter > 60 * (2.5f)) {
			showDayText();
		}
		if (counter > 60 * (3)) {
			showCoHostDefeated();
			counter = -1;
		}
		/*outlineAlpha -= 0.02f;
		nCohost.gameObject.renderer.material.SetColor ("_OutlineColor", new Color(0,0,0,outlineAlpha));
		nCohost.ColorTopLeft = new Color(1,1,1,outlineAlpha);*/
		if (coHostDefeatedTransition) {
			if (iscoHostDefeatedShown) {
				coHostDefeatedAlpha += 0.05f;
			} else {
				coHostDefeatedAlpha -= 0.05f;
			}
			if (isdayTextShown) {
				dayTextAlpha += 0.05f;
			} else {
				dayTextAlpha -= 0.05f;
			}
			if (coHostDefeatedAlpha < 0 || coHostDefeatedAlpha > 1) {
				coHostDefeatedTransition = false;
			}
			coHostDefeatedAlpha = Mathf.Clamp01 (coHostDefeatedAlpha);
			dayTextAlpha = Mathf.Clamp01 (dayTextAlpha);
			nCohostText.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, coHostDefeatedAlpha));
			nCohostText.ColorTopLeft = new Color (nCohostText.ColorTopLeft.r, nCohostText.ColorTopLeft.g, nCohostText.ColorTopLeft.b, coHostDefeatedAlpha);
			nCohostText.ColorBottomLeft = new Color (nCohostText.ColorBottomLeft.r, nCohostText.ColorBottomLeft.g, nCohostText.ColorBottomLeft.b, coHostDefeatedAlpha);
			nCohost.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, coHostDefeatedAlpha));
			nCohost.ColorTopLeft = new Color (nCohost.ColorTopLeft.r, nCohost.ColorTopLeft.g, nCohost.ColorTopLeft.b, coHostDefeatedAlpha);
			dayText.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color (0, 0, 0, dayTextAlpha));
			dayText.ColorTopLeft = new Color (dayText.ColorTopLeft.r, dayText.ColorTopLeft.g, dayText.ColorTopLeft.b, dayTextAlpha);
		}
	}
	public void activateWinScreen() {
		winScreen.GetComponent<WinScreen>().activateWinScreen ();
	}
	void showCoHostDefeated() {
		iscoHostDefeatedShown = true;
		coHostDefeatedTransition = true;
	}
	void showDayText() {
		isdayTextShown = true;
		coHostDefeatedTransition = true;
	}
}
