using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10;

	Camera cam;
	Rigidbody rb;

	void Awake()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 vel = new Vector3(Manager.Instance.PlayerInput.x,0,Manager.Instance.PlayerInput.y) * speed;
		//rb.AddTorque(vel);
		rb.AddForce(vel);
			
	}
}
