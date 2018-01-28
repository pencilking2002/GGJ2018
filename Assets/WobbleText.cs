using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WobbleText : MonoBehaviour {
	public float randomRotRange = 3;
	public float smoothing;
	Vector3 origEuler;
	Vector3 vel;

	void OnEnable()
	{
//		Wobble(() => { 
//			Wobble(); 
//		});
		origEuler = transform.localEulerAngles;
	}

//	void Wobble(Action callback=null)
//	{
//		var rand1 = UnityEngine.Random.Range(1.0f, randomRotRange);
//		var rand2 = UnityEngine.Random.Range(1.0f, randomRotRange);
//		var rand3 = UnityEngine.Random.Range(1.0f, randomRotRange);
//
//		var newEuler = new Vector3(transform.eulerAngles.x + rand1, transform.eulerAngles.y + rand2, transform.eulerAngles.z + rand3);
//		LeanTween.rotate(gameObject, newEuler, 0.5f).setOnComplete(callback);
//	}

	void Update()
	{
		var rand1 = UnityEngine.Random.Range(-randomRotRange, randomRotRange);
		var rand2 = UnityEngine.Random.Range(-randomRotRange, randomRotRange);
		var rand3 = UnityEngine.Random.Range(-randomRotRange, randomRotRange);
		var newEuler = new Vector3(origEuler.x + rand1, origEuler.y + rand2, origEuler.z + rand3);

		transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles, newEuler, ref vel, smoothing);
	}
}
