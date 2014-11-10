using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private CharacterController controller;
	private float walkSpeed = 50;
	private float runSpeed = 80;
	private float MoveSpeed = 4;
	private float MaxDist = 10;
	private float MinDist = 5;
	private Transform player;
	private float playX;
	private float playZ;
	private NavMeshAgent navCom;

	// Use this for initialization
	void Start () {
		//controller = GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		navCom = this.transform.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		navCom.SetDestination (player.position);
		/*
		if (Vector3.Distance(player.position, transform.position) > MaxDist)
		{
			// Get a direction vector from us to the target
			Vector3 dir = player.position - transform.position;
			
			// Normalize it so that it's a unit direction vector
			dir.Normalize();
			
			// Move ourselves in that direction
			transform.position += dir * MoveSpeed * Time.deltaTime;
		}
*/




		/*transform.LookAt (player);


		if(Vector2.Distance(transform.position,player.position) >= MinDist){
			
			transform.position  += transform.forward*MoveSpeed*Time.deltaTime ;
		

			
			
			if(Vector2.Distance(transform.position,player.position) <= MaxDist)
			{
				//Here Call any function U want Like Shoot at here or something
			} 
			
		}*/


		/*playX = player.position.x;
		playZ = player.position.z;

		Vector3 motion = new Vector3 (0, 0, 0);
		//transform.LookAt(player);
		//transform.position.y = 1;

		motion *= (Mathf.Abs(motion.x) == 1 && Mathf.Abs(motion.z) == 1)?.7f:1;
		//motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		if (transform.position.x < playX) {
			motion.x += 300;
			} 

		else {
			motion.x -= 300;
		}
		 

		if (transform.position.z > playZ) {
			motion.z += 300;
		} 
		
		else {
			motion.z -= 300;
		}

		motion += Vector3.up * -8;

		controller.Move(motion * Time.deltaTime);*/
	}
}
