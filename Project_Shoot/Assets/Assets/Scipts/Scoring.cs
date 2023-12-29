using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class Scoring : MonoBehaviour
{
    public float minx,minz,maxx,maxz,spacing;
    public ShootRay shooter;

    public int numTargets;
    public float score;
    public GameObject TargetBoi;
    public Vector3[] TargetPos;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        spawnTargets();
        TargetPos = new Vector3[numTargets];
    }
    void OnFire() {
        score += shooter.Fire();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnTargets() {
        Vector3 pos = new Vector3(0f,0f,0f);
        System.Random rnd = new System.Random();
        for (int i = 0; i< numTargets; i++){
            int randomX = rnd.Next(0,(int) Math.Floor((maxx-minx)/spacing));
            int randomZ = rnd.Next(0,(int) Math.Floor((maxz-minz)/spacing));
            int  randomY = rnd.Next(1,10);
            pos.x = minx + randomX*spacing;
            pos.z = minz + randomZ*spacing;
            pos.y = randomY;
            while (TargetPos.Contains(pos)) {
                randomX = rnd.Next(0,(int) Math.Floor((maxx-minx)/spacing));
                randomZ = rnd.Next(0,(int) Math.Floor((maxz-minz)/spacing));
                randomY = rnd.Next(1,10);
                pos.x = minx + randomX*spacing;
                pos.z = minz + randomZ*spacing;
                pos.y = randomY;
            }
            TargetPos[i] = pos;
        }

        foreach (Vector3 TarPos in TargetPos) {
            Instantiate(TargetBoi, TarPos, TargetBoi.transform.rotation);

        }
    }
}
