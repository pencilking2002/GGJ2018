using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashTrail : MonoBehaviour {

	public TrailRenderer tr;
	public Renderer playerRenderer;

	void OnEnable()
	{
		tr = GetComponent<TrailRenderer>();
		tr.startColor = playerRenderer.material.color;
		tr.endColor = new Color(playerRenderer.material.color.r, playerRenderer.material.color.g, playerRenderer.material.color.b, 0);

	}
}
