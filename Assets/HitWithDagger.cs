using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWithDagger : MonoBehaviour {

	public PlayerController player;


	void OnTriggerEnter(Collider other)
	{
		//print ("collide");
		var enemy = other.gameObject.GetComponent<PlayerController>();

		// If enemy is not null and the colliding player is not the this one
		if(enemy != null && enemy.playerIndex != player.playerIndex /*&& player.IsSwordAttacking()*/)
		{
			print (player.playerIndex);
			print(enemy.playerIndex);

			enemy.rb.AddForce(player.transform.forward * player.swordAttackForce, ForceMode.Impulse);	

		}
	}
}
