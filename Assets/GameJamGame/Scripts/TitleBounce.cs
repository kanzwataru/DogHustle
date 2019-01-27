using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBounce : MonoBehaviour {

    private Vector2 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }

    void Update () {

        this.gameObject.transform.position = startPos + new Vector2(0, Mathf.Sin(Time.time * 2f) * 12f);

	}
}
