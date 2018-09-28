using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySegment : MonoBehaviour {
	//cached components
	SpriteRenderer spriteRenderer;

	public GameObject parent;
	public float speed = 1.0f;

	GameObject child;
	GameObject head;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		HandleMovement();
		HandleGraphics();
	}

	void HandleMovement() {
		//move faster if further away
		float multiplier;
		if ((parent.transform.position - transform.position).normalized.magnitude > 1f) {
			multiplier = 1.2f;
		} else {
			multiplier = 1f;
		}

		//actually move
		transform.position += (parent.transform.position - transform.position).normalized * speed * Time.deltaTime * multiplier;
	}

	void HandleGraphics() {
		if (child == null) {
			spriteRenderer.color = Color.yellow;
		}
	}

	public void Duplicate(int n, float parentSpeed, GameObject headObject) {
		speed = parentSpeed * 0.9f;
		if (n <= 2) {
			return;
		}

		if (child == null) {
			child = Instantiate(this.gameObject);
		}

		PlayerBodySegment childScript = child.GetComponent<PlayerBodySegment>();

		childScript.parent = this.gameObject;
		childScript.Duplicate(n - 1, speed, headObject);

		head = headObject;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<PlayerHead>() != null) {
			Debug.Log("eating...");
		}
	}
}
