using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager Instance;
    public GameManager Game;

    void Awake()
    {
        if (InitInstance())
        {
            DontDestroyOnLoad(gameObject);
            Game = GetComponentInChildren<GameManager>();
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
