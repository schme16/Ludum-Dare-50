using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public GameObject player;
	public Rigidbody2D rb;

	public bool groundCheck = false;
	public Animator animator;
	public float playerSpeed = 5f;
	public float jumpPower = 5f;
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
				animator.SetTrigger(WalkKey);
			}
			else {
				animator.ResetTrigger(GroundedKey);
				animator.ResetTrigger(WalkKey);
				animator.SetTrigger(IdleKey);
			}

			//This is fired when the player grounds
			if (_groundCheck && !groundCheck) {
				
				//TODO: Play sfx for landing
				animator.ResetTrigger(WalkKey);
				animator.ResetTrigger(IdleKey);
				animator.SetTrigger(GroundedKey);
			}
		}


		//Was grounded, then jumped
		if (_groundCheck && groundCheck && Jump != 0) {
			//TODO: Play sfx for jump
			
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
		var RaycastGround = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
		if (RaycastGround.collider != null) {
			if (RaycastGround.collider.CompareTag("Ground")) {
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
}