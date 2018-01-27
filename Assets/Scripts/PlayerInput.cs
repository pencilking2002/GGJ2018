using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

	public float x;
	public float y;
    List<InputInfo> inputs = new List<InputInfo>();

	void Update()
	{
        if (InputManager.Devices.Count != 0)
        {
            var player = InputManager.Devices[0];
	        var player2 = InputManager.Devices[1];
            x = player.LeftStick.Value.x;
	        y = player.LeftStick.Value.y;

	        Debug.Log("Player 1 is a  : " + player.Name);
	        Debug.Log("Player 2 is a  : " + player.Name);
        }
        else
        {

			x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");
        }
	}

    public Vector3 GetPlayerInput(int playerIndex)
    {
        int playerInd = playerIndex;
        Debug.Log("playerInd = " + playerInd);
        Vector3 vel = new Vector3(x, 0, y);
        return vel;
    }

    private void OnEnable()
    {
        GameManager.onSetPlayers += AssignPlayers; 
    }

    private void OnDisable()
    {
        GameManager.onSetPlayers -= AssignPlayers;   
    }

    void AssignPlayers()
    {
        inputs.Add(new InputInfo());
    }
}
