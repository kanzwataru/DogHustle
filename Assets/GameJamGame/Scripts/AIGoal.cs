using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoal : MonoBehaviour {
	public float dwellTime = 30f;

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, Vector3.one);
	}
}
