using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventBus {
	class UnityEventImpl<T> : UnityEvent<T> {}

	static Dictionary<Type, object> events = new Dictionary<Type, object>();

	private static UnityEventImpl<T> GetEventOfType<T>()
	{
		if(!events.ContainsKey(typeof(T))) {
			events[typeof(T)] = new UnityEventImpl<T>();
		}

		return (UnityEventImpl<T>)events[typeof(T)];
	}

	public static void Emit<T>(T msg)
	{
		var uevent = GetEventOfType<T>();
		uevent.Invoke(msg);
	}

	public static void AddListener<T>(UnityAction<T> listener)
	{
		var uevent = GetEventOfType<T>();
		uevent.AddListener(listener);
	}

	public static void RemoveListener<T>(UnityAction<T> listener)
	{
		var uevent = GetEventOfType<T>();
		uevent.RemoveListener(listener);
	}
}
