using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
	public GameObject linePrefab;
	public Transform ground;
	public GameObject currentLine;

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCollider;
	public PolygonCollider2D polyCollider;
	public bool skip;

	public List<Vector2> fingerPositions;

	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		if (skip) return;
		
		
		if (Input.GetMouseButtonDown(0)) {
			CreateLine();
		}

		if (Input.GetMouseButton(0)) {
			Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f) {
				UpdateLine(tempFingerPos);
			}
		}
		else if (currentLine) {
			var rb = currentLine.AddComponent<Rigidbody2D>();
			polyCollider.enabled = true;
			Debug.Log(11111);
			currentLine = null;
		}
	}

	void CreateLine() {
		currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
		currentLine.transform.SetParent(ground);
		lineRenderer = currentLine.GetComponent<LineRenderer>();
		//edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
		polyCollider = currentLine.GetComponent<PolygonCollider2D>();
		polyCollider.enabled = false;
		fingerPositions.Clear();
		fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		lineRenderer.SetPosition(0, fingerPositions[0]);
		lineRenderer.SetPosition(1, fingerPositions[1]);
		//edgeCollider.points = fingerPositions.ToArray();
		polyCollider.points = fingerPositions.ToArray();
	}


	void UpdateLine(Vector2 newFingerPos) {
		fingerPositions.Add(newFingerPos);
		lineRenderer.positionCount++;
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
		//edgeCollider.points = fingerPositions.ToArray();
		polyCollider.points = fingerPositions.ToArray();
	}

	public void SkipEnable() {
		Debug.Log(111);
		skip = true;
	}
	public void SkipDisable() {
		Debug.Log(222);
		skip = false;
	}
}