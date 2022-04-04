using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour {
	public string _name;
	public string _score;

	private TextMeshProUGUI text;
	// Start is called before the first frame update
	void Start() {
		text = gameObject.GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update() {
		text.text = _name + " - " + _score;
	}
}