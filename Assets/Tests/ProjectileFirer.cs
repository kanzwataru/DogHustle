using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFirer : MonoBehaviour {
	public GameObject prefabA;
	public GameObject prefabB;

	bool typeFlipflop = false;

	void Update() {
		if(Input.GetKey(KeyCode.Space))
			Fire();
	}

	void Fire() {
		typeFlipflop = !typeFlipflop;
		if(typeFlipflop)
			Pool.RequestItem(prefabA, transform.position);
		else
			Pool.RequestItem(prefabB, transform.position);
	}
}
