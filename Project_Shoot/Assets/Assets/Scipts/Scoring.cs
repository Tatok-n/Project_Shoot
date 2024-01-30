using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using TMPro;

public class Scoring : MonoBehaviour
{
    public float minx,minz,maxx,maxz,spacing,TargetLife,turretCounter, turretLimit, TargetPoints, score, SpawnTime, CurrentHighScore;
    public ShootRay shooter;
    public int numTargets,missedTargets,maxTargetsmissed,waves,wavesToUpgrade;
    public GameObject TargetBoi,NormalUI,GameOverScreen,GameOverButton;
    public AnimationCurve ScoreCurve;
    public Vector3[] TargetPos;
    public bool spawnNewTargetsOnBreak,increasingTargets;
    public GunController gun;
    public TextMeshProUGUI ScoreUI,MissedUI,HighScore;
    public GameObject ShootingAnimation;
    public Transform ShootingPos;
    public TurretController[] turrets;
    public Movement mov;
    // Start is called before the first frame update
    void Start()
    {
        maxx = mov.left.position.x;
        minx = mov.right.position.x;
        maxz = mov.back.position.z;
        minz = mov.front.position.z;
        waves = 0;
        score = 0f;
        
        
        if (increasingTargets)
        {
            numTargets = 1;
        } else {
            spawnTargets(numTargets);
        }
        
    }

    public void incrementTargets()
    {
        if (waves>wavesToUpgrade)
        {
            numTargets += 1;
        }
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
        if (!spawnNewTargetsOnBreak) {
            SpawnTime += Time.deltaTime;
            if (SpawnTime>=TargetLife) {
                SpawnTime =0f;
                waves += 1;
                incrementTargets();
                spawnTargets(numTargets);
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
        if (score>PlayerPrefs.GetFloat("HighScore",0f)) {
                PlayerPrefs.SetFloat("HighScore", score);
            }
            HighScore.text = "Current High Score : " + Mathf.Round(PlayerPrefs.GetFloat("HighScore")).ToString();
            mov.PauseHandler.PauseMenu.SetActive(false);
            NormalUI.SetActive(false);
            Time.timeScale = 0f;
        mov.PauseHandler.StartPauseMenu();
        mov.PauseHandler.GameEND = true;
            GameOverScreen.SetActive(true);
            GameOverButton.SetActive(false);


    }

    public void spawnTargets(int number) {
        if (number == 1)
        {
            spawnTarget();
            return;
        }
        TargetPos = new Vector3[number];
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
