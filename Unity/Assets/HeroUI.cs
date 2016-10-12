using UnityEngine;
using System.Collections;

public class HeroUI : MonoBehaviour {
	bool show = false;
	float myAlpha = 0;
	Vector3 xpStartScale;
	GameObject xpBar;
	float currentXp = 0;
	EnemySpawner.EnemyType hostName;

	public Hero hero;
	public TypogenicText Text;
	public SpriteRenderer Head;
	public TypogenicText Lvl;
	public SpriteRenderer LvlBack;
	public SpriteRenderer LifeInner;
	public SpriteRenderer LifeOuter;
	public SpriteRenderer XpInner;
	public SpriteRenderer XpOuter;
	public SpriteRenderer CoinIcon;
	public TypogenicText CoinText;
	// Use this for initialization
	void Start () {
		xpStartScale = XpInner.transform.localScale;
		xpBar = XpInner.transform.parent.gameObject;
		currentXp = hero.xp;
		if (!PlayerPrefs.HasKey ("numberOfCoins")) {
			PlayerPrefs.SetInt("numberOfCoins",0);
		}
		if (PlayerPrefs.HasKey ("hostName")) {
			hostName = EnemySpawner.getType (PlayerPrefs.GetString ("hostName"));
		} else {
			hostName = EnemySpawner.EnemyType.AnderBreinholt;
		}
		string tmpName = EnemySpawner.getName(hostName);
		Text.Text = tmpName;
		Head.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(hostName)+"Head");
		if (tmpName.Length>16) {
			Text.transform.localPosition += new Vector3(0,0.15f,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (show) {
			myAlpha += 0.05f;
		} else {
			myAlpha -= 0.05f;
		}
		myAlpha = Mathf.Clamp (myAlpha, 0, 1);
		Head.color = new Color (Head.color.r, Head.color.g, Head.color.b, myAlpha);
		LvlBack.color = new Color (LvlBack.color.r, LvlBack.color.g, LvlBack.color.b, myAlpha);
		LifeInner.color = new Color (LifeInner.color.r, LifeInner.color.g, LifeInner.color.b, myAlpha);
		LifeOuter.color = new Color (LifeOuter.color.r, LifeOuter.color.g, LifeOuter.color.b, myAlpha);
		XpInner.color = new Color (XpInner.color.r, XpInner.color.g, XpInner.color.b, myAlpha);
		XpOuter.color = new Color (XpOuter.color.r, XpOuter.color.g, XpOuter.color.b, myAlpha);
		Text.ColorTopLeft = new Color(Text.ColorTopLeft.r,Text.ColorTopLeft.g,Text.ColorTopLeft.b,myAlpha);
		Text.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,myAlpha));
		Lvl.ColorTopLeft = new Color(Lvl.ColorTopLeft.r,Lvl.ColorTopLeft.g,Lvl.ColorTopLeft.b,myAlpha);
		Lvl.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,myAlpha));
		CoinIcon.color = new Color (CoinIcon.color.r, CoinIcon.color.g, CoinIcon.color.b, myAlpha);
		CoinText.ColorTopLeft = new Color(CoinText.ColorTopLeft.r,CoinText.ColorTopLeft.g,CoinText.ColorTopLeft.b,myAlpha);
		CoinText.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,myAlpha));
		CoinText.Text = ""+PlayerPrefs.GetInt ("numberOfCoins");
		if (hero.xp > 2) {
		}
		if (0.02f*(hero.xp-currentXp) > 0.001f) {
			currentXp += 0.02f*(hero.xp-currentXp);
		} else {
			currentXp = hero.xp;
		}
		xpBar.transform.localScale = new Vector3 (xpStartScale.x * currentXp / hero.maxXp, xpStartScale.y, xpStartScale.z);
		if (xpBar.transform.localScale.x<0) {
			xpBar.transform.localScale = new Vector3 (0,
			                                          xpBar.transform.localScale.y,
			                                          xpBar.transform.localScale.z);
		}
		if (currentXp>=hero.maxXp || xpBar.transform.localScale.x>=1 && hero.level<9) {
			xpBar.transform.localScale = new Vector3 (1,
			                                          xpBar.transform.localScale.y,
			                                          xpBar.transform.localScale.z);
			hero.xp %= hero.maxXp;
			hero.level ++;
			hero.setNewStatsByLevel();
			hero.mainDay.levelUpScreen.activateScreen();
		}
		Lvl.Text = ""+hero.level;
		PlayerPrefs.SetFloat(EnemySpawner.getString (hostName) +"Xp",currentXp);
		PlayerPrefs.SetInt(EnemySpawner.getString (hostName) +"Lvl",hero.level);
	}
	public void showHeroUI() {
		show = true;
	}
}
