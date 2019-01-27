using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPulser : MonoBehaviour {
	public Material pulseMat;
	public float max = 0.6f;
	public float speedMult = 0.5f;
	
	// Update is called once per frame
	void Update () {
		pulseMat.SetFloat("_Fade", UtilFuncs.remap(Mathf.Sin(Time.time * speedMult), -1, 1, 0, max));
	}
}
