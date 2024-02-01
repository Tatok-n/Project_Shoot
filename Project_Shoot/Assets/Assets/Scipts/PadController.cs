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

    float padSpacingVert;
    int numPadsHor;
    int numPadsVert;

    // Start is called before the first frame update

    public void SpawnPadRight()
    {
        if (padPos.x != ((numPadsHor - 1) / 2 * padSpacingVert))
        {
            right = Instantiate(Pad, new Vector3(padPos.x + padSpacingVert, padPos.y, padPos.z), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
          
            
    }

    public void SpawnPadLeft()
    {
        if (padPos.x != (-(numPadsHor - 1) / 2 * padSpacingVert))
        {
            left = Instantiate(Pad, new Vector3(padPos.x - padSpacingVert, padPos.y, padPos.z), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>(); ; ;
        }


    }

    public void SpawnPadBack()
    {
        
        if (padPos.z < ((numPadsVert - 1) / 2 * padSpacingVert))
        {
            back = Instantiate(Pad, new Vector3(padPos.x, padPos.y, padPos.z + padSpacingVert), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
       


    }

    public void SpawnPadFront()
    {
        if (padPos.z > (-(numPadsVert - 1) / 2 * padSpacingVert))
        {
            front = Instantiate(Pad, new Vector3(padPos.x, padPos.y, padPos.z - padSpacingVert), padTransform.rotation, ParentTransform).GetComponentInChildren<PadController>();
        }
        
        
    }
    void Awake()
    {
        padSpacingVert = PlayerPrefs.GetFloat("padSpacing", 5f);
        numPadsHor = PlayerPrefs.GetInt("numPadsHor", 11);
        numPadsVert = PlayerPrefs.GetInt("numPadsVert", 11);
        lvlGen = GameObject.Find("LevelGen").GetComponentInChildren<LevelGenerator>();
        lvlGen.GetCustomParams();
        TurretSpawn.position = padTransform.position;
        TurretSpawn.rotation = padTransform.rotation;
        TurretSpawn.localScale = new Vector3(0.5f,3f,0.5f);
        padPos = padTransform.position;
        spot.intensity = 0f;
        
        float spawnRange = padSpacingVert + 2;
        
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



        RaycastHit hitright;
        if (Physics.Raycast(rightemitter.position, transform.TransformDirection(Vector3.right), out hitright, 100f))
        {

            if (hitright.collider.tag == "Pads")
            {
                right = hitright.collider.GetComponent<PadController>();
            }
            else
            {
                right = null;
            }

        }
        RaycastHit hitleft;
        if (Physics.Raycast(leftemitter.position, transform.TransformDirection(Vector3.left), out hitleft, 100f))
        {

            if (hitleft.collider.tag == "Pads")
            {
                left = hitleft.collider.GetComponent<PadController>();
            }
            else
            {
                left = null;
            }


        }



    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            touchingPlayer = true;
            score = other.GetComponentInParent<Scoring>();
            mov = other.GetComponentInParent<Movement>();
            
        }
    }

    void Start()
    {
        RaycastHit hitfront;
        if (Physics.Raycast(frontemitter.position, transform.TransformDirection(Vector3.forward), out hitfront, 100f))
        {

            if (hitfront.collider.tag == "Pads")
            {
                front = hitfront.collider.GetComponent<PadController>();
            }
            else
            {
                front = null;
            }


        }

        RaycastHit hitback;
        if (Physics.Raycast(backemitter.position, transform.TransformDirection(-Vector3.forward), out hitback, 100f))
        {

            if (hitback.collider.tag == "Pads")
            {
                back = hitback.collider.GetComponent<PadController>();
            }
            else
            {
                back = null;
            }


        }
        RaycastHit hitright;
        if (Physics.Raycast(rightemitter.position, transform.TransformDirection(Vector3.right), out hitright, 100f))
        {

            if (hitright.collider.tag == "Pads")
            {
                right = hitright.collider.GetComponent<PadController>();
            }
            else
            {
                right = null;
            }

        }
        RaycastHit hitleft;
        if (Physics.Raycast(leftemitter.position, transform.TransformDirection(Vector3.left), out hitleft, 100f))
        {

            if (hitleft.collider.tag == "Pads")
            {
                left = hitleft.collider.GetComponent<PadController>();
            }
            else
            {
                left = null;
            }


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
