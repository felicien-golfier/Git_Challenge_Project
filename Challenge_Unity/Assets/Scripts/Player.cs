using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

	public int moveRange = 1;
	public PlayerManager playerManager;
	public GroundPiece piece = null;


	public Player(PlayerManager p, GroundPiece piece)
	{
        this.piece = piece;

        playerManager = GameObject.Instantiate (p);

        playerManager.transform.position = piece.transform.position;
        playerManager.player = this;
        piece.player = this;
		//p.gameObject.SetActive (false);
	}

	public void Move(GroundPiece piece)
	{
		this.piece.selected = false;
		this.piece.player = null;
		piece.player = this;
		this.piece = piece;
		playerManager.transform.position = piece.transform.position;
		GroundManager.instance.UnSetMovablePiecies ();
	}

	public void LaunchAttack(GroundPiece piece)
	{
		ProjectileLife pl = GameObject.Instantiate (playerManager.projectile).GetComponent<ProjectileLife>();
		pl.transform.position = playerManager.transform.position;
		pl.targetPosition = piece.transform.position;
	}
/*	public Player(params object[] args)
	{
		this = new Player ((PlayerManager) args [0]);
		range = args [1];
	}*/
}
