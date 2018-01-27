using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    public static Action onSetPlayers;
    public GameObject[] players;

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
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerController>().playerIndex = i;
        }

        if(onSetPlayers != null)
        {
            onSetPlayers(); 
        }

    }
}
