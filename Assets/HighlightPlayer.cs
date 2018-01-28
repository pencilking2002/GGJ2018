using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPlayer : MonoBehaviour {
	public GameObject selectionBG;
	public TextMesh text;
	public Renderer rend;

	public int playerIndex;

	void OnEnable()
	{
		selectionBG.SetActive(false);

	}

	public void Deselect()
	{
		selectionBG.SetActive(false);
	}

	public void ActivateSelection(int _index)
	{
		selectionBG.SetActive(true);
		playerIndex = _index;
		text.text = "P" + (playerIndex + 1).ToString();

	}
}
