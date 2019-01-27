using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExt {
    public static int not(this int n) {
        return n > 0 ? 0 : 1;
    }
}

public class Motor : MonoBehaviour, IMovable {
    public float maxSpeed = 30f;
    public float accelRate = 10f;
    public float decelRate = 5f;
    public float rotateRate = 0.1f;

    Transform xform;
    Transform rot_root;
    Rigidbody rb;
    Vector2 velocity = Vector2.zero;
    Vector2Int dir = Vector2Int.zero;

    void Start() {
        xform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rot_root = xform.Find("rotation_root");
    }

    float calc_vel(float current, int dir) {
        if(dir != 0)
            return Mathf.Clamp(current + (accelRate * dir), -1.0f, 1.0f);
        else
            return Mathf.Clamp(Mathf.Abs(current) - decelRate, 0.0f, 1.0f) * Mathf.Sign(current);
    }

    void FixedUpdate() {        
        velocity.x = calc_vel(velocity.x, dir.x);
        velocity.y = calc_vel(velocity.y, dir.y);

        var move_delta = new Vector3(
            velocity.y * maxSpeed, 
            0, 
            velocity.x * maxSpeed
        );

        if(dir != Vector2.zero) {
            var face_dir = new Vector3(dir.x, 0, -dir.y);
            var target_rot = Quaternion.LookRotation(face_dir, Vector3.up);

            rot_root.rotation = Quaternion.Lerp(rot_root.rotation, target_rot, rotateRate);
        }

        rb.AddRelativeForce(move_delta);
        if(move_delta == Vector3.zero) {
            rb.velocity = Vector3.zero;
        }
    }

    public void Move(Vector2Int dir)
    {
        this.dir = dir;
    }

    public bool isMoving() {
        return velocity != Vector2.zero;
    }

    public bool isRunning() {
        return false;
    }

}
