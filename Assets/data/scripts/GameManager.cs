using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// Start is called before the first frame update
	public Transform Water;
	public float WaterSpeed = 0.1f;
	public bool PlayerDead = false;
	public PlayerScript player;
	public AudioSource audio;
	public SpriteRenderer muteButton;
	public Sprite muteIcon;
	public Sprite umuteIcon;
	Vector2 WaterStartPosition;
	private int mute;
	void Start() {
		mute = PlayerPrefs.GetInt("mute");
		WaterStartPosition = Water.position;
	}

	// Update is called once per frame
	void Update() {
		PlayerDead = player.dead;

		//Gameplay continues as normal
		if (!PlayerDead) {
			WaterSpeed = Mathf.Max(1f, player.transform.position.y / 50);
			if (player.LeftGround) {
				Water.position = new Vector2(Water.position.x, Water.position.y + (WaterSpeed * Time.deltaTime));
			}
		}

		audio.mute = mute == 1;

		if (audio.mute) {
			muteButton.sprite = umuteIcon;
		}
		else {
			muteButton.sprite = muteIcon;
		}
	}

	public void toggleMute() {
		mute = mute == 1 ? 0 : 1;
		PlayerPrefs.SetInt("mute", mute);
		PlayerPrefs.Save();
	}
}