using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public Transform player;
	public GameObject[] objects;
	public GameObject[] tears;
	public Vector2 MinBounds;
	public Vector2 MaxBounds;
	float pHeight;

	// Start is called before the first frame update
	void Start() {
		pHeight = player.position.y + 6;
	}

	// Update is called once per frame
	void Update() {
		if (pHeight < player.position.y + 50) {
			float newPHeight = player.position.y + 100;

			for (float i = pHeight; i < newPHeight; i = i + 2f) {
				float chance = Random.Range(1f, 1000f);
				float tearChance = Random.Range(1f, 1000f);
				if (chance < (400f - (player.position.y * 0.001))) {
					GameObject prefab;
					if (tearChance < (400f - (player.position.y * 0.001))) {
						prefab = tears[Random.Range(0, tears.Length)];
					}
					else {
						prefab = objects[Random.Range(0, objects.Length)];
					}

					GameObject obj = Instantiate(prefab,
						new Vector3(Random.Range(MinBounds.x, MaxBounds.x), MinBounds.y + i, 1),
						prefab.transform.rotation);

					obj.transform.SetParent(transform);
					obj.layer = 6;
				}
			}

			pHeight = newPHeight;
		}
	}
}