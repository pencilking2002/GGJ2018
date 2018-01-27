using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager Instance;
    public GameManager Game;
    public PlayerInput PlayerInput;


    void Awake()
    {
        if (InitInstance())
        {
            DontDestroyOnLoad(gameObject);
            Game = GetComponentInChildren<GameManager>();
            PlayerInput = GetComponentInChildren<PlayerInput>();
        }
    }

    bool InitInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }


   

}
