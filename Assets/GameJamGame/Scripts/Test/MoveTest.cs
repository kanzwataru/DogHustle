using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTest : MonoBehaviour {
	public Transform goal;

	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent>().destination = goal.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
