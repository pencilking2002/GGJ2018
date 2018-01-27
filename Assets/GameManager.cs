using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

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
    }
}
