﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHuman : MonoBehaviour, IMovable {
	enum EAIState {
		Moving,
		Idling
	}

	public float goalThreshold = 0.02f;

	NavMeshAgent agent;
	Transform goalsRoot;
	
	Transform currentGoalXform;
	AIGoal    currentGoal;
	EAIState  state = EAIState.Idling;

	float idleTimer = 0.0f;

	public bool isMoving() {
		return state == EAIState.Moving;
	}

	void Start () {
		goalsRoot = GameObject.Find("AIGoals").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();

		DecideGoals();
		MoveToNextGoal();
	}
	
	void FixedUpdate () {
		switch(state) {
		case EAIState.Idling:
			idleTimer -= Time.deltaTime;
			if(idleTimer <= 0.0f) {
				DecideGoals();
				MoveToNextGoal();
			}
		break;

		case EAIState.Moving:
			Debug.Log(agent.remainingDistance);
			if(agent.remainingDistance <= goalThreshold) {
				state = EAIState.Idling;
			}
		break;
		}
	}

	void MoveToNextGoal() {
		agent.destination = currentGoalXform.position;
		state = EAIState.Moving;
	}

	void DecideGoals() {
		Transform goal;
		do {
			goal = goalsRoot.GetChild((int)Random.Range(0, goalsRoot.childCount - 1));
		} while(goal == currentGoalXform);

		currentGoalXform = goal;
		currentGoal = goal.GetComponent<AIGoal>();

		idleTimer = currentGoal.dwellTime;
	}
}
