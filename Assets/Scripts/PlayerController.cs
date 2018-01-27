using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
    public int playerIndex;
	public PickupType currentPickup;

	public GameObject daggerPickupMode;
	//public GameObject jumpPickupMode;

	Camera cam;
	Rigidbody rb;


	void Awake()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
        Vector3 vel =  Manager.Instance.PlayerInput.GetPlayerInput(playerIndex) * speed;
        rb.AddForce(vel);	
	}

	void OnTriggerEnter(Collider other)
	{
		var pickup = other.GetComponent<Pickup>();

		if (pickup != null)
		{
			print(pickup.pickupType);
			currentPickup = pickup.pickupType;

			if (currentPickup == PickupType.Dagger)
			{
				daggerPickupMode.SetActive(true);
				Destroy(pickup.gameObject);
			}

		}
	}

}
