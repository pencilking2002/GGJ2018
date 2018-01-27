using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;

public class PlayerInput : MonoBehaviour {

	public float x, y, x2, y2;
    List<InputInfo> inputs = new List<InputInfo>();
    public static Action<int> onSwordAttack;
    int playerInd;


	void Update()
	{
        if (InputManager.Devices.Count != 0)
        {
            print("Devices " + InputManager.Devices.Count);
            for (int i = 0; i < InputManager.Devices.Count; i++)
            {
                inputs[i].device = InputManager.Devices[i];
                inputs[i].index = i;

                inputs[i].x = inputs[i].device.LeftStick.Value.x;
                inputs[i].y = inputs[i].device.LeftStick.Value.y;


                if (inputs[i].device.Action2.WasPressed)
                {
                    if (onSwordAttack != null)
                    {
                        onSwordAttack(i);
                    }
                }

            }
            //var player1 = InputManager.Devices[0];
	        //var player2 = InputManager.Devices[1];


           /* switch ()
            {
                case 0:
                    x = inputs[0].device.LeftStick.Value.x;
                    y = inputs[0].device.LeftStick.Value.y;


                    if (inputs[0].device.Action2.WasPressed)
                    {
                        if (onSwordAttack != null)
                        {
                            onSwordAttack(0);
                        }
                    }
                    break;
                case 1:
                    x2 = inputs[1].device.LeftStick.Value.x;
                    y2 = inputs[1].device.LeftStick.Value.y;
                    if (inputs[1].device.Action2.WasPressed)
                    {
                        if (onSwordAttack != null)
                        {
                            onSwordAttack(1);
                        }
                    }
                    break;

            }
            */
        }
        else
        {

			x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");
        }
	}

    public Vector3 GetPlayerInput(int playerIndex)
    {
        //print(playerIndex);

        //playerInd = playerIndex;
        //switch (playerInd)
        //{
        //case 0:
        //    Vector3 vel = new Vector3(x, 0, y);
        //    return vel;
        //case 1:
        //Vector3 vel2 = new Vector3(x2, 0, y2);
        //return vel2;
        //}
        //return new Vector3(0,0,0);
        return new Vector3(inputs[playerIndex].x, 0, inputs[playerIndex].y);
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
        print("Assigned PLayers");
    }

}
