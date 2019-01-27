using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour {
	class Wall {
		public Material[] mats;
		public float percent;
		public bool obstructed;
	}

	public LayerMask wallLayerMask;
	public string wallTag;
	public Material wallMatTemplate;
	public float fadedOpacity = 0.2f;
	public float fadeFade = 0.5f;
	public float fadeTime = 2.0f;

	Dictionary<Transform, Wall> walls = new Dictionary<Transform, Wall>();
	Transform player;
	Transform xform;

	void Start() {
		xform = GetComponent<Transform>();
		player = xform.parent.parent;

		/* yikes, assuming naming conventions */
		foreach(var obj in GameObject.FindObjectsOfType<GameObject>()) {
			if(obj.name.Contains("wall") || obj.name.Contains("Wall")) {
				obj.layer = LayerMask.NameToLayer(wallTag);
				obj.tag = wallTag;
			}
		}

		foreach(var obj in GameObject.FindGameObjectsWithTag(wallTag)) {
			var mesh = obj.GetComponent<MeshRenderer>();
			if(mesh) {
				var mats = mesh.materials;
				for(int i = 0; i < mats.Length; ++i) {
					mats[i] = new Material(wallMatTemplate);
				}

				walls.Add(obj.transform, new Wall() {mats = mats, percent = 0.0f, obstructed = false});

				mesh.materials = mats;
			}
		}
	}

	void Update() {
		var dir = player.position - xform.position;
		float dist = dir.magnitude;

		foreach(var wall in walls.Values) {
			if(wall.obstructed) {
				if(wall.percent < 1.0f)
					wall.percent += fadeTime * Time.deltaTime;
			}
			else {
				if(wall.percent > 0.0f)
					wall.percent -= fadeTime * Time.deltaTime;
			}
		
			foreach(var mat in wall.mats) {
				mat.SetFloat("_Opacity", EasingFunction.EaseInOutSine(1.0f, fadedOpacity,wall.percent));
				mat.SetFloat("_Fade", EasingFunction.EaseInOutSine(0.0f, fadeFade, wall.percent));
			}

			wall.obstructed = false;
		}

		var hits = Physics.SphereCastAll(xform.position, 1f, dir, dist, wallLayerMask);
		foreach(var hit in hits) {
			if(walls.ContainsKey(hit.transform))
				walls[hit.transform].obstructed = true;
		}		
	}
}
