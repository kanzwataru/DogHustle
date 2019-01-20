using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour {
	void Start () {
		var poolcomp = gameObject.GetComponent<PoolComponent>();
		if(poolcomp) {
			poolcomp.resetEvent.AddListener(OnReset);
		}	
	}

	void OnReset() {
		Debug.Log("PROJECTILE RECEIVED RESET!");
	}

	void Update() {
		transform.Translate(20f * Time.deltaTime, 0, 0);

		if(transform.position.x > 100)
			Hit();
	}

	void Hit() {
		var poolcomp = gameObject.GetComponent<PoolComponent>();
		if(poolcomp) {
			poolcomp.MarkDestroyed();
		}
		else {
			Destroy(gameObject);
		}
	}
}
