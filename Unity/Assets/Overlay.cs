using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	Hero hero;
	SpriteRenderer heroSprRend;
	SpriteRenderer sprRend;
	float heroDarkValue = 1;
	float alpha = 0;

	float lightR = 1;
	float lightG = 0;
	float lightB = 1;
	float liSpeed = 0.1f;
	bool fadeUp = true;
	AudioSource mySound;
	Vector3 loserPos;
	Vector3 heroStartScale;
	Vector3 startScale;
	float counter = 0;
	public SpriteRenderer crazyLightRend;
	public GameObject loserSign;
	public GameObject loserButtons;
	// Use this for initialization
	void Start () {
		hero = GameObject.Find ("GameWindow/Hero").GetComponent<Hero>();
		sprRend = GetComponent<SpriteRenderer>();
		heroSprRend = hero.GetComponent<SpriteRenderer> ();
		loserPos = loserSign.transform.position-transform.position;
		loserPos += new Vector3 (0, 0, transform.position.z-1);
		loserSign.transform.position += new Vector3 (0, 1000, 0);
		mySound = GetComponent<AudioSource> ();
		crazyLightRend.color = new Color(lightR,lightG,lightB,0);
		startScale = transform.localScale;
		heroStartScale = GameObject.Find ("GameWindow").transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (!fadeUp) {
			counter += 0.115f;
			if (lightR>=1) {
				lightB -= liSpeed;
				if (lightB<=0) {
					lightG += liSpeed;
				}
			}
			if (lightG>=1) {
				lightR -= liSpeed;
				if (lightR<=0) {
					lightB += liSpeed;
				}
			}
			if (lightB>=1) {
				lightG -= liSpeed;
				if (lightG<=0) {
					lightR += liSpeed;
				}
			}
			lightR = Mathf.Clamp01(lightR);
			lightG = Mathf.Clamp01(lightG);
			lightB = Mathf.Clamp01(lightB);
			crazyLightRend.color = new Color(lightR,lightG,lightB,80/255f);
			transform.localScale = startScale+0.3f*startScale*Mathf.Abs (Mathf.Sin(counter));
			transform.localScale = new Vector3 (transform.localScale.x,transform.localScale.y,startScale.z);
			hero.transform.localScale = heroStartScale+0.3f*heroStartScale*Mathf.Abs (Mathf.Sin(counter));
			transform.eulerAngles = new Vector3(0,0,15*Mathf.Sin(counter*0.3f));
		}
		if (hero.life <= 0 && fadeUp) {
			if (alpha == 0) {
				hero.transform.position += new Vector3(0,0,-9);
			}
			alpha += 0.01f;
			heroDarkValue -= 0.002f;
			heroDarkValue = Mathf.Clamp(heroDarkValue,0.85f,1);
			//heroSprRend.color =new Color(heroDarkValue,heroDarkValue,heroDarkValue,1);
			transform.position = new Vector3(0,0,transform.position.z);
			sprRend.color = new Color(sprRend.color.r,sprRend.color.g,sprRend.color.b,alpha);
			if (alpha>1) {
				loserButtons.transform.position = new Vector3 (4.86f,-0.27f,-9.1f);
				mySound.Play();
				loserSign.transform.position = loserPos;
				fadeUp = false;
			}
		}
	}
}
