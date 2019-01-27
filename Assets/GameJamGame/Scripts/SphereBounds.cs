using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBounds : MonoBehaviour {
	public float radius = 20f;
	
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
