﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public int playerIndex;
	public PickupType currentPickup;

	[Header("Sword Settings")]
	[Space(5)]
	public float attackLength = 1;
	public GameObject daggerPickupMode;
	//public GameObject jumpPickupMode;

	Camera cam;
	Rigidbody rb;
	int swordAttackTweenID = -9999;


	void Awake()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			SwordAttack();
		}
	}

	void FixedUpdate()
	{
        Vector3 vel =  Manager.Instance.PlayerInput.GetPlayerInput(playerIndex) * speed;
        rb.AddForce(vel);	
	}

	void SwordAttack()
	{
		if (currentPickup == PickupType.Dagger && !LeanTween.isTweening(swordAttackTweenID))
		{
			print ("Attack");
			swordAttackTweenID = LeanTween.moveLocal(daggerPickupMode, new Vector3(0,0, attackLength), 0.1f)
			.setOnComplete(() => {
				swordAttackTweenID = LeanTween.moveLocal(daggerPickupMode, Vector3.zero, 0.1f).id;
			}).id;
		}
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
