using UnityEngine;
using System.Collections;
using System.Security.Policy;
using System.Collections.Generic;

public class GroundManager : Singleton<GroundManager> {

	public int length = 10;
	public int width = 10;
	public int height = 5;
	public float lengthThreshold = 1;
	public float widthThreshold = 1;
	public float heightThreshold = 1;

	public GameObject prefab;

	public List<GroundPiece> movablePiecies;
	private GroundPiece[][][] ground;
	private Transform groundPiecies;


	// Use this for initialization
	void Start () {
		InstanciatekGround ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InstanciatekGround()
	{
		groundPiecies = new GameObject ().transform;
		groundPiecies.name = "Piecies";

		ground = new GroundPiece[height][][];
		for (int k = 0; k < height; k++)
		{
			ground[k] = new GroundPiece [length][];
			for (int i = 0; i < length; i++)
			{
				ground [k] [i] = new GroundPiece[width];
				for (int j = 0; j < width; j++)
				{
					GroundPiece groundPiece = Instantiate (prefab).GetComponent<GroundPiece>();
					groundPiece.transform.position = new Vector3 ((float)i*lengthThreshold, (float)k*heightThreshold, (float)j*widthThreshold);
					groundPiece.transform.SetParent (groundPiecies);
					groundPiece.name = k + "x" + i + "x" + j;
					groundPiece.coord = new Vector3 (k, i, j);
					ground [k] [i] [j] = groundPiece;
				}
			}
		}
	}

	public void SetMovablePiecies(Player player)
	{
		int moveRange = player.moveRange;
		int pk = (int) player.piece.coord.x;
		int pi = (int) player.piece.coord.y;
		int pj = (int) player.piece.coord.z;
		for (int i = pi-moveRange; i< pi+moveRange; i++){
			for (int j = pj-moveRange;j< pj+moveRange;j++){
				if (i>=0 && j>=0 && i < ground [pk].Length && j < ground [pk][i].Length)
					ground [pk] [i] [j].movable = true;
			}
		}
	}
	public void UnSetMovablePiecies(){
		List<GroundPiece> tmp = movablePiecies;
		tmp.ForEach (x=>x.movable = false);
	}
}