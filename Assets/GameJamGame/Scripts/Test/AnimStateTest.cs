using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateTest : MonoBehaviour {
	AnimState state;

	float time = 5f;
	float timer = 0f;
	bool flipflop = false;

	// Use this for initialization
	void Start () {
		state = GetComponent<AnimState>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= time) {
			timer = 0;
			flipflop = !flipflop;

			if(flipflop)
				state.SwitchAnim("b");
			else
				state.SwitchAnim("a");
		}

		if(Input.GetKeyDown(KeyCode.Space))
			state.PlayOnce("oneshot");
	}
}
