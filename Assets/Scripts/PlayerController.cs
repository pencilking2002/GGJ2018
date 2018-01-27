using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float randForceRange = 0.2f;

	Camera cam;
	Rigidbody rb;

	void Awake()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 vel = new Vector3(
			Manager.Instance.PlayerInput.x + Random.Range(-randForceRange, randForceRange), 
			0, 
			Manager.Instance.PlayerInput.y + Random.Range(-randForceRange, randForceRange)) * speed;

		rb.AddForce(vel);
			
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Pickup>())
		{
			print("found pickup");
		}
	}
}
