using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

	public float x;
	public float y;

	void Update()
	{
		
        //InputDevice device = InputManager.ActiveDevice;



        //InputControl control = device.GetControl(InputControlType.Action1);
        if (InputManager.Devices.Count != 0)
        {
	        var player1 = InputManager.Devices[0];
	        var player2 = InputManager.Devices[1];


	        //      x = Input.GetAxis("Horizontal");
	        //y = Input.GetAxis("Vertical");
	        x = player1.LeftStick.Value.x;
	        y = player1.LeftStick.Value.y;

	        Debug.Log("Player 1 is a  : " + player1.Name);
	        Debug.Log("Player 2 is a  : " + player2.Name);
        }
        else
        {

			x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");
        }
	}

    public Vector3 GetPlayerInput(int playerIndex)
    {
        Vector3 vel = new Vector3(x, 0, y);
        return vel;
    }
}
