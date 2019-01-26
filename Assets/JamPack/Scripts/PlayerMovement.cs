using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Motor motor;

	void Start () {
        motor = this.GetComponentInChildren<Motor>();
	}

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            motor.Move(new Vector2(0, 1));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motor.Move(new Vector2(0, -1));
        }

        if (Input.GetKey(KeyCode.A))
        {
            motor.Move(new Vector2(-1, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            motor.Move(new Vector2(1, 0));
        }

    }

}
