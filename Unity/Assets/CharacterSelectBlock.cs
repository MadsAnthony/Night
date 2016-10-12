using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectBlock : MonoBehaviour {
	public EnemySpawner.EnemyType characterName;
	public SpriteRenderer cursor;
	public Image charIcon;
	public TypogenicText cost;
	public SpriteRenderer coinIcon;
	public CharacterSelectLogic CharSelLogic;
	int price = 50;
	bool isLocked;
	// Use this for initialization
	void Start () {
		if (charIcon != null) {
			//charIcon.color = new Color (70 / 255f, 70 / 255f, 70 / 255f);
			charIcon.sprite = EnemySpawner.getSprite (characterName);
			coinIcon.GetComponent<Renderer>().enabled = false;
			cost.GetComponent<Renderer>().enabled = false;
			charIcon.color = new Color (1, 1, 1);
			if (characterName == EnemySpawner.EnemyType.Medina) {
				charIcon.rectTransform.localScale = new Vector3(0.9f,0.9f,1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		isLocked = false;
		if (characterName != EnemySpawner.EnemyType.AnderBreinholt) {
			if (!PlayerPrefs.HasKey (EnemySpawner.getString (characterName) + "Defeated")) {
				coinIcon.GetComponent<Renderer>().enabled = false;
				cost.GetComponent<Renderer>().enabled = false;
				charIcon.color = new Color (0, 0, 0);
			} else {
				if (PlayerPrefs.HasKey (EnemySpawner.getString (characterName) + "IsBought")) {
					coinIcon.GetComponent<Renderer>().enabled = false;
					cost.GetComponent<Renderer>().enabled = false;
					charIcon.color = new Color (1, 1, 1);
				} else {
					coinIcon.GetComponent<Renderer>().enabled = true;
					cost.GetComponent<Renderer>().enabled = true;
					charIcon.color = new Color (70 / 255f, 70 / 255f, 70 / 255f);
					isLocked = true;
				}
			}
		}
	}
	public void showCursor() {
		cursor.GetComponent<Renderer>().enabled = true;
	}
	public void hideCursor() {
		cursor.GetComponent<Renderer>().enabled = false;
	}
	void OnDrawGizmos()
	{
		if (charIcon != null) {
			charIcon.sprite = EnemySpawner.getSprite (characterName);
		}
	}
	void OnMouseDown()
	{
		if (PlayerPrefs.HasKey (EnemySpawner.getString (characterName) + "Defeated") || characterName == EnemySpawner.EnemyType.AnderBreinholt) {
			if (isLocked /* && got the money*/) {
				if (PlayerPrefs.HasKey ("numberOfCoins")) {
					int tmpNCoins = PlayerPrefs.GetInt("numberOfCoins");
					if (tmpNCoins>=price) {
						CharSelLogic.GetComponent<AudioSource>().PlayOneShot(CharSelLogic.duLignerEnAndenSound,4);
						PlayerPrefs.SetInt (EnemySpawner.getString (characterName) + "IsBought",1);
						PlayerPrefs.SetInt("numberOfCoins",tmpNCoins-price);
					}
				}
			}
			if (PlayerPrefs.HasKey (EnemySpawner.getString (characterName) +"IsBought")) {
				if (PlayerPrefs.GetInt (EnemySpawner.getString (characterName) +"IsBought") == 1) {
					CharSelLogic.selectedBlock = this;
				}
			}
			if (characterName == EnemySpawner.EnemyType.AnderBreinholt) {
				CharSelLogic.selectedBlock = this;
			}
		}
	}
}
