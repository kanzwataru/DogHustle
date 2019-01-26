using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimCoop : MonoBehaviour {
	IMovable movable;
	AnimState state;

	void Start() {
		movable = transform.parent.GetComponent<IMovable>();
		state = GetComponent<AnimState>();
	}

	void Update () {
		if(movable.isMoving()) {
			state.EnsureState("walk");
		}
		else {
			state.EnsureState("idle");
		}
	}
}
