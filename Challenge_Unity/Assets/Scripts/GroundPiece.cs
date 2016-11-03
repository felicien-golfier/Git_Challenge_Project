﻿using UnityEngine;
using System.Collections;

public class GroundPiece : MonoBehaviour{

	[HideInInspector]
	public Player player = null;
	[HideInInspector]
	public Vector3 coord;

	//public Material mainMaterial;

	public Blink blink;


	public bool movable{
		get {return _movable;}
		set{
			if (_movable != value)
			{
				
			}
		}
	}
	private bool _movable = false;

	public bool selected{
		get {
			return _selected;
		}

		set {
			if (_selected != value)
			{
				blink.On = value;
				_selected = value;
			}
		}
	}
	private bool _selected = false;

	void Start()
	{
		if (blink == null)
			blink = GetComponentInChildren<Blink> ();
	}
}