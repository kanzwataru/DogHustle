using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldSpaceTest : MonoBehaviour {
	public Transform target;
	public RectTransform sprite;

	void Update () {
		var pos = Camera.main.WorldToScreenPoint(target.position);
		sprite.anchoredPosition = new Vector2(
			pos.x,
			pos.y
		);
	}
}
