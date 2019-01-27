using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Motor motor;

	void Start () {
        motor = this.GetComponentInChildren<Motor>();
        EventBus.AddListener<GameOverEvent>(HandleEvent);
        EventBus.AddListener<PauseEvent>(HandleEvent);
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
        var dir = Vector2Int.zero;
        motor.Move(dir);
        enabled = false;
    }

    private void HandleEvent(PauseEvent msg)
    {
        var dir = Vector2Int.zero;
        motor.Move(dir);
        enabled = !enabled;
    }

}
