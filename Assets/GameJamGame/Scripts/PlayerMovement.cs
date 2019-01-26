using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Motor motor;

	void Start () {
        motor = this.GetComponentInChildren<Motor>();
        EventBus.AddListener<GameOverEvent>(HandleEvent);
	}

    private void FixedUpdate()
    {
        var dir = Vector2Int.zero;

        if (Input.GetKey(KeyCode.W))
            dir.y = 1;
        else if (Input.GetKey(KeyCode.S))
            dir.y = -1;

        if (Input.GetKey(KeyCode.A))
            dir.x = 1;
        else if (Input.GetKey(KeyCode.D))
            dir.x = -1;

        motor.Move(dir);
    }

    private void HandleEvent(GameOverEvent msg) {
        enabled = false;
    }
}
