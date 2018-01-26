using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour 
{
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() =>
        {
            Manager.Instance.Game.StartGame();
        });
    }

}
