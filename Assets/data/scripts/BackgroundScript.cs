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
	float pHeight;

	void Start() {
		spriteRenderer = bg.GetComponent<SpriteRenderer>();
		bgHeight = spriteRenderer.bounds.size.y;
		pHeight = player.position.y - bgHeight;
		var newBG = Instantiate(bg, new Vector2(3.33f, (0 * bgHeight) + BackgroundOffset), Quaternion.identity);
		newBG.SetParent(transform);
	}

	// Update is called once per frame
	void Update() {
		float test = asTile(player.position.y, bgHeight);

		if (pHeight < test) {
			float newPHeight = test;

			var newBG = Instantiate(bg, new Vector2(3.33f, (test * bgHeight) + BackgroundOffset), Quaternion.identity);
			newBG.SetParent(transform);

			pHeight = newPHeight;
		}
	}

	/*Returns a "tile" number based on a pixel value*/
	float asTile(float n, float tileSize) {
		return (Mathf.Ceil((n / tileSize)));
	}

	/*Returns a pixel number based on a "tile" value*/
	float asPixels(float n, float tileSize) {
		return (Mathf.Ceil(n) * tileSize);
	}
}