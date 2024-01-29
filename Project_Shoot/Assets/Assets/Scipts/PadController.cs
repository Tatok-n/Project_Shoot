using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

public class PadController : MonoBehaviour
{
    public Light spot;
    public float Life,Speed,Strength,ScaleX,ScaleY,ScaleZ;
    public Transform padTransform,frontemitter,leftemitter,rightemitter,backemitter,TurretSpawn,ParentTransform;
    public Vector3 padPos;
    public bool Used,recFront,recBack,recRight,recLeft,Topulse,touchingPlayer,spawnedturret;
    public Color ActionColor;
    public AnimationCurve ActionCurve;
    public PadController front,back,left,right;
    public GameObject Turret,Pad;
    public Movement mov;

    public Scoring score;
    public TurretController turretBoi;
    public VisualEffect pulsefx;

    public LevelGenerator lvlGen;

    // Start is called before the first frame update
    public void TrySpawnTurret() {
        if (padPos.z <=  (-(lvlGen.numPadsVert - 1) / 2 * lvlGen.padSpacingVert)) // is on the "front" border
        {
            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 270f, TurretSpawn.eulerAngles.z);
            TurretSpawn.position = new Vector3(padPos.x, padPos.y + 1.5f, lvlGen.backWall.position.z);
            turretBoi = Instantiate(Turret, TurretSpawn).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = 1f;
        } else if (padPos.z >= ((lvlGen.numPadsVert - 1) / 2 * lvlGen.padSpacingVert)) { // is on the "back" border
            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 90f, TurretSpawn.eulerAngles.z);
            TurretSpawn.position = new Vector3(padPos.x, padPos.y + 1.5f, lvlGen.frontWall.position.z);
            turretBoi = Instantiate(Turret, TurretSpawn).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = -1f;
        } else if (padPos.x <= (-(lvlGen.numPadsHor - 1) / 2 * lvlGen.padSpacingVert)) { //is on the "left" wall
            TurretSpawn.position = new Vector3(lvlGen.rightWall.position.x, padPos.y + 1.5f, padPos.z);
            TurretSpawn.eulerAngles = new Vector3(TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 180f, TurretSpawn.eulerAngles.z);
            turretBoi = Instantiate(Turret, TurretSpawn).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = 1f;
        } else if (padPos.x >= ((lvlGen.numPadsHor - 1) / 2 * lvlGen.padSpacingVert)) {
            TurretSpawn.position = new Vector3(lvlGen.leftWall.position.x, padPos.y + 1.5f, padPos.z);
            turretBoi = Instantiate(Turret, TurretSpawn).GetComponentInChildren<TurretController>();
            turretBoi.dirRight = -1f;
        }
    }
    public void SpawnPadRight()
    {
        if (padPos.x != ((lvlGen.numPadsHor - 1) / 2 * lvlGen.padSpacingVert))
        {
            right = Instantiate(Pad, new Vector3(padPos.x + lvlGen.padSpacingVert, padPos.y, padPos.z), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
          
            
    }

    public void SpawnPadLeft()
    {
        if (padPos.x != (-(lvlGen.numPadsHor - 1) / 2 * lvlGen.padSpacingVert))
        {
            left = Instantiate(Pad, new Vector3(padPos.x - lvlGen.padSpacingVert, padPos.y, padPos.z), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>(); ; ;
        }


    }

    public void SpawnPadBack()
    {
        
        if (padPos.z < ((lvlGen.numPadsVert - 1) / 2 * lvlGen.padSpacingVert))
        {
            back = Instantiate(Pad, new Vector3(padPos.x, padPos.y, padPos.z + lvlGen.padSpacingVert), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
       


    }

    public void SpawnPadFront()
    {
        if (padPos.z > (-(lvlGen.numPadsVert - 1) / 2 * lvlGen.padSpacingVert))
        {
            front = Instantiate(Pad, new Vector3(padPos.x, padPos.y, padPos.z - lvlGen.padSpacingVert), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
        
        
    }
    void Awake()
    {

        TurretSpawn.position = padTransform.position;
        TurretSpawn.rotation = padTransform.rotation;
        TurretSpawn.localScale = new Vector3(0.5f,3f,0.5f);
        padPos = padTransform.position;
        spot.intensity = 0f;
        lvlGen = GameObject.Find("LevelGen").GetComponentInChildren<LevelGenerator>();
        float spawnRange = lvlGen.padSpacingVert + 2;
        
        if (padPos.x == 0 && padPos.z == 0)
        {
            SpawnPadBack();
            SpawnPadFront();
            SpawnPadLeft();
            SpawnPadRight();
        }
        else if (padPos.z == 0 && padPos.x > 0)
        {
            SpawnPadBack();
            SpawnPadFront();
            SpawnPadRight();

        }
        else if (padPos.z == 0 && padPos.x < 0)
        {
            SpawnPadBack();
            SpawnPadFront();
            SpawnPadLeft();
        } else if (padPos.z <0)
        {
           SpawnPadFront();
        }
        else if (padPos.z > 0)
        {
            SpawnPadBack();
        }
        TrySpawnTurret();


        
        RaycastHit hitright;
        if (Physics.Raycast(rightemitter.position, transform.TransformDirection(Vector3.right) , out hitright, spawnRange))
        {

            if (hitright.collider.tag == "Pads") {
                right = hitright.collider.GetComponent<PadController>();
            }
            

        } else
        {
            right = Instantiate(Pad, new Vector3 (padPos.x+ lvlGen.padSpacingVert, padPos.y, padPos.z ),padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }

        RaycastHit hitleft;
        if (Physics.Raycast(leftemitter.position, transform.TransformDirection(Vector3.left) , out hitleft, spawnRange))
        {
            
            if (hitleft.collider.tag == "Pads") {
                left = hitleft.collider.GetComponent<PadController>();
            }


        }
        else
        {
            Instantiate(Pad, new Vector3(padPos.x - lvlGen.padSpacingVert, padPos.y, padPos.z), padTransform.rotation, ParentTransform);
        }

        
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            touchingPlayer = true;
            score = other.GetComponentInParent<Scoring>();
            mov = other.GetComponentInParent<Movement>();
            
        }
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (!Used) {
            spot.intensity = 0f;
            spot.color = Color.white;
        }
        if (Topulse) {
            Pulse();
        }

        if (mov!= null && !mov.CloseEnough(padPos, new Vector3 (mov.player.position.x,mov.groundval,mov.player.position.z), 1f)) {
            touchingPlayer = false;
        }
    }

    void Pulse() {
        Used = true;
        spot.color = ActionColor;
        Life += Speed*Time.deltaTime;
        spot.intensity = Strength*ActionCurve.Evaluate(Life);
        if (Life>= 1.5f) {
            Topulse = false;
            Used = false;
            Life = 0f;
            recBack = false;
            recFront = false;
            recLeft = false;
            recRight = false;
            pulsefx.Play();
            if (touchingPlayer) {
                score.GameOver();
            }
        } else if (Life >=0.8f) {
            if (!recFront && front!= null) {
                front.Topulse = true;
                front.recBack = true;
                recFront = true;
            }
            if (!recBack && back!= null) {
                back.Topulse = true;
                back.recFront = true;
                recBack = true;
            }
            if (!recLeft && left!= null) {
                left.recRight = true;
                left.Topulse = true;
                recLeft = true;
            }
            if (!recRight && right != null) {
                right.recLeft = true;
                right.Topulse = true;
                recRight = true;
            }

        } 

    }
}
