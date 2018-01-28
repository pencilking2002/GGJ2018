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

    public static Action onSetPlayers;
    public GameObject[] players;
    public int numberOfPlayers = 0;
    public GameObject player;
    public bool gameCanEnd;
    public GameMode mode = GameMode.Menu;

    public void Awake()
    {
        SetPlayers();
    }

    public void StartGame()
    {
        gameCanEnd = false;
        SceneManager.LoadScene("Main-Mike");
    }

    public void SetPlayers()
    {
        
        foreach(InputDevice device in InputManager.Devices)
        {
            Instantiate(player, new Vector3(0f, 10.0f, 0f), Quaternion.identity);
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

        gameCanEnd = true;
    }

    public void Update()
    {
        if(numberOfPlayers < 0 && gameCanEnd)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
