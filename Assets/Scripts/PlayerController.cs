using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10;
    private float dashSpeed = 10.0f;
	public int playerIndex;
    public bool lockDash;
    public float dashCoolTimerStart;
    public float dashCoolTimerEnd = 2.0f;
    public GameObject stretchObj;
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
        dashCoolTimerStart = Time.time;
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
        Vector3 vel =  Manager.Instance.PlayerInput.GetPlayerInput(playerIndex) * speed;
        rb.AddForce(vel);

        if (Time.time > dashCoolTimerStart + dashCoolTimerEnd)
        {
            lockDash = false;
        }else{
            lockDash = true;
        }
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

                var scale = stretchObj.transform.localScale;

                LeanTween.scale(stretchObj, new Vector3(scale.x, scale.y * 1.2f, scale.z), 0.1f).setOnComplete(() =>
                {
                    LeanTween.scale(stretchObj, scale, 0.1f);
                });

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

    void DashMove(int index)
    {
        if(index == playerIndex && !lockDash)
        {
            dashCoolTimerStart = Time.time;
            rb.AddForce(new Vector3(rb.velocity.x * dashSpeed, 7.0f, rb.velocity.z * dashSpeed), ForceMode.Impulse);
            lockDash = true;

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
        PlayerInput.onDashMove += DashMove;
        PlayerInput.onRotateAction += RotateCharacter;
	}

	void OnDisable()
	{
		PlayerInput.onSwordAttack -= SwordAttack;
        PlayerInput.onDashMove -= DashMove;
        PlayerInput.onRotateAction -= RotateCharacter;

	}

}
