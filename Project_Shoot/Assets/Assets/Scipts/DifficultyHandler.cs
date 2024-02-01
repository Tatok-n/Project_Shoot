using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    public Scoring Scoringmanager;
    public Movement MovingBoi;
    public float bulletSpeed, pointsPerTarget, fireRate, turretAnimationSpeed, pulseInterval,TargetLife;
    public int willPulse, wavesToUpgrade, maxMissed, wavesTillUpgrade, UpgradeStep;
    public bool spawnOnBreak, IncrementSpawns;
    // Start is called before the first frame update
    void Start()
    {
        turretAnimationSpeed = PlayerPrefs.GetFloat("TurretAnimationSpeed") ;
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        pulseInterval = PlayerPrefs.GetFloat("PulseInterval") ;
        willPulse = PlayerPrefs.GetInt("WillPulse") ;
        pointsPerTarget = PlayerPrefs.GetFloat("pointsPerTarget") ;
        TargetLife = PlayerPrefs.GetFloat("TargetLife") ;
        fireRate = PlayerPrefs.GetFloat("turretLimit");
        wavesToUpgrade = PlayerPrefs.GetInt("wavesToUpgrade");
        maxMissed = PlayerPrefs.GetInt("maxTargetsmissed");
        wavesTillUpgrade = PlayerPrefs.GetInt("WavesTillUpgrade",2);
        UpgradeStep = PlayerPrefs.GetInt("UpgradeStep", 1);
        if (PlayerPrefs.GetInt("SpawnOnBreak",0) == 1)
        {
            spawnOnBreak = true;
        } else
        {
            spawnOnBreak= false;
        }
        if (PlayerPrefs.GetInt("Upgrades", 0) == 1)
        {
            IncrementSpawns = true;
        }
        else
        {
            IncrementSpawns = false;
        }





        foreach (TurretController turrcorr in MovingBoi.turrets)
        {
            turrcorr.Speed = turretAnimationSpeed;
            turrcorr.BulletSpeed = bulletSpeed;
        }
        
        MovingBoi.PulseInterval = pulseInterval;
        MovingBoi.willPulse = willPulse;
        Scoringmanager.TargetPoints = pointsPerTarget;
        Scoringmanager.TargetLife = TargetLife;
        Scoringmanager.turretLimit = fireRate;
        Scoringmanager.wavesToUpgrade = wavesToUpgrade;
        Scoringmanager.maxTargetsmissed = maxMissed;
        Scoringmanager.spawnNewTargetsOnBreak = spawnOnBreak;
        Scoringmanager.increasingTargets = IncrementSpawns;
        Scoringmanager.wavesToUpgrade = wavesTillUpgrade;
        Scoringmanager.spawnIncrement = UpgradeStep;

    }

    // Update is called once per frame
    void Update()
    {
       if (MovingBoi.turrets[0].BulletSpeed != bulletSpeed)
        {
            foreach (TurretController turrcorr in MovingBoi.turrets)
            {
                turrcorr.Speed = turretAnimationSpeed;
                turrcorr.BulletSpeed = bulletSpeed;
            }
        }
    }
}
