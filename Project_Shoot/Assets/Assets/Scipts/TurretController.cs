using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TurretController : MonoBehaviour
{
    public VisualEffect spawnEffect;
    public float radius,lifetime,DefRad,Speed,dirFront,dirRight,BulletSpeed;
    public Vector3 Spherepos,DefPos;
    public int spawnRate;
    public Transform spawnpos,turrettrans,newtrans;

    public AnimationCurve radiusCurve, PosCurve;
    public GameObject Child;

    public bool Shoot,playanimation;

    public PewPewController shot;
    // Start is called before the first frame update
    void Awake()
    {
        Child = GameObject.Find("PewPew Shot");

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
            shot.speed = BulletSpeed;
            Shoot = false;
            lifetime = 0f;
           
        }
    }
    public void Fire() {
        Shoot = true;
        spawnEffect.Play();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Shoot) {
            TurretShoot();
            playanimation = true;
        }
    }
}