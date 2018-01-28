using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using InControl;

public enum GameMode {
	Menu,
	Match,
	GameOver
}

public class GameManager : MonoBehaviour {

	public static Action onStartMatch;
	public static Action onGameOver;
	public static Action onStartMenu;

    public static Action onSetPlayers;
    public GameObject[] players;
    public int numberOfPlayers = 0;
    public GameObject player;
   // public bool gameCanEnd;
    public GameMode mode;

    public void Awake()
    {
        SetPlayers();
		
    }

	
    void Start()
    {
		StartMenu();
    }

//    public void StartGame()
//    {
//		StartMenu();
//        //gameCanEnd = false;
//       // SceneManager.LoadScene("Main-Mike");
//    }

    public void SetPlayers()
    {
        
        foreach(InputDevice device in InputManager.Devices)
        {
            Instantiate(player, new Vector3(5f, 10.0f, -5f), Quaternion.identity);
            numberOfPlayers++;
        }

        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerController>().playerIndex = i;
            if (onSetPlayers != null)
            {
                onSetPlayers();
            }
        }

        //gameCanEnd = true;
    }

    public void Update()
    {
//        if(numberOfPlayers < 0 && gameCanEnd)
//        {
//            SceneManager.LoadScene("MainMenu");
//        }
    }

    public void StartMenu()
    {
    	mode = GameMode.Menu;
		Manager.Instance.audioManager.PlayMusic(AudioType.MainMenu);

    	if (onStartMenu != null)
    		onStartMenu();
    }


    public void StartMatch()
    {
    	if (mode == GameMode.Match)
    		return;

		Manager.Instance.audioManager.PlayMusic(AudioType.LevelMusic);

    	mode = GameMode.Match;
		if (onStartMatch != null)
		{
    		onStartMatch();
    		//print ("there are listeners");

    		print (numberOfPlayers);

    		for (int i=0; i < players.Length; i++)
    		{
	    		// get all the players

				// find the corresponding Manager.Instance.PlayerInput.inputs and get the color
				var playerRend = players[i].transform.Find("Mesh").GetComponent<Renderer>();
				var otherRend = Manager.Instance.PlayerInput.inputs[i].currAvatar.GetComponent<Renderer>();

				playerRend.material.color = otherRend.material.color;

				//Manager.Instance.PlayerInput.inputs[i].currAvatar.GetComponent<Renderer>().sharedMaterial.color;
				// assign to the player
			}
    	}
    	else
    	{
			print ("no listeners");

    	}

    }

    public void GameOver()
    {
    	mode = GameMode.GameOver;
		if (onGameOver != null)
    		onGameOver();
    }

    public bool IsMatch()
    {
    	return mode == GameMode.Match;
    }
	public bool IsGameOver()
    {
		return mode == GameMode.GameOver;

    }
	public bool IsMenu()
    {
		return mode == GameMode.Menu;
    }
}
