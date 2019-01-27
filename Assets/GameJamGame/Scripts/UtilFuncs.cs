public static class UtilFuncs {
	public static float remap(float value, float oldmin, float oldmax, float newmin, float newmax) {
		float real_range = oldmax - oldmin;
		float new_range = newmax - newmin;
		return (((value - oldmin) * new_range) / real_range) + newmin;
	}
}
