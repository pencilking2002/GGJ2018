using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using InControl;

public class GameManager : MonoBehaviour {

    public static Action onSetPlayers;
    public GameObject[] players;
    public GameObject player;

    public void Awake()
    {
        SetPlayers();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetPlayers()
    {
        foreach(InputDevice device in InputManager.Devices)
        {
            Instantiate(player, new Vector3(0f, 10.0f, 0f), Quaternion.identity);
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
    }
}
