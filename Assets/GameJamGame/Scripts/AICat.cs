using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICat : MonoBehaviour, IMovable, IBarkable {
	enum EAIState {
		walking,
		running
	}

	public float goalThreshold = 0.02f;
	public float turnSpeed = 0.3f;
	public float runSpeed = 12f;
	public float walkSpeed = 4f;

	Transform goalsRoot;
	Transform catGoalsRoot;
	SphereBounds catInterest;

	NavMeshAgent agent;
	Transform xform;

	EAIState state = EAIState.walking;

	public bool isMoving() {
		return true;
	}

	public bool isRunning() {
		return state == EAIState.running;
	}

	public void OnBarked() {
		SwitchRun();
	}

	// Use this for initialization
	void Start () {
		goalsRoot = GameObject.Find("AIGoals").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		catInterest = GameObject.Find("CatInterestBounds").GetComponent<SphereBounds>();

		SwitchWalk();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance <= goalThreshold) {
			NextGoal();
			if(state == EAIState.running)
				SwitchWalk();
		}
	}

 	public Vector3 RandomNavmeshLocation(Vector3 pos, float radius) {
		/* https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html */
		Vector3 randomDirection = Random.insideUnitSphere * radius;
		randomDirection += pos;
		NavMeshHit hit;
		Vector3 finalPosition = Vector3.zero;
		if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
			finalPosition = hit.position;            
		}
		return finalPosition;
    }

	void SwitchWalk() {
		NextGoal();

		agent.speed = walkSpeed;
		state = EAIState.walking;
	}

	void SwitchRun() {
		NextGoal();

		agent.speed = runSpeed;
		state = EAIState.running;
	}

	void NextGoal() {
		Debug.Log("New spot!");
		agent.destination = RandomNavmeshLocation(catInterest.transform.position, catInterest.radius);
	}
}
