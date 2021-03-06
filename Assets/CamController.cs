﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {
	public Vector3 origPos;

	public float numMoveDown = -10;

	void Start()
	{
		origPos = transform.position;
		//print ("start");
	}

	void MoveCamToMatchPos()
	{
		print ("move");
		LeanTween.move(gameObject, transform.position + new Vector3(0,numMoveDown,0), 2.0f).setEaseInOutExpo().setOnComplete(() => {
			PerlinShake.Instance.AssignOrigPos();
		});
	}

	void OnEnable()
	{
		GameManager.onStartMatch += MoveCamToMatchPos;
	}

	void OnDisable()
	{
		GameManager.onStartMatch -= MoveCamToMatchPos;
	}

	public void ResetPos()
	{
		transform.position = origPos;
		PerlinShake.Instance.AssignOrigPos();
	}
}
