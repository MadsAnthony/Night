using UnityEngine;
using System.Collections;

public class JoystickButton : MonoBehaviour {

	SpriteRenderer sprRendButton;
	Color startColor;
	Vector3 startScale;
	bool upJoystick;
	bool attackJoystick;
	public Hero hero;
	public string type;

	// Use this for initialization
	void Start () {
		sprRendButton = GetComponentInChildren<SpriteRenderer> ();
		startColor = sprRendButton.color;
		startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		sprRendButton.color = startColor;
		int i = 0;
		if (type == "left") {
			hero.leftJoystick = false;
		}
		if (type == "right") {
			hero.rightJoystick = false;
		}
		if (type == "down") {
			if (hero.isNearPickup || hero.hasThrowableItem) {
				sprRendButton.color = startColor;
			} else {
				sprRendButton.color = new Color (sprRendButton.color.r,
				                                 sprRendButton.color.g,
				                                 sprRendButton.color.b,
				                                 120f/255f);
			}
			hero.downJoystick = false;
		}
		if (type == "up") {
			upJoystick = false;
		}
		if (type == "attack") {
			attackJoystick = false;
		}
		while (i < Input.touchCount) {
			if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.gameObject == gameObject) {
						if (type == "left") {
							hero.leftJoystick = true;
						}
						if (type == "right") {
							hero.rightJoystick = true;
						}
						if (type == "down" /*&& sprRendButton.color == startColor */) {
							hero.downJoystick = true;
						}
						if (type == "up") {
							upJoystick = true;
							hero.jump();
						}
						if (type == "reset") {
							hero.rJoystick = true;
						}
						if (type == "attack") {
							attackJoystick = true;
							hero.startHit();
						}
						if (type == "noShitSherlock") {
							hero.mainDay.noShitSherlock.playNoShitSherlock();
						}
						if (type == "levelUpScreen") {
							hero.mainDay.levelUpScreen.activateScreen();
						}
						if (type == "pause") {
							GameObject.Find("PauseMenu").transform.position = new Vector3(0,0,GameObject.Find("PauseMenu").transform.position.z);
						}
						if (type == "unpause") {
							GameObject.Find("PauseMenu").transform.position = new Vector3(0,10,GameObject.Find("PauseMenu").transform.position.z);
						}
						if (type == "exit") {
							PlayerPrefs.SetString("MenuGotoScreen","splashScreen");
							Application.LoadLevel("Menu");
						}
					}
				}
			}
			/*if (Input.GetTouch(i).phase == TouchPhase.Ended) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider.gameObject == gameObject) {
						if (type == "left") {
							hero.leftJoystick = false;
							heroRefl.leftJoystick = false;
						}
						if (type == "right") {
							hero.rightJoystick = false;
							heroRefl.rightJoystick = false;
						}
					}
				}
			}*/
			++i;
		}
		if (type == "left") {
			buttonEffect(hero.leftJoystick);
		}
		if (type == "right") {
			buttonEffect(hero.rightJoystick);
		}
		if (type == "down") {
			buttonEffect(hero.downJoystick);
		}
		if (type == "up") {
			buttonEffect(upJoystick);
		}
		if (type == "attack") {
			buttonEffect(attackJoystick);
		}
	}
	void buttonEffect(bool myBool) {
		if (myBool) {
			transform.localScale = startScale * 1.1f;
			sprRendButton.color = new Color (120 / 255f,
			                                 1,
			                                 100f / 255f,
			                                 sprRendButton.color.a);
		} else {
			transform.localScale += (startScale - transform.localScale) * 0.1f;
		}
	}

	void OnMouseDown()
	{
		/*if (type == "left") {
			hero.leftJoystick = true;
			heroRefl.leftJoystick = true;
		}
		if (type == "right") {
			hero.rightJoystick = true;
			heroRefl.rightJoystick = true;
		}
		if (type == "up") {
			hero.jump();
		}*/
		if (type == "reset") {
			hero.rJoystick = true;
		}
		if (type == "pause") {
			GameObject.Find("PauseMenu").transform.position = new Vector3(0,0,GameObject.Find("PauseMenu").transform.position.z);
		}
		if (type == "unpause") {
			GameObject.Find("PauseMenu").transform.position = new Vector3(0,10,GameObject.Find("PauseMenu").transform.position.z);
		}
		if (type == "exit") {
			PlayerPrefs.SetString("MenuGotoScreen","splashScreen");
			Application.LoadLevel("Menu");
		}
	}
	/*
	void OnMouseUp()
	{
		if (type == "left") {
			hero.leftJoystick = false;
			heroRefl.leftJoystick = false;
		}
		if (type == "right") {
			hero.rightJoystick = false;
			heroRefl.rightJoystick = false;
		}
	}
	*/
}
