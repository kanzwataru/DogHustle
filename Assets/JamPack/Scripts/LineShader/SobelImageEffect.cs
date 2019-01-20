using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SobelImageEffect : MonoBehaviour {
    public Material mat;

    private RenderTexture inputCamTex;

	// Use this for initialization
	void Start () {
        inputCamTex = new RenderTexture(Screen.width, Screen.height, 32);

        var inputCam = GameObject.Find("SobelInputCam").GetComponent<Camera>();
        inputCam.enabled = true;
        inputCam.targetTexture = inputCamTex;
        mat.SetTexture("_FilterTex", inputCamTex);

        var cam = GetComponent<Camera>();
        cam.depthTextureMode |= DepthTextureMode.Depth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, mat);
    }
}
