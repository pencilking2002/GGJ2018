using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
	Dagger,
	JumpPad,
	None
}

public class Pickup : MonoBehaviour {
	public PickupType pickupType;
}
