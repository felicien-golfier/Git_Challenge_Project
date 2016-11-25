using UnityEngine;
using System.Collections;
using System.Security.Policy;
using System.Collections.Generic;
using UnityEditor;

public class GroundEditor : MonoBehaviour
{

    public int length = 10;
    public int width = 10;
    public int height = 5;
    // hexagone : 1/(2*sqrt(3)) + 1/sqrt(3)
    public float lengthThreshold = 1;
    public float widthThreshold = 1;
    public float heightThreshold = 1;
    public float lateralShift = 1 / 3;

    private string pieciesParentName = "piecies";

    public GameObject prefab;
    private GroundPiece[][][] ground;
    private Transform groundPiecies;


    // Use this for initialization
    void Start()
    {
        if (ground == null && !GetExistingGroundPiecies())
        {
            Debug.Log("No piecies founded");

        }
        else if (ground != null)
        {
            Debug.Log("Got your F*king piecies");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetAllPiecies(bool editor = false)
    {
        //if (ground == null)
        //    return;
        //for (int k = 0; k < height; k++)
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            if (ground[k][i][j] != null)
        //            {
        //                ground[k][i][j].Delete();
        //                if (editor)
        //                    DestroyImmediate(ground[k][i][j].gameObject);
        //                else
        //                    Destroy(ground[k][i][j].gameObject);
        //            }
        //        }
        //    }
        //}
        DestroyImmediate(GameObject.Find(pieciesParentName));
        if (ground != null)
            ground = null;

    }

    public bool GetExistingGroundPiecies()
    {
        if (groundPiecies == null)
        {
            if (GameObject.Find(pieciesParentName) != null)
            {
                groundPiecies = GameObject.Find(pieciesParentName).transform;
            }
            else
                return false;
        }
        else
            return true;
        try
        {
            ground = new GroundPiece[height][][];
            for (int k = 0; k < height; k++)
            {
                ground[k] = new GroundPiece[length][];
                for (int i = 0; i < length; i++)
                {
                    ground[k][i] = new GroundPiece[width];
                    for (int j = 0; j < width; j++)
                    {
                        Transform groundPiece = groundPiecies.FindChild(k + "x" + i + "x" + j);
                        if (groundPiece != null)
                        {
                            ground[k][i][j] = groundPiece.GetComponent<GroundPiece>();
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            return false;
        }
        return true;
    }


    public void InstanciatekGround()
    {
        if (groundPiecies == null)
        {
            groundPiecies = new GameObject().transform;
            groundPiecies.name = pieciesParentName;
        }
        ground = new GroundPiece[height][][];
        for (int k = 0; k < height; k++)
        {
            ground[k] = new GroundPiece[length][];
            for (int i = 0; i < length; i++)
            {
                ground[k][i] = new GroundPiece[width];
                for (int j = 0; j < width; j++)
                {
                    GroundPiece groundPiece = Instantiate(prefab).GetComponent<GroundPiece>();
                    groundPiece.transform.position = new Vector3((float)i * lengthThreshold, (float)k * heightThreshold, (float)j * widthThreshold + (i % 2 == 1 ? lateralShift : 0));
                    groundPiece.transform.SetParent(groundPiecies);
                    groundPiece.name = k + "x" + i + "x" + j;
                    groundPiece.k = k;
                    groundPiece.i = i;
                    groundPiece.j = j;
                    ground[k][i][j] = groundPiece;
                }
            }
        }
    }

    public void SetMovablePiecies(Player player)
    {
        int moveRange = player.moveRange;
        int pk = (int)player.piece.k;
        int pi = (int)player.piece.i;
        int pj = (int)player.piece.j;
        for (int i = pi - moveRange; i < pi + moveRange; i++)
        {
            for (int j = pj - moveRange; j < pj + moveRange; j++)
            {
                if (i >= 0 && j >= 0 && i < ground[pk].Length && j < ground[pk][i].Length && ground[pk][i][j] != null && ground[pk][i][j] != player.piece)
                    ground[pk][i][j].movable = true;
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(GroundEditor))]
    public class GMEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GroundEditor myScript = (GroundEditor)target;

            if (GUILayout.Button("Get All Existing Ground Piecies"))
            {
                myScript.GetExistingGroundPiecies();

            }

            if (GUILayout.Button("Load All Ground Piecies"))
            {
                //myScript.ResetAllPiecies(true);
                myScript.InstanciatekGround();
            }

            if (GUILayout.Button("Reset All Ground Piecies"))
            {
                myScript.ResetAllPiecies(true);

            }
        }
    }


#endif
}