using UnityEngine;
using System.Collections;

public class CharacterSelectLogic : MonoBehaviour {
	public CharacterSelectBlock selectedBlock;
	public TypogenicText coinNumber;
	public CharacterSelectBlock[] row1;
	public CharacterSelectBlock[] row2;
	public TypogenicText characterName;
	public TypogenicText levelText;
	public GameObject character1;
	public GameObject character2;
	public SpriteRenderer characterHead;
	public SpriteRenderer characterBreast;
	public AudioClip duLignerEnAndenSound;
	Vector3 characterHeadStartPos;
	Vector3 characterNameStartPos;
	// Use this for initialization
	void Start () {
		int tmpIndex = 0;
		if (PlayerPrefs.HasKey ("hostName")) {
			tmpIndex = EnemySpawner.getSelectNumber(EnemySpawner.getType(PlayerPrefs.GetString("hostName")));
		}
		if (tmpIndex < 3) {
			selectedBlock = row1 [tmpIndex];
		} else {
			selectedBlock = row2 [tmpIndex%4];
		}
		characterHeadStartPos = characterHead.transform.localPosition;
		characterNameStartPos = characterName.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		string tmpName = EnemySpawner.getName(selectedBlock.characterName);
		characterName.Text = ""+tmpName;
		if (tmpName.Length>16) {
			characterName.transform.localPosition = characterNameStartPos+new Vector3(0,0.1f,0);
		} else {
			characterName.transform.localPosition = characterNameStartPos;
		}
		if (!PlayerPrefs.HasKey (EnemySpawner.getString(selectedBlock.characterName)+"Lvl")) {
			PlayerPrefs.SetInt(EnemySpawner.getString(selectedBlock.characterName)+"Lvl",1);
		}
		levelText.Text = ""+PlayerPrefs.GetInt (EnemySpawner.getString (selectedBlock.characterName) + "Lvl");
		if (!PlayerPrefs.HasKey ("numberOfCoins")) {
			PlayerPrefs.SetInt("numberOfCoins",0);
		}
		coinNumber.Text = "" + PlayerPrefs.GetInt ("numberOfCoins");
		if (selectedBlock.characterName == EnemySpawner.EnemyType.AnderBreinholt) {
			character1.SetActive(true);
			character2.SetActive(false);
		} else {
			if (selectedBlock.characterName == EnemySpawner.EnemyType.Medina) {
				characterBreast.GetComponent<Renderer>().enabled = true;
			} else {
				characterBreast.GetComponent<Renderer>().enabled = false;
			}
			characterHead.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(selectedBlock.characterName)+"Head");
			characterHead.transform.localPosition = characterHeadStartPos+EnemySpawner.getMoveVector(selectedBlock.characterName);
			character1.SetActive(false);
			character2.SetActive(true);
		}
		foreach (CharacterSelectBlock charSelBlock in row1) {
			charSelBlock.hideCursor();
		}
		foreach (CharacterSelectBlock charSelBlock in row2) {
			charSelBlock.hideCursor();
		}
		if (selectedBlock != null) {
			selectedBlock.showCursor ();
		}
	}
}
