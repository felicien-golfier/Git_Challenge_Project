using UnityEngine;
using System.Collections;
using UnityEditor;

public class Tools {
	
	public static Transform GetSelectedGO()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, out hit))
			return hit.transform;
		else
			return null;
	}
}
