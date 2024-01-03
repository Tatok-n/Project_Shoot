using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using TMPro;

public class Scoring : MonoBehaviour
{
    public float minx,minz,maxx,maxz,spacing,TargetLife,turretCounter, turretLimit;
    public ShootRay shooter;

    public int numTargets,missedTargets,maxTargetsmissed;
    public float TargetPoints,score,SpawnTime,CurrentHighScore;
    public GameObject TargetBoi,NormalUI,GameOverScreen;
    public AnimationCurve ScoreCurve;
    public Vector3[] TargetPos;
    public bool spawnOnPoints;
    public GunController gun;
    public TextMeshProUGUI ScoreUI,MissedUI,HighScore;
    public GameObject ShootingAnimation;
    public Transform ShootingPos;
    public TurretController[] turrets;
    public Movement mov;
    // Start is called before the first frame update
    void Start()
    {
        
        score = 0f;
        spawnTargets(numTargets);
        TargetPos = new Vector3[numTargets];
    }
    void OnFire() {
        score += shooter.Fire();
        UpdateUI();
        gun.fire = true;
        gun.ShootingTime = 0f;
        Instantiate(ShootingAnimation, ShootingPos.position, ShootingPos.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        if (turrets.Length == 0) {
            turrets = mov.turrets;
        }
        if (!spawnOnPoints) {
            SpawnTime += Time.deltaTime;
            if (SpawnTime>=TargetLife) {
                spawnTargets(numTargets);
                SpawnTime =0f;
            }
        }

        if (missedTargets>=maxTargetsmissed) {
            GameOver();
        }

        turretCounter += Time.deltaTime;
        if (turretCounter>=turretLimit) {
            System.Random rnd1 = new System.Random();
            TurretController tur = turrets[rnd1.Next(0,turrets.Length-1)];
            if (tur.Shoot) {
                tur =  turrets[rnd1.Next(0,turrets.Length)];
            }
            tur.Fire();
            turretCounter = 0f;
        }
        
    }

    public void GameOver() {
        if (PlayerPrefs.GetFloat("HighScore")!= 0f && score>PlayerPrefs.GetFloat("HighScore")) {
                PlayerPrefs.SetFloat("HighScore", score);
            }
            HighScore.text = "Current High Score : " + Mathf.Round(PlayerPrefs.GetFloat("HighScore")).ToString();
            NormalUI.SetActive(false);
            GameOverScreen.SetActive(true);
            
    }

    public void spawnTargets(int number) {
        Vector3 pos = new Vector3(0f,0f,0f);
        System.Random rnd = new System.Random();
        for (int i = 0; i< number; i++){
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

    public void spawnTarget() {
        Vector3 pos = new Vector3(0f,0f,0f);
        System.Random rnd = new System.Random();
        int randomX = rnd.Next(0,(int) Math.Floor((maxx-minx)/spacing));
        int randomZ = rnd.Next(0,(int) Math.Floor((maxz-minz)/spacing));
        int  randomY = rnd.Next(1,10);
        pos.x = minx + randomX*spacing;
        pos.z = minz + randomZ*spacing;
        pos.y = randomY;
        Instantiate(TargetBoi, pos, TargetBoi.transform.rotation);

    }

    public void UpdateUI () {
        ScoreUI.text = "Current score: " + Mathf.Round(score).ToString();
        MissedUI.text = "Missed targets: " + (missedTargets).ToString();
    }
}
