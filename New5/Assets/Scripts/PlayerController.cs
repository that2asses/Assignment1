using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

public class PlayerController : MonoBehaviour {

	// Handling Variables
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;

	// System Variables
	private Quaternion targetRot;

	// Components
	private CharacterController controller;
	private Camera cam;
	public Gun gun;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		ControlMouse ();
		//ControlWASD ();
		if (Input.GetButtonDown ("Shoot")) {
			gun.Shoot();		
		}
		else if(Input.GetButton ("Shoot")) {
			gun.ShootContinuous();		
		}

	}

	void ControlMouse(){

		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
		targetRot = Quaternion.LookRotation (mousePos - new Vector3(transform.position.x,0,transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRot.eulerAngles.y,rotationSpeed * Time.deltaTime);


		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		
		/*if (input != Vector3.zero) {
			targetRot = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRot.eulerAngles.y,rotationSpeed * Time.deltaTime);
		}*/

		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);
	}

	void ControlWASD(){
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		
		if (input != Vector3.zero) {
			targetRot = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRot.eulerAngles.y,rotationSpeed * Time.deltaTime);
		}
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);
	}

}
