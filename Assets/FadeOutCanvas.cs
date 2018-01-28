using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCanvas : MonoBehaviour {

	CanvasGroup cg;
	int tweenID;

	void Awake()
	{
		cg = GetComponent<CanvasGroup>();
		BlinkText();
	}

	void BlinkText()
	{
		tweenID = LeanTween.alphaCanvas(cg, 0.0f, 0.4f).setEaseInOutSine().setLoopPingPong(-1).id;
		//UnityEngine.Random.Range(	
	}

	void StartMatch()
	{
		LeanTween.cancel(tweenID);
		LeanTween.alphaCanvas(cg, 0.0f, 0.3f);
	}

	void OnEnable()
	{
		GameManager.onStartMatch += StartMatch;
	}

	void OnDisable()
	{
		GameManager.onStartMatch -= StartMatch;
	}
}
