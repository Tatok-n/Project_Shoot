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
        switch (difficulty)
        {
            case 0: //easy 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.2f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.2f);
                PlayerPrefs.SetFloat("PulseInterval", 3f);
                PlayerPrefs.SetInt("WillPulse", 0);
                PlayerPrefs.SetFloat("pointsPerTarget", 125f);
                PlayerPrefs.SetFloat("TargetLife", 8f);
                PlayerPrefs.SetFloat("turretLimit", 2f);
                PlayerPrefs.SetInt("wavesToUpgrade", 3);
                PlayerPrefs.SetInt("maxTargetsmissed", 10);
                break;
            case 1: //medium 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.4f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.4f);
                PlayerPrefs.SetFloat("PulseInterval", 3f);
                PlayerPrefs.SetInt("WillPulse", 0);
                PlayerPrefs.SetFloat("pointsPerTarget", 250f);
                PlayerPrefs.SetFloat("TargetLife", 5f);
                PlayerPrefs.SetFloat("turretLimit", 1f);
                PlayerPrefs.SetInt("wavesToUpgrade", 2);
                PlayerPrefs.SetInt("maxTargetsmissed", 7);
                break;
            case 2: //hard 
                PlayerPrefs.SetFloat("TurretAnimationSpeed", 0.6f);
                PlayerPrefs.SetFloat("BulletSpeed", 0.5f);
                PlayerPrefs.SetFloat("PulseInterval", 8f);
                PlayerPrefs.SetInt("WillPulse", 1);
                PlayerPrefs.SetFloat("pointsPerTarget", 500f);
                PlayerPrefs.SetFloat("TargetLife", 5f);
                PlayerPrefs.SetFloat("turretLimit", 1f);
                PlayerPrefs.SetInt("wavesToUpgrade", 1);
                PlayerPrefs.SetInt("maxTargetsmissed", 7);
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
