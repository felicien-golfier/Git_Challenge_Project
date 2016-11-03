using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class GroundManager : Singleton<GroundManager> {

	public int length = 10;
	public int width = 10;
	public int height = 5;
	public float lengthThreshold = 1;
	public float widthThreshold = 1;
	public float heightThreshold = 1;
	public GameObject prefab;

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
		
	}
}