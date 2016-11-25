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
		Attack = 1,
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
        if (selectedPiece != null && selectedPiece.player != null && Input.GetMouseButtonDown(1))
        {
            if (action == Action.Attack)
            {
                action = Action.None;
                UnSelectPiece();
            }
            else
            {
                action = Action.Attack;
                GroundManager.instance.UnSetMovablePiecies();
            }
        }
    }

	private void ClickOnPiece(GroundPiece piece){
		
		switch(action){

		case Action.None:
			SelectPiece (piece);
			if (piece.player != null)
            {
				GroundManager.instance.SetMovablePiecies (piece.player);
				action = Action.Moving;
			}
			break;

		case Action.Moving:
                if (selectedPiece == null || selectedPiece == piece )
                {
                    SelectPiece(piece);
                    GroundManager.instance.UnSetMovablePiecies();
                }else if (piece.player != null)
                {
                    action = Action.None;
                    GroundManager.instance.UnSetMovablePiecies();
                    ClickOnPiece(piece);
                    return;
                }
                else if (selectedPiece.player != null && piece.movable)
                    selectedPiece.player.Move(piece);
                else
                {
                    break;
                }
			action = Action.None;
            break;


            case Action.Attack:
                if (selectedPiece != null && selectedPiece.player != null)
                    selectedPiece.player.LaunchAttack(piece);
                //action = Action.None;
                break;


		case Action.Creation:
			if (players.Count < PlayerNumber && piece.player == null) {
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
		
    private void UnSelectPiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.selected = false;
            selectedPiece = null;
            GroundManager.instance.UnSetMovablePiecies();
        }
    }
	private Player CreateNewPlayer(GroundPiece piece)
	{
		Player p = new Player (PlayerTemplate, piece);
		players.Add (p);
		return p;
	}
}
