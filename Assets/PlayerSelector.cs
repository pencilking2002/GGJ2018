using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour {

	public static PlayerSelector Instance;

	public static bool allCharsSelected;

	public HighlightPlayer[] SelectionAvatars;

	void Awake()
	{
		InitInstance();
	}

	// Use this for initialization
	void Start () {

		var numPlayers = Manager.Instance.Game.numberOfPlayers;
		if (numPlayers == 1 || numPlayers == 0)
		{
			Manager.Instance.Game.StartMatch();
		}
		else
		{
			for(int i=0; i < numPlayers; i++)
			{
				SelectionAvatars[i].ActivateSelection(i);
				//List<InputInfo> inputInfo = new List<InputInfo>(Manager.Instance.PlayerInput.inputs);
				Manager.Instance.PlayerInput.inputs[i].currAvatar = SelectionAvatars[i];


			}
		}

		//print (numPlayers);

	}

	public HighlightPlayer GetSelectionAvatar(int _index)
	{
		return SelectionAvatars[_index];
	}

	void Update()
	{
		if (allCharsSelected)
		{
			Manager.Instance.Game.StartMatch();
			allCharsSelected = false;
		}
	}

	void InitInstance()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

}
