using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {
	// Start is called before the first frame update
	public Transform bg;
	public Transform cam;
	public Transform player;
	public int tileSize;
	public float BackgroundOffset = 0;
	float bgHeight;
	SpriteRenderer spriteRenderer;
	private Transform[] bgs;
	private float[] bgY;

	void Start() {
		spriteRenderer = bg.GetComponent<SpriteRenderer>();
		bgHeight = spriteRenderer.bounds.size.y;
		for (var i = 0; i < 10000; i++) {
			var newBG = Instantiate(bg, new Vector2(3.33f, (i * bgHeight)), Quaternion.identity);
			newBG.SetParent(transform);
		}
	}

	// Update is called once per frame
	void Update() {
	}
}