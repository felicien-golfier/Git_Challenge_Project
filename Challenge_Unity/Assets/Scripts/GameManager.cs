using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Security.Policy;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager> {

	public float PlayerNumber = 1;
	public PlayerManager PlayerTemplate;
	private List<Player> players = new List<Player> ();
	private GroundPiece selectedPiece;
	private Action action = Action.Creation;

	public enum Action{
		None = 0,
		//Selected = 1,
		Moving = 2,
		Creation = 100
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			Transform tmp = Tools.GetSelectedGO ();
			GroundPiece piece;
			if (tmp != null && (piece = tmp.GetComponent<GroundPiece> ()) != null) 
				ClickOnPiece (piece);
		}
	}

	private void ClickOnPiece(GroundPiece piece){
		
		switch(action){

		case Action.None:
			SelectPiece (piece);
			if (piece.player != null) {
				GroundManager.instance.SetMovablePiecies (piece.player);
				action = Action.Moving;
			}else if (selectedPiece == piece)
			{
				
			}
			break;

		case Action.Moving:
			if (selectedPiece.player != null && piece.movable)
				selectedPiece.player.Move (piece);
			action = Action.None;
			break;

		case Action.Creation:
			if (players.Count < PlayerNumber) {
				piece.player = CreateNewPlayer (piece);
			}
			if (players.Count >= PlayerNumber)
				action = Action.None;
			break;
		default :
			break;
		}
	}
	private void SelectPiece(GroundPiece piece){

		if (selectedPiece != null)
			selectedPiece.selected = false;
		if (selectedPiece != piece) {
			selectedPiece = piece;
			piece.selected = true;
		} else
			selectedPiece = null;
	}
		
		
	private Player CreateNewPlayer(GroundPiece piece)
	{
		Player p = new Player (PlayerTemplate, piece.transform.position);
		p.piece = piece;
		players.Add (p);
		return p;
	}
}
