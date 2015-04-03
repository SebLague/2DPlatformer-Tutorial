using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController2D))]
public class Player : MonoBehaviour {

	float gravity = -22;

	Vector3 velocity;

	CharacterController2D controller;


	void Start() {
		controller = GetComponent<CharacterController2D> ();
	}

	void Update () {
	


		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

	}
}
