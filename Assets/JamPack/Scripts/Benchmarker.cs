using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benchmarker : SingletonObject<Benchmarker> {
	class Bench {
		public int times = 0;
		public double ms = 0;
	}
	
	Dictionary<string, Bench> benchGroups = new Dictionary<string, Bench>();

	public void BenchCall(string name, Action action) {
		var stopwatch = System.Diagnostics.Stopwatch.StartNew();
		action();
		stopwatch.Stop();

		if(!benchGroups.ContainsKey(name)) {
			benchGroups.Add(name, new Bench());
		}

		benchGroups[name].ms += stopwatch.Elapsed.TotalMilliseconds;
		benchGroups[name].times += 1;
	}

	void LateUpdate() {
		Debug.Log("New Frame");
		foreach(var group in benchGroups) {
			Debug.Log(string.Format("{0} took {1} ms in total (called {2} times)", group.Key, group.Value.ms, group.Value.times));

			group.Value.times = 0;
			group.Value.ms = 0;
		}
	}
}
