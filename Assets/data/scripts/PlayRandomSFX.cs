using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSFX : MonoBehaviour {
	public AudioSource audio;
	public AudioClip[] clips;

	// Start is called before the first frame update
	void OnEnable() {
		audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
	}

	// Update is called once per frame
	void Update() {
	}
}