using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUp : MonoBehaviour {

	 float animInterval = 3.0f;
	 float lastAnimate;
	 Vector3 origLocalPos;

	 void OnEnable()
	 {
	 	origLocalPos = transform.localPosition;
	 }

	 void Update()
	 {
		if (Time.time > lastAnimate + animInterval)
	 	{
	 		LeanTween.moveLocalY(gameObject, transform.localPosition.y + 10, 0.2f)
	 		.setOnComplete(() => {
	 			transform.localPosition = new Vector3(origLocalPos.x, origLocalPos.y - 10, origLocalPos.z);
					LeanTween.moveLocalY(gameObject, origLocalPos.y, 0.2f);

	 		});

	 		lastAnimate = Time.time;
	 	}
	 } 
}
