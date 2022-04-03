using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public Transform player;
	public Camera cam;
	public float xOffset;
	public float yOffset;
	public float yMin;
	
	float PositionX;
	
	
	// Start is called before the first frame update
	void Start() {
		PositionX = transform.position.x;
		transform.position = new Vector3((PositionX + xOffset), Mathf.Lerp(transform.position.y, Mathf.Max(player.position.y + yOffset, yMin), 1), transform.position.z);

	}

	// Update is called once per frame
	void Update() {
		transform.position = new Vector3((PositionX + xOffset), Mathf.Lerp(transform.position.y, Mathf.Max(player.position.y + yOffset, yMin), Time.deltaTime), transform.position.z);
	}
}