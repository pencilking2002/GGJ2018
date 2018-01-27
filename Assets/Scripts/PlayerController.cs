using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    void PlayerInput_OnRotateAction(int obj)
    {

    }

    public float speed = 10;
    public float rotSpeed = 20;
	public int playerIndex;
	public PickupType currentPickup;
	[HideInInspector]
	public Rigidbody rb;

	[Header("Sword Settings")]
	[Space(5)]
	public float attackLength = 1;
	public GameObject daggerPickupMode;
	public float swordAttackForce = 30;

	public SphereCollider attackSphere;

	//public GameObject jumpPickupMode;

	Camera cam;
	int swordAttackTweenID = -9999;

	bool swordAttacking;

	void Awake()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
        Vector3 vel =  Manager.Instance.PlayerInput.GetPlayerInput(playerIndex) * speed;
        rb.AddForce(vel);   
    }

	void SwordAttack(int index)
	{
//		print("index: " + index);
//		print("player index: " + playerIndex);


		if (index == playerIndex)
		{
			if (currentPickup == PickupType.Dagger && !LeanTween.isTweening(swordAttackTweenID))
			{
				//print ("Attack");

				swordAttackTweenID = LeanTween.moveLocal(daggerPickupMode, new Vector3(0,0, attackLength), 0.1f)
				.setOnComplete(() => {
					swordAttackTweenID = LeanTween.moveLocal(daggerPickupMode, Vector3.zero, 0.1f).id;
				}).id;

				swordAttacking = true;

				attackSphere.gameObject.SetActive(true);

				Manager.Instance.audioManager.Play(AudioType.DaggerSwish);

				LeanTween.delayedCall(0.1f, () => {
					swordAttacking = false;
					attackSphere.gameObject.SetActive(false);
				});
			}
		}
	}

    void RotateCharacter(int index, int direction)
    {
        print("In Rotate with player " + index);
        if(index == playerIndex)
        {
            rb.MoveRotation(Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 5.0f * direction, transform.eulerAngles.z)));
        }
    }


	void OnTriggerEnter(Collider other)
	{
		var pickup = other.GetComponent<Pickup>();

		if (pickup != null)
		{
			//print(pickup.pickupType);
			currentPickup = pickup.pickupType;

			if (currentPickup == PickupType.Dagger)
			{
				daggerPickupMode.SetActive(true);
				Destroy(pickup.gameObject);
			}

		}
	}

	public bool IsSwordAttacking()
	{
		return swordAttacking;
	}

	void OnEnable()
	{
		PlayerInput.onSwordAttack += SwordAttack;
        PlayerInput.onRotateAction += RotateCharacter;
	}

	void OnDisable()
	{
		PlayerInput.onSwordAttack -= SwordAttack;
        PlayerInput.onRotateAction -= RotateCharacter;
	}

}
