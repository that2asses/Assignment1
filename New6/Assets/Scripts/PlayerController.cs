using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

	// Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;
	private float acceleration = 5;

	// System
	private Quaternion targetRotation;
	private Vector3 currentVelocityMod;
	private bool reloading;
	private AnimatorTransitionInfo armsTransitionInfo;

	// Components
	public Transform handHold;
	public Gun[] guns;
	private Gun currentGun;
	private CharacterController controller;
	private Animator animator;
	private Camera cam;
	private GameGUI gui;

	void Start () {
		controller = GetComponent<CharacterController>();
		gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GameGUI>();
		animator = GetComponent<Animator>();
		cam = Camera.main;

		EquipGun(0);
	}

	void Update () {
		ControlMouse();
		//ControlWASD();


		armsTransitionInfo = animator.GetAnimatorTransitionInfo(1);

		// Gun Input
		if (currentGun) {
			if (Input.GetButtonDown("Shoot")) {
				currentGun.Shoot();
			}
			else if (Input.GetButton("Shoot")) {
				currentGun.ShootContinuous();
			}

			if (Input.GetButtonDown("Reload")) {
				if (currentGun.Reload()) {
					animator.SetTrigger("Reload");
					reloading = true;
				}
			}

			if (reloading) {
				if (armsTransitionInfo.nameHash == Animator.StringToHash("Arms.Reload -> Arms.Weapon Hold")) {
					currentGun.FinishReload();
					reloading = false;
				}
			}
		}

		for (int i = 0; i < guns.Length; i++) {
			if (Input.GetKeyDown((i+1) + "") || Input.GetKeyDown("[" + (i+1) + "]")) {
				EquipGun(i);
				break;
			}
		}

		if (currentGun.currentAmmoInMag == 0) {
			if (currentGun.Reload()) {
				currentGun.currentAmmoInMag = currentGun.ammoPerMag;
				animator.SetTrigger("Reload");
				reloading = true;
			}

		}
	
	}

	void EquipGun(int i) {
		if (currentGun) {
			Destroy(currentGun.gameObject);
		}

		currentGun = Instantiate(guns[i],handHold.position,handHold.rotation) as Gun;
		currentGun.transform.parent = handHold;
		currentGun.gui = gui;
		animator.SetFloat("Weapon ID",currentGun.gunID);
	}

	void ControlMouse() {

		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x,0,transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);

		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

		currentVelocityMod = Vector3.MoveTowards(currentVelocityMod,input,acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityMod;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);

		animator.SetFloat("Speed",Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));
	}

	void ControlWASD() {
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		
		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
		}

		currentVelocityMod = Vector3.MoveTowards(currentVelocityMod,input,acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityMod;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);

		animator.SetFloat("Speed",Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));
	}
	
}
