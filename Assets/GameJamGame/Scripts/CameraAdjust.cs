using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour {
	public Vector3 moveAmount = new Vector3(0,0,10f);
	public float moveSpeed = 0.2f;

	Transform xform;
	IMovable movable;
	float moveBackPercent = 0.0f;
	Vector3 defaultPos;
	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		xform = GetComponent<Transform>();
		movable = xform.parent.parent.GetComponent<IMovable>();
		defaultPos = xform.localPosition;
		targetPos = defaultPos - moveAmount;
	}
	
	// Update is called once per frame
	void Update () {
		if(movable.isMoving()) {
			if(moveBackPercent < 1.0f)
				moveBackPercent += moveSpeed * Time.deltaTime;
		}
		else {
			if(moveBackPercent > 0.0f)
				moveBackPercent -= moveSpeed * Time.deltaTime;
		}

		xform.localPosition = Vector3.Lerp(defaultPos, targetPos, Mathf.Clamp(moveBackPercent, 0.0f, 1.0f));
	}
}
