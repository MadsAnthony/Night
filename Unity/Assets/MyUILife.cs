using UnityEngine;
using System.Collections;

public class MyUILife : MonoBehaviour {

	float myAlpha = 0;
	bool show = false;
	Vector3 lifeStartScale;
	Enemy enemy;
	HelicopterShelter boss;
	Vector3 textStartPos;

	public float counter = 0;

	public TypogenicText Text;
	public SpriteRenderer Head;
	public SpriteRenderer Name;
	public TypogenicText Lvl;
	public SpriteRenderer LvlBack;
	public SpriteRenderer LifeInner;
	public SpriteRenderer LifeOuter;

	// Use this for initialization
	void Start () {
		lifeStartScale = LifeInner.transform.parent.localScale;
		textStartPos = Text.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy != null) {
			LifeInner.transform.parent.localScale = new Vector3 (lifeStartScale.x * enemy.life / enemy.maxLife, lifeStartScale.y, lifeStartScale.z);
			if (LifeInner.transform.parent.localScale.x < 0) {
					LifeInner.transform.parent.localScale = new Vector3 (0,
                                              LifeInner.transform.localScale.y,
                                              LifeInner.transform.localScale.z);
			}
		}
		if (boss != null) {
			LifeInner.transform.parent.localScale = new Vector3 (lifeStartScale.x * boss.life / boss.maxLife, lifeStartScale.y, lifeStartScale.z);
			if (LifeInner.transform.parent.localScale.x < 0) {
				LifeInner.transform.parent.localScale = new Vector3 (0,
				                                                     LifeInner.transform.localScale.y,
				                                                     LifeInner.transform.localScale.z);
			}
		}
		if (show) {
			myAlpha = 1;
		} else {
			myAlpha -= 0.05f;
		}
		myAlpha = Mathf.Clamp (myAlpha, 0, 1);
		if (myAlpha==1) {
			counter ++;
			if (counter>100) {
				counter = 0;
				show = false;
			}
		}
		Head.color = new Color (Head.color.r, Head.color.g, Head.color.b, myAlpha);
		Name.color = new Color (Name.color.r, Name.color.g, Name.color.b, myAlpha);
		LvlBack.color = new Color (LvlBack.color.r, LvlBack.color.g, LvlBack.color.b, myAlpha);
		LifeInner.color = new Color (LifeInner.color.r, LifeInner.color.g, LifeInner.color.b, myAlpha);
		LifeOuter.color = new Color (LifeOuter.color.r, LifeOuter.color.g, LifeOuter.color.b, myAlpha);
		Text.ColorTopLeft = new Color(Text.ColorTopLeft.r,Text.ColorTopLeft.g,Text.ColorTopLeft.b,myAlpha);
		Text.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,myAlpha));
		Lvl.ColorTopLeft = new Color(Lvl.ColorTopLeft.r,Lvl.ColorTopLeft.g,Lvl.ColorTopLeft.b,myAlpha);
		Lvl.gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,myAlpha));
	}
	public void makeVisible(HelicopterShelter newEnemy) {
		counter = 0;
		show = true;
		boss = newEnemy;
		Text.transform.position = textStartPos;
		//Lvl.Text = ""+newEnemy.level;
		Head.sprite = Resources.Load<Sprite> ("Heads/JanniPHead");
		Text.Text = "Janni Pedersen";
	}
	public void makeVisible(Enemy newEnemy) {
		counter = 0;
		show = true;
		enemy = newEnemy;
		Text.transform.position = textStartPos;
		Lvl.Text = ""+newEnemy.level;
		if (newEnemy.coHostName == "Liam") {
			Head.sprite = Resources.Load<Sprite> ("Heads/LiamHead");
			Text.Text = "Liam O'Connor";
		}
		if (newEnemy.coHostName == "AndersB") {
			Head.sprite = Resources.Load<Sprite> ("Heads/AndersBHead");
			Text.Text = "Anders Breinholt";
		}
		if (newEnemy.coHostName == "LarsHjortshoej") {
			Head.sprite = Resources.Load<Sprite> ("Heads/LarsHjortshoejHead");
			Text.Text = "Lars Hjortshøj";
		}
		if (newEnemy.coHostName == "AndersLMadsen") {
			Head.sprite = Resources.Load<Sprite> ("Heads/AndersLMadsenHead");
			Text.transform.position = textStartPos+new Vector3(0,0.14f,0);
			Text.Text = "Anders Lund \n Madsen";
		}
		if (newEnemy.coHostName == "SoerenMalling") {
			Head.sprite = Resources.Load<Sprite> ("Heads/SoerenMallingHead");
			Text.Text = "Søren Malling";
		}
		if (newEnemy.coHostName == "Medina") {
			Head.sprite = Resources.Load<Sprite> ("Heads/MedinaHead");
			Text.Text = "Medina";
		}
		if (newEnemy.coHostName == "MartinBrygmann") {
			Head.sprite = Resources.Load<Sprite> ("Heads/MartinBrygmannHead");
			Text.Text = "Martin Brygmann";
		}
		if (newEnemy.coHostName == "HuxiBach") {
			Head.sprite = Resources.Load<Sprite> ("Heads/HuxiBachHead");
			Text.Text = "Huxi Bach";
		}
		if (newEnemy.coHostName == "LasseSjoerslev") {
			Head.sprite = Resources.Load<Sprite> ("Heads/LasseSjoerslevHead");
			Text.Text = "Lasse Sjørslev";
		}
	}
}
