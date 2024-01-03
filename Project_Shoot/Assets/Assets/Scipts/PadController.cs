using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PadController : MonoBehaviour
{
    public Light spot;
    public float Life,Speed,Strength,ScaleX,ScaleY,ScaleZ;
    public Transform padTransform,frontemitter,leftemitter,rightemitter,backemitter,TurretSpawn;
    public Vector3 padPos;
    public bool Used,recFront,recBack,recRight,recLeft,Topulse,touchingPlayer,spawnedturret;
    public Color ActionColor;
    public AnimationCurve ActionCurve;
    public PadController front,back,left,right;
    public GameObject Turret;
    public Movement mov;

    public Scoring score;
    public TurretController turretBoi;
    public VisualEffect pulsefx;

    // Start is called before the first frame update
    void Awake()
    {
        TurretSpawn.position = padTransform.position;
        TurretSpawn.rotation = padTransform.rotation;
        TurretSpawn.localScale = new Vector3(0.5f,3f,0.5f);
        padPos = padTransform.position;
        spot.intensity = 0f;
        RaycastHit hitright;
        if (Physics.Raycast(rightemitter.position, transform.TransformDirection(Vector3.right) , out hitright, 100f))
        {

            if (hitright.collider.tag == "Pads") {
                right = hitright.collider.GetComponent<PadController>();
            } else if (!spawnedturret)  {
                spawnedturret = true;
                TurretSpawn.position = new Vector3 (hitright.point.x, hitright.point.y + 1.5f, hitright.point.z);
                Instantiate(Turret,TurretSpawn);
                turretBoi = GetComponentInChildren<TurretController>();
                turretBoi.dirRight = -1f;                      
                
            }
            

        }

        RaycastHit hitleft;
        if (Physics.Raycast(leftemitter.position, transform.TransformDirection(Vector3.left) , out hitleft, 100f))
        {

            if (hitleft.collider.tag == "Pads") {
                left = hitleft.collider.GetComponent<PadController>();
            } else if (!spawnedturret)  {
                spawnedturret = true;
                TurretSpawn.position = new Vector3 (hitleft.point.x, hitleft.point.y + 1.5f, hitleft.point.z);
                TurretSpawn.eulerAngles = new Vector3 (TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 180f, TurretSpawn.eulerAngles.z);
                Instantiate(Turret,TurretSpawn);
                turretBoi = GetComponentInChildren<TurretController>();
                turretBoi.dirRight = 1f;           
                
            }
            

        }

        RaycastHit hitfront;
        if (Physics.Raycast(frontemitter.position, transform.TransformDirection(Vector3.forward) , out hitfront, 100f))
        {

            if (hitfront.collider.tag == "Pads") {
                front = hitfront.collider.GetComponent<PadController>();
            }else if (!spawnedturret)  {
                spawnedturret = true;
                TurretSpawn.position = new Vector3 (hitfront.point.x, hitfront.point.y + 1.5f, hitfront.point.z);
                TurretSpawn.eulerAngles = new Vector3 (TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 270f, TurretSpawn.eulerAngles.z);
                Instantiate(Turret,TurretSpawn);
                turretBoi = GetComponentInChildren<TurretController>();
                turretBoi.dirFront = -1f;   
            }
            

        }

        RaycastHit hitback;
        if (Physics.Raycast(backemitter.position, transform.TransformDirection(-Vector3.forward) , out hitback, 100f))
        {

            if (hitback.collider.tag == "Pads") {
                back = hitback.collider.GetComponent<PadController>();
            } else if (!spawnedturret)  {
                spawnedturret = true;
                TurretSpawn.position = new Vector3 (hitback.point.x, hitback.point.y + 1.5f, hitback.point.z);
                TurretSpawn.eulerAngles = new Vector3 (TurretSpawn.eulerAngles.x, TurretSpawn.eulerAngles.y + 90f, TurretSpawn.eulerAngles.z);
                Instantiate(Turret,TurretSpawn);
                turretBoi = GetComponentInChildren<TurretController>();
                turretBoi.dirFront = 1f;
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
