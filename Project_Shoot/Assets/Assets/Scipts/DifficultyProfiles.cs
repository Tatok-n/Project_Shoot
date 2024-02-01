using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DifficultyProfiles : MonoBehaviour
{
    public int difficulty;
    // Start is called before the first frame update
    public void SetDiff()
    {
        difficulty = PlayerPrefs.GetInt("Diff");
        switch (difficulty)
        {
            case 0: //easy 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.2f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.2f);
                PlayerPrefs.SetFloat("PulseInterval", 3f);
                PlayerPrefs.SetInt("WillPulse", 0);
                PlayerPrefs.SetFloat("pointsPerTarget", 1138f);
                PlayerPrefs.SetFloat("TargetLife", 8f);
                PlayerPrefs.SetFloat("turretLimit", 2f);
                PlayerPrefs.SetInt("WavesTillUpgrade", 3);
                PlayerPrefs.SetInt("maxTargetsmissed", 10);
                PlayerPrefs.SetInt("Upgrades", 1);
                PlayerPrefs.SetInt("UpgradeStep", 1);
                PlayerPrefs.SetInt("SpawnOnBreak", 0);
                PlayerPrefs.SetInt("numPadsHor", 11);
                PlayerPrefs.SetInt("numPadsVert", 11);
                PlayerPrefs.SetInt("padSpacing", 5);
                break;
            case 1: //medium 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.4f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.4f);
                PlayerPrefs.SetFloat("PulseInterval", 3f);
                PlayerPrefs.SetInt("WillPulse", 0);
                PlayerPrefs.SetFloat("pointsPerTarget", 1364f);
                PlayerPrefs.SetFloat("TargetLife", 5f);
                PlayerPrefs.SetFloat("turretLimit", 1f);
                PlayerPrefs.SetInt("WavesTillUpgrade", 2);
                PlayerPrefs.SetInt("maxTargetsmissed", 7);
                PlayerPrefs.SetInt("Upgrades", 1);
                PlayerPrefs.SetInt("UpgradeStep", 1);
                PlayerPrefs.SetInt("SpawnOnBreak", 0);
                PlayerPrefs.SetInt("numPadsHor", 7);
                PlayerPrefs.SetInt("numPadsVert", 7);
                PlayerPrefs.SetInt("padSpacing", 5);
                break;
            case 2: //hard 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.6f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.5f);
                PlayerPrefs.SetFloat("PulseInterval", 8f);
                PlayerPrefs.SetInt("WillPulse", 1);
                PlayerPrefs.SetFloat("pointsPerTarget", 1658f);
                PlayerPrefs.SetFloat("TargetLife", 5f);
                PlayerPrefs.SetFloat("turretLimit", 1f);
                PlayerPrefs.SetInt("WavesTillUpgrade", 1);
                PlayerPrefs.SetInt("maxTargetsmissed", 7);
                PlayerPrefs.SetInt("Upgrades", 1);
                PlayerPrefs.SetInt("UpgradeStep", 2);
                PlayerPrefs.SetInt("SpawnOnBreak", 0);
                PlayerPrefs.SetInt("numPadsHor", 5);
                PlayerPrefs.SetInt("numPadsVert", 5);
                PlayerPrefs.SetInt("padSpacing", 5);
                break;
            case 3: //custom
                break;

        }
    }
     void Awake()
    {
        SetDiff();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
