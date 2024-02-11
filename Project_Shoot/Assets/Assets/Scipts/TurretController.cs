using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.VFX;


public class TurretController : MonoBehaviour
{
    public VisualEffect spawnEffect;
    public float radius,lifetime,DefRad,Speed,dirFront,dirRight,BulletSpeed;
    public Vector3 Spherepos,DefPos;
    public int spawnRate,friendCount;
    public Transform spawnpos,turrettrans,newtrans;

    public AnimationCurve radiusCurve, PosCurve;
    public GameObject Child;

    public bool Shoot, playanimation;

    public PewPewController shot;
    public AudioSource turreShot;

    public TurretController leftTurret, RightTurret;

    // Start is called before the first frame update
    void Awake()
    {
        Child = GameObject.Find("PewPew Shot");

    }
    public void PlaySound()
    {
        turreShot.Play();
    }
    public void TurretShoot() {
        if (playanimation) {
            spawnEffect.Play();
            playanimation = false;
        }
        lifetime += Time.deltaTime*Speed;
        radius = DefRad*radiusCurve.Evaluate(lifetime);
        Spherepos = new Vector3 (DefPos.x + PosCurve.Evaluate(lifetime), DefPos.y, DefPos.z);
        spawnEffect.SetFloat("CircleRadius", radius);
        spawnEffect.SetVector3("CirclePos", Spherepos);
        if (lifetime <= 1f) {
            spawnEffect.SetInt("SpawnRate",spawnRate);
        } else {
            spawnEffect.SetInt("SpawnRate",0);
            newtrans = spawnpos;
            newtrans.localScale = new Vector3 (1f,1f,1f);
            GameObject projectileIntantiated = Instantiate(Child, newtrans);
            shot = projectileIntantiated.GetComponent<PewPewController>();
            projectileIntantiated.GetComponent<Transform>().position = newtrans.position;
            //PlaySound();
            shot.speed = BulletSpeed;
            Shoot = false;
            lifetime = 0f;
           
        }
    }
    public void Fire() {
        Shoot = true;
        spawnEffect.Play();
    }

    public void ShootRecurs(int depth)
    {
        
        if (depth == 0) {
            Fire();
            UnityEngine.Debug.Log(depth);
            return;
        }
       
        int LeftRight = UnityEngine.Random.Range(0, 2);
        if (LeftRight ==1 ) { 
            if (leftTurret!=null)
            {
                leftTurret.ShootRecurs(depth - 1);
            } else
            {
                Fire();
            }
        }
        if (LeftRight == 0)
        {
            if (RightTurret != null)
            {
                RightTurret.ShootRecurs(depth - 1);
            }
            else
            {
                Fire();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Shoot) {
            TurretShoot();
            playanimation = true;
        }

        if (friendCount < 2)
        {
            RaycastHit rightEm;
            if (Physics.Raycast(turrettrans.position, transform.forward, out rightEm, 10f))
            {
                if (rightEm.collider.tag == "Wall")
                {
                    RightTurret = null;
                    friendCount = 1;
                } else if (rightEm.collider.tag == "Turret")
                {
                    RightTurret = rightEm.collider.GetComponent<TurretController>();
                    friendCount ++;
                }
            }

        }

        if (friendCount < 2)
        {
            RaycastHit leftEm;
            if (Physics.Raycast(turrettrans.position, -transform.forward, out leftEm, 10f))
            {
                if (leftEm.collider.tag == "Wall")
                {
                    leftTurret = null;
                    friendCount = 1;
                }
                else if (leftEm.collider.tag == "Turret")
                {
                    leftTurret = leftEm.collider.GetComponent<TurretController>();
                    friendCount++;
                }
            }

        }

    }
}
