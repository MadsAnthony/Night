using UnityEngine;
using System.Collections;

public class BodyLogic : MonoBehaviour {
	public EnemySpawner.EnemyType hostType;
	public SpriteRenderer headIdle;
	public SpriteRenderer headSpeak;
	public SpriteRenderer headAttack;
	public SpriteRenderer headHurt;
	public SpriteRenderer breast;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("hostName")) {
			hostType = EnemySpawner.getType(PlayerPrefs.GetString("hostName"));
		}
		if (hostType != EnemySpawner.EnemyType.AnderBreinholt) {
			headIdle.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(hostType)+"Head");
			headAttack.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(hostType)+"HeadAttack");
			headHurt.sprite = Resources.Load<Sprite> ("Heads/"+EnemySpawner.getString(hostType)+"HeadHurt");
			headIdle.transform.position += EnemySpawner.getMoveVector(hostType);
			headAttack.transform.position += EnemySpawner.getMoveVector(hostType);
			headHurt.transform.position += EnemySpawner.getMoveVector(hostType);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
