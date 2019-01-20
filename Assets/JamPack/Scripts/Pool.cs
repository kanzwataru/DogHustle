using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool {
	private static readonly int DEFAULT_STASH_SIZE = 64;
	private static Dictionary<GameObject, List<PoolComponent>> stash = new Dictionary<GameObject, List<PoolComponent>>();

	private static void DumpPoolObjects() {
		stash = new Dictionary<GameObject, List<PoolComponent>>();
	}

	private static void RegisterPrefab(GameObject prefab) {
		stash.Add(prefab, new List<PoolComponent>(DEFAULT_STASH_SIZE));
		AddMore(prefab);
	}

	private static void AddMore(GameObject prefab) {
		var container = GameObject.Find("__pool_objects__");
		if(!container) {
			container = new GameObject("__pool_objects__");
		}

		for(int i = 0; i < DEFAULT_STASH_SIZE; ++i) {
			var obj = GameObject.Instantiate(prefab);
			obj.transform.parent = container.transform;
			
			var poolobj = obj.AddComponent<PoolComponent>();

			stash[prefab].Add(poolobj);
		}
	}

	public static GameObject RequestItem(GameObject prefab) {
		if(!stash.ContainsKey(prefab)) {
			RegisterPrefab(prefab);
		}

		foreach(var poolobj in stash[prefab]) {
			if(poolobj == null) {
				/* this should not happen but we still don't want to error out */
				Debug.Log("WARNING detected null pool object of prefab: " + prefab.name);
				continue; 
			}

			if(poolobj.destroyed) {
				poolobj.gameObject.SetActive(true);
				poolobj.resetEvent.Invoke();

				return poolobj.gameObject;
			}
		}
		
		/* All the objects in the pool are taken up */
		Debug.Log("Extending pool: " + prefab.name);
		AddMore(prefab);

		return RequestItem(prefab);
	}

	public static GameObject RequestItem(GameObject prefab, Vector3 position) {
		var obj = RequestItem(prefab);

		obj.transform.position = position;

		return obj;
	}

	public static GameObject RequestItem(GameObject prefab, Vector3 position, Quaternion rotation) {
		var obj = RequestItem(prefab);

		obj.transform.position = position;
		obj.transform.rotation = rotation;

		return obj;
	}
}
