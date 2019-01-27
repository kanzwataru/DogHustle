using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHuman : MonoBehaviour, IMovable {
	enum EAIState {
		Moving,
		Idling,
		Turning
	}

	public float goalThreshold = 0.02f;
	public float turnSpeed = 0.3f;

	NavMeshAgent agent;
	Transform goalsRoot;
	Transform xform;

	Transform currentGoalXform;
	AIGoal    currentGoal;
	EAIState  state = EAIState.Moving;

	float idleTimer = 0.0f;

	public bool isMoving() {
		return state == EAIState.Moving;
	}

	public bool isRunning() {
		return false;
	}

	void Start () {
		goalsRoot = GameObject.Find("AIGoals").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		xform = transform;

		DecideGoals();
		MoveToNextGoal();
        EventBus.AddListener<PauseEvent>(HandleEvent);
    }

    void Update () {
		switch(state) {
		case EAIState.Idling:
			idleTimer -= Time.deltaTime;
			if(idleTimer <= 0.0f) {
				DecideGoals();
				MoveToNextGoal();
			}
		break;

		case EAIState.Moving:
                if (agent.enabled)
                {
                    if (agent.remainingDistance <= goalThreshold)
                    {
                        state = EAIState.Turning;
                    }
                }
		break;

		case EAIState.Turning:
			if(agent.remainingDistance >= goalThreshold)
				state = EAIState.Moving;

			xform.rotation = Quaternion.Lerp(xform.rotation, currentGoalXform.rotation, turnSpeed * Time.deltaTime);
			if(xform.rotation == currentGoalXform.rotation)
				state = EAIState.Idling;
		break;
		}
	}

	void MoveToNextGoal() {
		agent.destination = currentGoalXform.position;
		state = EAIState.Moving;
	}

	void DecideGoals() {
		bool valid = true;
		int times = 0;
		Transform goal;
		do {
			goal = goalsRoot.GetChild((int)Random.Range(0, goalsRoot.childCount - 1));
		
			if(goal == currentGoal)
				valid = false;
			
			/* don't go there if there is already soemone */
			var nearby = Physics.OverlapSphere(goal.position, 2f);
			foreach(var collider in nearby) {
				if(collider.GetComponent<AIHuman>() != null) {
					valid = false;
				}
			}

			if(times++ > goalsRoot.childCount)
				break; /* don't get stuck */
		} while(goal != valid);

		currentGoalXform = goal;
		currentGoal = goal.GetComponent<AIGoal>();

		idleTimer = currentGoal.dwellTime;
	}

    private void HandleEvent(PauseEvent msg)
    {
        agent.enabled = !agent.enabled;
        MoveToNextGoal();
    }
}
