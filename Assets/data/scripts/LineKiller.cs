using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineKiller : MonoBehaviour {
	public Transform waves;
	public Transform ground;
	public Transform bg;

	public Vector3 offset;

	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		foreach (Transform child in ground) {
			if (waves.position.y - 80 > child.position.y) {
				Destroy(child.gameObject, 1);
			}
		}
		foreach (Transform child in bg) {
			if (waves.position.y - 10 > child.position.y) {
				Destroy(child.gameObject, 1);
			}
		}
	}
}