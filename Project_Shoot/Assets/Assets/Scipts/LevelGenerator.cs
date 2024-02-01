using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Material FloorMat;
    public Transform FloorTransform, leftWall, rightWall, frontWall, backWall,TurretSpawn, TurretContainer;
    public float padSpacingHor, padSpacingVert;
    public int numPadsHor, numPadsVert;
    public GameObject Turret;
    public TurretController turretBoi;
    // Start is called before the first frame update
    void Awake()
    {
        GetCustomParams();
        SetFloor();
        SetWalls();
        SpawnTurrets();
    }

    public void GetCustomParams()
    {
        padSpacingHor = PlayerPrefs.GetFloat("padSpacing",5f);
        padSpacingVert = PlayerPrefs.GetFloat("padSpacing",5f);
        numPadsHor = PlayerPrefs.GetInt("numPadsHor",11);
        numPadsVert = PlayerPrefs.GetInt("numPadsVert",11);
    }
    void Update()
    {
        
    }


    public void SetFloor()
    {
        FloorMat.SetFloat("_ScaleX", FloorTransform.localScale.x);
        FloorMat.SetFloat("_ScaleY", FloorTransform.localScale.y);

        FloorTransform.localScale = new Vector3((numPadsHor+1) / 2, 1, (numPadsVert+1) / 2);
    }

    public void SetWalls()
    {
        Vector3 leftWallpos = new Vector3((numPadsHor - 1) / 2f * padSpacingHor + 4.5f, 0f, 0f); //EVERYTHIGN IS FLIPPED
        Vector3 rightWallpos = new Vector3(-((numPadsHor - 1) / 2f * padSpacingHor + 4.5f), 0f, 0f);
        Vector3 backWallpos = new Vector3(0f, 0f, (numPadsVert - 1f) / 2f * padSpacingVert + 4.5f);
        Vector3 frontWallpos = new Vector3(0f, 0f, -((numPadsVert - 1f) / 2f * padSpacingVert + 4.5f));
       
        leftWall.position = leftWallpos;
        rightWall.position = rightWallpos;   
        backWall.position = backWallpos; 
        frontWall.position = frontWallpos;


    }

    public void SpawnTurrets()
    {
        
        for (float i = rightWall.position.x + padSpacingVert; i <= leftWall.position.x; i = i + padSpacingVert)
        {
            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, 180f, TurretSpawn.eulerAngles.z); //do bottom wall
            //TurretSpan.localScale = 
            TurretSpawn.position = new Vector3(i, TurretSpawn.position.y, backWall.position.z);
            turretBoi = Instantiate(Turret, TurretSpawn.position, TurretSpawn.rotation, TurretContainer).GetComponentInChildren<TurretController>();
            turretBoi.dirFront = -1f;

            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, 0f, TurretSpawn.eulerAngles.z); //do front wall
            //TurretSpan.localScale = 
            TurretSpawn.position = new Vector3(i, TurretSpawn.position.y, frontWall.position.z);
            turretBoi = Instantiate(Turret, TurretSpawn.position, TurretSpawn.rotation, TurretContainer).GetComponentInChildren<TurretController>();
            turretBoi.dirFront = 1f;
        }
        
        for (float i = frontWall.position.z + padSpacingVert; i <= backWall.position.z; i = i + padSpacingVert)
        {
            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, 270, TurretSpawn.eulerAngles.z); //do left wall
            TurretSpawn.position = new Vector3(leftWall.position.x, TurretSpawn.position.y, i);
            turretBoi = Instantiate(Turret, TurretSpawn.position, TurretSpawn.rotation, TurretContainer).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = -1f;

            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, 90, TurretSpawn.eulerAngles.z); //do right wall
            TurretSpawn.position = new Vector3(rightWall.position.x, TurretSpawn.position.y, i);
            turretBoi = Instantiate(Turret, TurretSpawn.position, TurretSpawn.rotation, TurretContainer).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = 1f;
        }

        
    }
}
