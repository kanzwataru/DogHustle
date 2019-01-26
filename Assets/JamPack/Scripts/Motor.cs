using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour {

    private Rigidbody rigidBody;

    public float speed = 30f;
    public bool canMove = true;

	private void Start () {
        rigidBody = this.GetComponent<Rigidbody>();
	}

    public void Move(Vector2 dir)
    {
        if (canMove)
        {
            rigidBody.AddForce(dir * speed);
        }
    }

}
