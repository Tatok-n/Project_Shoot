using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics;





public class Scoring : MonoBehaviour
{
    public float minx,minz,maxx,maxz,spacing,TargetLife,turretCounter, turretLimit, TargetPoints, score, SpawnTime, CurrentHighScore;
    public ShootRay shooter;
    public int numTargets,missedTargets,maxTargetsmissed,waves,wavesToUpgrade,spawnIncrement;
    public GameObject TargetBoi,NormalUI,GameOverScreen,GameOverButton;
    public AnimationCurve ScoreCurve;
    public Vector3[] TargetPos;
    public bool spawnNewTargetsOnBreak,increasingTargets,isPaused;
    public GunController gun;
    public TextMeshProUGUI ScoreUI,MissedUI,HighScore;
    public GameObject ShootingAnimation;
    public Transform ShootingPos;
    public TurretController[] turrets;
    public Movement mov;
    public List<TargetController> targets = new List<TargetController>();
    public TurretController[] cardinalTurrets = new TurretController[4];
    // Start is called before the first frame update
    void OnEnable()
    {
        cardinalTurrets = new TurretController[4];
        maxx = mov.left.position.x;
        minx = mov.right.position.x;
        maxz = mov.back.position.z;
        minz = mov.front.position.z;
        waves = 0;
        score = 0f;

        if (PlayerPrefs.GetInt("Upgrades") == 1)
        {
            increasingTargets = true;
        } else
        {
            increasingTargets = false;
        }


        if (PlayerPrefs.GetInt("SpawnOnBreak") == 1)
        {
            spawnNewTargetsOnBreak = true;
        }
        else
        {
            spawnNewTargetsOnBreak = false;
        }

        if (increasingTargets)
        {
            numTargets = 1;
        } else if(!spawnNewTargetsOnBreak)
        {
            spawnTargets(numTargets);
        }
        
        if (spawnNewTargetsOnBreak)
        {
         
            spawnTargets(numTargets);
        }
        
    }

    public void incrementTargets()
    {
        if (waves>wavesToUpgrade)
        {
            numTargets += spawnIncrement;
            waves = 0;
        }
    }
    void OnFire() {
        if (isPaused)
        {
            return;
        }
        score += shooter.Fire();
        UpdateUI();
        gun.ShootSound();
        gun.fire = true;
        gun.ShootingTime = 0f;
        ShootingBoi Impact = Instantiate(ShootingAnimation, ShootingPos.position, ShootingPos.rotation).GetComponent<ShootingBoi>() ;
        if (shooter.Fire()!=0)
        {
            Impact.PlayImpact();
        }
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
        CardinalTurretManager();
        turretCounter += Time.deltaTime;
        if (turretCounter>=turretLimit) {
            TurretController tur = cardinalTurrets[UnityEngine.Random.Range(0,4)];
            
            if (tur.Shoot) {
                tur = cardinalTurrets[UnityEngine.Random.Range(0, 4)];
            }
            tur.ShootRecurs(UnityEngine.Random.Range(0, 5));
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
            int randomX = rnd.Next(0,(int) Math.Floor(((maxx-1)- (minx + 1)) /spacing));
            int randomZ = rnd.Next(0,(int) Math.Floor(((maxz-1)- (minz + 1)) /spacing));
            int  randomY = rnd.Next(1,10);
            pos.x = minx + randomX*spacing;
            pos.z = minz + randomZ*spacing;
            pos.y = randomY;
            while (TargetPos.Contains(pos)) {
                randomX = rnd.Next(0,(int) Math.Floor(((maxx - 1) - (minx + 1)) /spacing));
                randomZ = rnd.Next(0,(int) Math.Floor(((maxz-1)- (minz + 1)) /spacing));
                randomY = rnd.Next(1,10);
                pos.x = (minx + 1) + randomX*spacing;
                pos.z = (minz + 1) + randomZ*spacing;
                pos.y = randomY;
            }
            TargetPos[i] = pos;
        }

        foreach (Vector3 TarPos in TargetPos) {
          targets.Add(Instantiate(TargetBoi, TarPos, TargetBoi.transform.rotation).GetComponent<TargetController>());

        }
    }

    public void spawnTarget() {
        Vector3 pos = new Vector3(0f,0f,0f);
        System.Random rnd = new System.Random();
        int randomX = rnd.Next(0,(int)Math.Floor(((maxx - 1) - (minx + 1)) / spacing));
        int randomZ = rnd.Next(0,(int)Math.Floor(((maxz - 1) - (minz + 1)) / spacing));
        int  randomY = rnd.Next(1,10);
        pos.x = (minx + 1) + randomX * spacing;
        pos.z = (minz + 1) + randomZ * spacing;
        pos.y = randomY;
        
        targets.Add(Instantiate(TargetBoi, pos, TargetBoi.transform.rotation).GetComponent<TargetController>());

    }

    public void UpdateUI () {
        ScoreUI.text = "Current score: " + Mathf.Round(score).ToString();
        MissedUI.text = "Missed targets: " + (missedTargets).ToString();
    }

    public void CardinalTurretManager()
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        RaycastHit Front;
        Vector3 EmPos = new Vector3(mov.player.position.x, mov.player.position.y + 0.3f, mov.player.position.z);
        if (Physics.Raycast(EmPos, transform.forward, out Front, 100f, layerMask))
        {
           
            if (Front.collider.tag == "Turret")
            {

                cardinalTurrets[0] = Front.collider.GetComponent<TurretController>();
            }
           
        }
        RaycastHit Back;
        if (Physics.Raycast(EmPos, -transform.forward, out Back, 100f, layerMask))
        {
            
            if (Back.collider.tag == "Turret")
            {
                cardinalTurrets[1] = Back.collider.GetComponent<TurretController>();
            }

        }
        RaycastHit Right;
        if (Physics.Raycast(EmPos, transform.right, out Right, 100f, layerMask))
            {
                
            if (Right.collider.tag == "Turret")
            {
                cardinalTurrets[2] = Right.collider.GetComponent<TurretController>();
            }

        }
        RaycastHit Left;
        if (Physics.Raycast(EmPos, -transform.right, out Left, 100f, layerMask))
                {
                    
            if (Left.collider.tag == "Turret")
            {
                cardinalTurrets[3] = Left.collider.GetComponent<TurretController>();
            }

        }
    }
}
