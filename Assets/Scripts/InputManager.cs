using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public float x;
	public float y;

	void Update()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
	}
}
