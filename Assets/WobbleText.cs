using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WobbleText : MonoBehaviour {
	
	public float randomRotRange = 3;
	public float smoothing;
	Vector3 origEuler;
	Vector3 vel;
	Vector3 origPos;

	void OnEnable()
	{
		origEuler = transform.localEulerAngles;
		origPos = transform.position;
	}

	public void ResetPos()
	{
		transform.position = origPos;
	}


}
