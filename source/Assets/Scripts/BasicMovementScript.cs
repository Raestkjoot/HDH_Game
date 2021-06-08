using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
//Free look.
//Two different cameras, one that clips planes just in front of the player and only sees objects on a clippable objects layer.
//Two different cameras, second that doesn't let planes clip and only sees objects that souldn't be clipped.
//Make it so the player can't look through the terrain just cause he's found a hill. Maybe a raycast that doesn't let the camera get too close to the terrain?

public class BasicMovementScript : MonoBehaviour {
	
	public float jumpPower = 10;
	public float gravity = 0.0982f;
	private float moveSpeed = 5.0f;
	public float normalSpeed = 5.0f;
	public float sprintSpeed = 8.0f;
	public float zoom, zoomMin, zoomMax, zoomSpeed;
	public float characterRotSpeed;

	//The main parrent with the charactercontroller attached.
	public Transform character;
	public Transform cam;
	public Animator animator;
	//Parent of the visual mesh of the character.
	public Transform aesthetics;
	public Quaternion aesthRot;
	//The visual mesh of the character.
	public Transform charMesh;
	public Transform charMeshStartRot;

	private float mouseX, mouseY;
	private float moveFB, moveLR;
	private float verticalSpeed;

	public bool swordDrawn = false;

	// Use this for initialization
	void Start () {
		//Hide cursor.
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		moveSpeed = normalSpeed;
		aesthRot = aesthetics.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		//Change zoom with the mouse wheel.
		zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		if (zoom > zoomMin)
			zoom = zoomMin;
		if (zoom < zoomMax)
			zoom = zoomMax;
		cam.transform.localPosition = new Vector3 (0, 0, zoom);


		if (swordDrawn == true) {
			animator.SetBool ("Combat", true);
		}
		else {
			animator.SetBool ("Combat", false);
		}

		RaycastHit hit;
		if (Physics.Raycast (cam.position, Vector3.up, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.name == "Terrain") {
				mouseY += (0.4f + hit.distance);
			}
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			if (swordDrawn == false) {
				moveSpeed = sprintSpeed;
				animator.SetBool ("Sprinting", true);
			}
		}
		else {
			moveSpeed = normalSpeed;
			animator.SetBool ("Sprinting", false);
		}

		//this is for forth/back movement and strafing left/right
		if (Input.GetKey (KeyCode.W)) {
			animator.SetInteger ("VerticalMove", 1);
		} 
		else if (Input.GetKey (KeyCode.S)) {
			animator.SetInteger ("VerticalMove", -1);
		}
		else {
			animator.SetInteger ("VerticalMove", 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			animator.SetInteger ("HorizontalMove", 1);
		}
		else if (Input.GetKey (KeyCode.D)) {
			animator.SetInteger ("HorizontalMove", -1);
		}
		else {
			animator.SetInteger ("HorizontalMove", 0);
		}

		/*if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
			//Rotate leftwards
			print ("rotLeft");
			Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y - 30), charMeshStartRot.localRotation.z);
			charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
		}
		else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
			//Rotate rightwards
			print ("rotRight");
			Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y + 30), charMeshStartRot.localRotation.z);
			charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
		}*/

		if (swordDrawn == false) {
			if (Input.GetKey (KeyCode.A)) {
				//Rotate leftwards
				print ("rotLeft");
				if (Input.GetKey (KeyCode.W)) {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y - 30), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				} else if (Input.GetKey (KeyCode.S)) {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y + 30), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				} else {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y - 50), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				}
			} else if (Input.GetKey (KeyCode.D)) {
				//Rotate rightwards
				print ("rotRight");
				if (Input.GetKey (KeyCode.W)) {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y + 30), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				} else if (Input.GetKey (KeyCode.S)) {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y - 30), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				} else {
					Quaternion targetRot = Quaternion.Euler (charMeshStartRot.localRotation.x, (charMeshStartRot.localRotation.y + 50), charMeshStartRot.localRotation.z);
					charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, targetRot, Time.deltaTime * characterRotSpeed);
				}
			} else if (charMesh.localRotation != charMeshStartRot.localRotation) {
				print ("return");
				charMesh.localRotation = Quaternion.Slerp (charMesh.localRotation, charMeshStartRot.localRotation, Time.deltaTime * characterRotSpeed);
			}
		}

		moveFB = Input.GetAxis ("Vertical") * moveSpeed;
		moveLR = Input.GetAxis ("Horizontal") * moveSpeed;

		Vector3 movement = new Vector3 (moveLR, 0, moveFB);
		movement = character.rotation * movement;


		//TODO: Free look.
		//Rotate character around y and rotate camera view around x with the mouse.
		mouseX += Input.GetAxis ("Mouse X");
		mouseY -= Input.GetAxis ("Mouse Y");
		mouseY = Mathf.Clamp (mouseY, -60f, 60f);

		character.localRotation = Quaternion.Euler (0, mouseX, 0);
		float rotDist = Mathf.Abs(Mathf.Abs( aesthRot.y) - Mathf.Abs(transform.rotation.y));
		aesthRot = Quaternion.Slerp (aesthRot, transform.rotation, Time.deltaTime * characterRotSpeed);
		if (rotDist > 0.5) {
			aesthRot = Quaternion.Slerp (aesthRot, transform.rotation, Time.deltaTime * characterRotSpeed * 2);
		}
		aesthRot = Quaternion.Euler(0f, aesthRot.eulerAngles.y, 0f);
		aesthetics.rotation = aesthRot;
		transform.localRotation = Quaternion.Euler (mouseY, 0, 0);
		CharacterController controller = character.GetComponent<CharacterController>();
		if (controller.isGrounded) {
			animator.SetBool ("Jump", false);
			if (Input.GetKeyDown (KeyCode.Space)) {
				verticalSpeed = jumpPower;
				animator.SetBool ("Jump", true);
			}
		}
		else {
			verticalSpeed -= gravity * Time.deltaTime;
		}
		movement.y += verticalSpeed * Time.deltaTime;
		controller.Move (movement * Time.deltaTime);

		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
