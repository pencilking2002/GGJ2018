using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;

public class PlayerInput : MonoBehaviour {

	public float x, y, x2, y2;
    List<InputInfo> inputs = new List<InputInfo>();
    public static Action<int> onSwordAttack;
    public static Action<int> onDashMove;
    public static Action<int,int> onRotateAction;
    public GameObject player;


    void Update()
	{
        if (InputManager.Devices.Count != 0)
        {

            //print("Devices " + InputManager.Devices.Count);
            for (int i = 0; i < InputManager.Devices.Count; i++)
            {
                inputs[i].device = InputManager.Devices[i];
                inputs[i].index = i;

                inputs[i].x = inputs[i].device.LeftStick.Value.x;
                inputs[i].y = inputs[i].device.LeftStick.Value.y;

                if(inputs[i].device.RightTrigger.IsPressed || inputs[i].device.LeftTrigger.IsPressed )
                {
                    int direction = inputs[i].device.RightTrigger.IsPressed ? 1 : -1;
                    
                   if(onRotateAction != null)
                    {
                        onRotateAction(i, direction);
                    }
                }

                if (inputs[i].device.Action2.WasPressed)
                {
                    if (onSwordAttack != null)
                    {
                        onSwordAttack(i);
                    }
                }

                else if(inputs[i].device.Action4.WasPressed)
                {
                    if(onDashMove != null)
                    {
                        onDashMove(i);
                    }
                }
            } 
        }
        // This logic is for if there are no controllers hooked up
        // Player can only use the keyboard
        else
        {

			x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");

			if (Input.GetKeyDown(KeyCode.F))
            {
                if (onSwordAttack != null)
                {
                    onSwordAttack(0);
                }
            }
			else if(Input.GetKeyDown(KeyCode.C))
            {
		        if(onDashMove != null)
		        {
		            onDashMove(0);
		        }
            }
        }
	}

    public Vector3 GetPlayerInput(int playerIndex)
    {
    	if (InputManager.Devices.Count != 0)
        	return new Vector3(inputs[playerIndex].x, 0, inputs[playerIndex].y);
        else
			return new Vector3(x, 0, y);
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
