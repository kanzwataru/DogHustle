using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExt {
    public static int not(this int n) {
        return n > 0 ? 0 : 1;
    }
}

public class Motor : MonoBehaviour {
    public bool canMove = true;
    public float maxSpeed = 30f;
    public float accelRate = 10f;
    public float decelRate = 5f;

    Transform xform;
    Rigidbody rb;
    Vector2 speed = Vector2.zero;
    Vector2Int dir = Vector2Int.zero;

    void Start() {
        xform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    float calc_vel(float current, int dir) {
        if(dir != 0)
            return Mathf.Clamp(current + (accelRate * dir), -1.0f, 1.0f);
        else
            return Mathf.Clamp(Mathf.Abs(current) - decelRate, 0.0f, 1.0f) * Mathf.Sign(current);
    }

    void FixedUpdate() {        
        speed.x = calc_vel(speed.x, dir.x);
        speed.y = calc_vel(speed.y, dir.y);

        rb.AddForce(new Vector3(speed.y * maxSpeed, 
                                      0, 
                                speed.x * maxSpeed));
    }

    public void Move(Vector2Int dir)
    {
        if (canMove)
        {
            this.dir = dir;
        }
    }

}
