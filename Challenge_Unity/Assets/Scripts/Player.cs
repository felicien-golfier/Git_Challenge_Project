﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

	public int moveRange = 5;
	public PlayerManager playerMonobehavior;
	public GroundPiece piece = null;

	public Player(PlayerManager p, Vector3 position)
	{
		playerMonobehavior = GameObject.Instantiate (p);
		p.transform.position = Vector3.zero;
		p.player = this;
		p.transform.position = position;
		//p.gameObject.SetActive (false);
	}

	public void Move(GroundPiece piece)
	{
		this.piece.selected = false;
		this.piece.player = null;
		piece.player = this;
		this.piece = piece;
		playerMonobehavior.transform.position = piece.transform.position;
		GroundManager.instance.UnSetMovablePiecies ();
	}
/*	public Player(params object[] args)
	{
		this = new Player ((PlayerManager) args [0]);
		range = args [1];
	}*/
}
