using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimStateInfo {
	public string name;
	public GameObject prefab;
}

public class AnimState : MonoBehaviour {
	[SerializeField]
	public AnimStateInfo[] animations;
	public string currentAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
