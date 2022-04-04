using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public TextMeshProUGUI height;
	public GameObject player;
	public Rigidbody2D rb;
	public Transform LeftFoot;
	public Transform RightFoot;
	public Transform CentreStep;
	public GameObject deathSFX;

	public bool groundCheck = false;
	public Animator animator;
	public float playerSpeed = 5f;
	public float jumpPower = 5f;
	public float groundCheckDistance = 1f;
	public LevelChanger lc;
	float startYPos;
	public AudioSource audio;
	public AudioClip JumpSfx;
	public AudioClip WalkSfx;
	public AudioClip LandSfx;
	int GroundedKey = Animator.StringToHash("Grounded");
	int JumpKey = Animator.StringToHash("Jump");
	int WalkKey = Animator.StringToHash("Walk");
	int IdleKey = Animator.StringToHash("Idle");
	public bool dead = false;
	public bool LeftGround = false;
	bool _dead = false;


	// Start is called before the first frame update
	void Awake() {
		dead = false;
		_dead = false;
		startYPos = transform.position.y;
	}

	// Update is called once per frame
	void FixedUpdate() {
		var _groundCheck = IsGrounded();

		//Shorthand the X/Yaxis
		var X = Input.GetAxis("Horizontal");
		var Y = Input.GetAxis("Vertical");

		//Get Jump input
		var Jump = Input.GetAxis("Jump");


		//Player on the ground
		if (_groundCheck) {
			//Walking
			if (X != 0) {
				animator.ResetTrigger(GroundedKey);
				animator.ResetTrigger(IdleKey);
				animator.ResetTrigger(JumpKey);
				animator.SetTrigger(WalkKey);
				//audio.PlayOneShot(WalkSfx);
			}
			else {
				animator.ResetTrigger(GroundedKey);
				animator.ResetTrigger(WalkKey);
				animator.ResetTrigger(JumpKey);
				animator.SetTrigger(IdleKey);
			}

			//This is fired when the player grounds
			if (_groundCheck && !groundCheck) {
				//audio.PlayOneShot(LandSfx);
				animator.ResetTrigger(WalkKey);
				animator.ResetTrigger(IdleKey);
				animator.ResetTrigger(JumpKey);
				animator.SetTrigger(GroundedKey);
			}
		}


		//Was grounded, then jumped
		if (_groundCheck && groundCheck && Jump != 0) {
			//audio.PlayOneShot(JumpSfx);

			rb.velocity = new Vector2(0, jumpPower);
			animator.SetTrigger(JumpKey);
			animator.ResetTrigger(WalkKey);
			animator.ResetTrigger(GroundedKey);
			animator.ResetTrigger(IdleKey);
		}


		groundCheck = _groundCheck;

		if (!dead) {
			//X axis input changing
			if (X != 0) {
				rb.velocity = new Vector2(X * playerSpeed, rb.velocity.y);
				//Set the player facing direction
				if (X > 0) {
					transform.parent.localScale =
						new Vector3(-0.5f, transform.parent.localScale.y, transform.parent.localScale.z);
				}
				else {
					transform.parent.localScale =
						new Vector3(0.5f, transform.parent.localScale.y, transform.parent.localScale.z);
				}
			}
		}

		if (_dead && !dead) {
			deathSFX.SetActive(true);

			StartCoroutine(getRequest());
		}

		LeftGround = transform.position.y > startYPos + 0.5f;

		dead = _dead;
		if (height) {
			height.text = Mathf.Max(0, Mathf.RoundToInt(transform.position.y - startYPos)).ToString();
		}
	}

	//Figures out if the player is touching the ground
	bool IsGrounded() {
		RaycastHit2D RaycastGroundLeftFoot = Physics2D.Raycast(LeftFoot.position, Vector2.down, groundCheckDistance,
			LayerMask.GetMask("Ground"));

		RaycastHit2D RaycastGroundRightFoot = Physics2D.Raycast(RightFoot.position, Vector2.down, groundCheckDistance,
			LayerMask.GetMask("Ground"));

		RaycastHit2D RaycastGroundCentreStep = Physics2D.Raycast(CentreStep.position, Vector2.down,
			groundCheckDistance,
			LayerMask.GetMask("Ground"));

		RaycastHit2D RaycastGroundBody = Physics2D.Raycast(transform.position, Vector2.down,
			groundCheckDistance,
			LayerMask.GetMask("Ground"));

		if (RaycastGroundLeftFoot.collider != null || RaycastGroundRightFoot.collider != null ||
			RaycastGroundCentreStep.collider != null || RaycastGroundBody.collider != null) {
			return true;
		}
		else {
			return false;
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == 4) {
			_dead = true;
		}
	}

	IEnumerator getRequest() {
		UnityWebRequest uwr = UnityWebRequest.Get("https://ldjam-50-inky-towers.herokuapp.com/post-score?score=" + height.text + "&name=" +
												PlayerPrefs.GetString("player-name"));
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError) {
			lc.LevelToLoad = "restart";
			lc.FadeToLevel();
			Debug.Log("Error While Sending: " + uwr.error);
		}
		else {
			lc.LevelToLoad = "restart";
			lc.FadeToLevel();
			Debug.Log("Received: " + uwr.downloadHandler.text);
		}
	}
}