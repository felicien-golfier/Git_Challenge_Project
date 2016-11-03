using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Security.Policy;

public class GameManager : MonoBehaviour {

	public float PlayerNumber = 1;
	public PlayerManager PlayerTemplate;
	private List<Player> players = new List<Player> ();
	private GroundPiece selectedPiece;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			Transform tmp = Tools.GetSelectedGO ();
			if (tmp != null) 
			{
				GroundPiece piece = tmp.GetComponent<GroundPiece> ();
				if (piece != null && players.Count < PlayerNumber)
				{
					piece.player = CreateNewPlayer (piece.transform.position);
				}
				else if (piece != null && selectedPiece != piece && !piece.movable){
					SelectNewPiece (piece);
				} else if (piece != null && selectedPiece.player != null && piece.movable){
					selectedPiece.player.Move (piece);
				}
			}
		}
	}

	private void SelectNewPiece(GroundPiece piece){
		if (selectedPiece == piece)
			return;
		if (selectedPiece != null)
		selectedPiece.selected = false;
		selectedPiece = piece;
		piece.selected = true;
		if (piece.player != null)
			GroundManager.instance.SetMovablePiecies (piece.player);



	}

	private Player CreateNewPlayer(Vector3 position)
	{
		Player p = new Player (PlayerTemplate, position);
		players.Add (p);
		return p;
	}
}
