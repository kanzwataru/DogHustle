using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolComponent : MonoBehaviour {
	public UnityEvent resetEvent = new UnityEvent();
	public bool destroyed { get; private set; }

	public void Awake() {
		destroyed = true; /* start destroyed */
		resetEvent.AddListener(OnReset);
	}

	public void MarkDestroyed() {
		destroyed = true;
		gameObject.SetActive(false);
	}

	void OnReset() {
		destroyed = false;
	}
}
