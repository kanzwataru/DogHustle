using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderReplacer : MonoBehaviour {
    public Shader shader;
    public string tag = "Opaque";

	// Use this for initialization
	void Start () {
        var cam = GetComponent<Camera>();
        cam.SetReplacementShader(shader, tag);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
