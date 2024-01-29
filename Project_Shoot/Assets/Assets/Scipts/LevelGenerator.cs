using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Material FloorMat;
    public Transform FloorTransform, leftWall, rightWall, frontWall, backWall;
    public float padSpacingHor, padSpacingVert;
    public int numPadsHor, numPadsVert;
    // Start is called before the first frame update
    void Start()
    {
        SetFloor();
    }

    // Update is called once per frame
    void Update()
    {
        SetWalls();
    }


    public void SetFloor()
    {
        FloorMat.SetFloat("_ScaleX", FloorTransform.localScale.x);
        FloorMat.SetFloat("_ScaleY", FloorTransform.localScale.y);
    }

    public void SetWalls()
    {
        Vector3 leftWallpos = new Vector3((numPadsHor - 1f) / 2f * padSpacingHor + 4.5f, 0f, 0f);
        Vector3 rightWallpos = new Vector3(-((numPadsHor - 1) / 2 * padSpacingHor + 4.5f), 0f, 0f);
        Vector3 backWallpos = new Vector3(0f, 0f, -((numPadsVert - 1f) / 2f * padSpacingVert + 4.5f));
        Vector3 frontWallpos = new Vector3(0f, 0f, (numPadsVert - 1f) / 2f * padSpacingVert + 4.5f);
        /*
        leftWallpos.x = (numPadsHor - 1) / 2 * padSpacingHor + 4.5;
        rightWallpos.x = -((numPadsHor - 1) / 2 * padSpacingHor + 4.5);
        backWallpos.z = -((numPadsVert - 1) / 2 * padSpacingVert + 4.5);
        frontWallpos.z = (numPadsVert - 1) / 2 * padSpacingVert + 4.5;
        */
        leftWall.position = leftWallpos;
        rightWall.position = rightWallpos;   
        backWall.position = backWallpos; 
        frontWall.position = frontWallpos;


    }
}
