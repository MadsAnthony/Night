using UnityEngine;
using System.Collections;

public class AttackDamage : MonoBehaviour {
	float counter = 0;
	int counterDir = 1;
	float alphaCounter = 1;
	TextMesh txM;

	public float damage;
	public TypogenicText typoTx;

	// Use this for initialization
	void Start () {
		counter = Random.Range (0, 4);
		int randN = Random.Range(0,2);
		if (randN == 0)  {
			counterDir = 1;
		} else {
			counterDir = -1;
		}
		txM = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		counter += 0.1f*counterDir;
		alphaCounter -= 0.01f;
		txM.text = "" + damage;
		typoTx.Text = "" + damage;
		txM.color = new Color (txM.color.r, txM.color.g, txM.color.b, alphaCounter);
		transform.position += new Vector3 (0.01f*Mathf.Cos(counter), 0.02f, 0);
		GetComponentInChildren<TypogenicText>().gameObject.GetComponent<Renderer>().material.SetColor ("_OutlineColor", new Color(0,0,0,alphaCounter));
		GetComponentInChildren<TypogenicText>().ColorTopLeft = new Color(1,1,1,alphaCounter);
		if (alphaCounter < 0) {
			Destroy(gameObject);
		}
	}
}
