using UnityEngine;
using System.Collections;
using System.Security.Policy;
using System.Collections.Generic;
using UnityEditor;

public class GroundManager : Singleton<GroundManager> {

    [HideInInspector]
	public List<GroundPiece> movablePiecies;
	private GroundPiece[][][] ground;
    private Transform groundPieciesGO;
    private string pieciesParentName = "piecies";

    private int height;
    private int length;
    private int width;

    // Use this for initialization
    void Start () {
        if (!GetExistingGroundPiecies())
        {
            Debug.Log("No piecies founded");

        }else if (ground != null)
        {
            Debug.Log("Got your F*king piecies");
        }
		
	}

    public bool GetExistingGroundPiecies()
    {
        if (groundPieciesGO == null)
        {
            if (GameObject.Find(pieciesParentName) != null)
            {
                groundPieciesGO = GameObject.Find(pieciesParentName).transform;
            }
            else
                return false;
        }

        try
        {
            var piecesTr = GetGround(out height, out length, out width, groundPieciesGO);

            ground = new GroundPiece[height][][];
            for (int k = 0; k < height; k++)
            {
                ground[k] = new GroundPiece[length][];
                for (int i = 0; i < length; i++)
                {
                    ground[k][i] = new GroundPiece[width];
                    for (int j = 0; j < width; j++)
                    {
                        Transform pieceTr;
                        if ( piecesTr.TryGetValue(new Vector3(k,i,j), out pieceTr))
                        {
                            ground[k][i][j] = pieceTr.GetComponent<GroundPiece>();
                        }
                    }
                }
            }
        }
        catch(System.Exception e)
        {
            return false;
        }
        return true;
    }

    private static Dictionary<Vector3,Transform> GetGround(out int height, out int length, out int width, Transform pieciesParent)
    {
        height = 0;
        length = 0;
        width = 0;

        Dictionary<Vector3, Transform> groundPiecies = new Dictionary<Vector3, Transform>();
        foreach (Transform t in pieciesParent)
        {
            string[] positions = t.name.Split('x');
            if (positions.Length == 3)
            {
                
                int y = int.Parse(positions[0]);
                int x = int.Parse(positions[1]);
                int z = int.Parse(positions[2]);
                groundPiecies.Add(new Vector3(y, x, z), t);
                height = height <= y ? y+1 : height;
                length = length <= x ? x+1 : length;
                width = width <= z ? z+1 : width;
            }
        }
        return groundPiecies;
    }

	public void SetMovablePiecies(Player player)
	{
		int moveRange = player.moveRange;
		int pk = (int) player.piece.k;
		int pi = (int) player.piece.i;
		int pj = (int) player.piece.j;
		for (int i = pi-moveRange; i< pi+moveRange; i++){
			for (int j = pj-moveRange;j< pj+moveRange;j++){
				if (i>=0 && j>=0 && i < ground [pk].Length && j < ground [pk][i].Length && ground[pk][i][j] != null && ground[pk][i][j] != player.piece)
					ground [pk] [i] [j].movable = true;
			}
		}
	}

	public void UnSetMovablePiecies(){
        GroundPiece[] tmp = new GroundPiece[movablePiecies.Count];
        movablePiecies.CopyTo(tmp);
		foreach(var p in tmp)
        {
            p.movable = false;
        }
	}
}