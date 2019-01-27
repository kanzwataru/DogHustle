using UnityEngine;

public static class VectorEase {
	public static Vector3 Ease(System.Func<float, float, float, float> func, Vector3 a, Vector3 b, float t) {
		return new Vector3(func(a.x, b.x, t),
						   func(a.y, b.y, t),
						   func(a.z, b.z, t));
	}
}
