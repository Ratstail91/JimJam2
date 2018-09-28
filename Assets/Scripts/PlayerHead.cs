using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {
	//cached children
	public GameObject firstBodySegment;

	//input
	Vector3 inputVector;

	//private members
	float speed = 2f;
	int length = 10;

	void Start() {
		firstBodySegment.GetComponent<PlayerBodySegment>().parent = this.gameObject;
		firstBodySegment.GetComponent<PlayerBodySegment>().Duplicate(length, speed, this.gameObject);
	}

	void Update() {
		HandleInput();
		HandleMovement();
	}

	void HandleInput() {
		inputVector.x = Input.GetAxis("Horizontal");
		inputVector.y = Input.GetAxis("Vertical");
	}

	void HandleMovement() {
		transform.position += inputVector.normalized * speed * Time.deltaTime;
	}
}
