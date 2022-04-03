using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public GameObject player;
	public Rigidbody2D rb;
	public Transform LeftFoot;
	public Transform RightFoot;
	public Transform CentreStep;
	public bool groundCheck = false;
	public Animator animator;
	public float playerSpeed = 5f;
	public float jumpPower = 5f;
	public float groundCheckDistance = 1f;
	public AudioSource audio;
	public AudioClip JumpSfx;
	public AudioClip WalkSfx;
	public AudioClip LandSfx;
	int GroundedKey = Animator.StringToHash("Grounded");
	int JumpKey = Animator.StringToHash("Jump");
	int WalkKey = Animator.StringToHash("Walk");
	int IdleKey = Animator.StringToHash("Idle");

	// Start is called before the first frame update
	void Start() {
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

		//X axis input changing
		if (X != 0) {
			rb.velocity = new Vector2(X * playerSpeed, rb.velocity.y);
			//Set the player facing direction
			if (X > 0) {
				transform.parent.localScale =
					new Vector3(-1, transform.parent.localScale.y, transform.parent.localScale.z);
			}
			else {
				transform.parent.localScale =
					new Vector3(1, transform.parent.localScale.y, transform.parent.localScale.z);
			}
		}
	}


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
}