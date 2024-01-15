using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    public Scoring Scoringmanager;
    public Movement MovingBoi;
    public float bulletSpeed, pointsPerTarget, fireRate, turretAnimationSpeed, pulseInterval,TargetLife;
    public int willPulse, wavesToUpgrade, maxMissed;
    // Start is called before the first frame update
    void Start()
    {
        foreach(TurretController turrcorr in MovingBoi.turrets)
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
