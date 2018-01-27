using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
	Dagger,
	JumpPad
}

public class Pickup : MonoBehaviour {
	public PickupType pickupType;
}
