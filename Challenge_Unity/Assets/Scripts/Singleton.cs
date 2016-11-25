using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;
	public static T instance{get{return _instance;}}

	void Awake () {
		if (_instance == null)
			_instance = gameObject.GetComponent<T>();
		else
			UnityEngine.Debug.LogError("This Script already exist");
	}
}
