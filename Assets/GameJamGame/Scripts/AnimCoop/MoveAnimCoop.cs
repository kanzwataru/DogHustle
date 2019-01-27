using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimCoop : MonoBehaviour {
	IMovable movable;
	AnimState state;

	public bool happy = false;

	void Start() {
		movable = transform.parent.GetComponent<IMovable>();
		state = GetComponent<AnimState>();

		EventBus.AddListener<HappinessChangedEvent>(HandleEvent);
	}

	void Update () {
		if(movable.isMoving()) {
			if(movable.isRunning())
				state.EnsureState("run");
			else
				state.EnsureState("walk");
		}
		else {
			if(happy)
				state.EnsureState("idle");
			else
				state.EnsureState("sadidle");
		}
	}

	void HandleEvent(HappinessChangedEvent msg) {
		if(msg.person == transform.parent) {
			happy = msg.happy;
		}
	}
}
