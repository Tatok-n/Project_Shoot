using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CustomLevelUI : MonoBehaviour
{
    public GameObject prevMenu, newMenu;
    public Slider NumPadsVert, NumPadsHor, PadSpacing, TurrAnimationSpeed, BulletSpeed, PulseInterval, TargetLife, FireRate, MaxMissed, Waves, incrementStep;
    public Toggle SpawnTars, Puls, Upgrades;
    public TextMeshProUGUI NumPadsVertTxt, NumPadsHorTxt, PadSpacingTxt, TurrAnimationSpeedTxt, BulletSpeedTxt, PulseIntervalTxt, TargetLifeTxt, FireRateTxt, MaxMissedTxt, WavesTxt, incrementStepTxt, pointsPerTarget;
    public float ScoreMulti;
    // Start is called before the first frame update
    void Start()
    {
        ScoreMulti = 1f;
    }

   public void enterMenu()
    {
        prevMenu.SetActive(false);
        newMenu.SetActive(true);
        PlayerPrefs.SetInt("Diff", 3);
    }

    public void exitMenu()
    {
        prevMenu.SetActive(true);
        newMenu.SetActive(false);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Custom_Level");
    }
    public void UpdatePrefs()
    {
        PlayerPrefs.SetFloat("padSpacing", PadSpacing.value);
        PlayerPrefs.SetInt("numPadsHor", (int)(NumPadsHor.value*2+1));
        PlayerPrefs.SetInt("numPadsVert", (int) NumPadsVert.value*2+1);
        PlayerPrefs.SetFloat("TurretAnimationSpeed", TurrAnimationSpeed.value);
        PlayerPrefs.SetFloat("BulletSpeed", BulletSpeed.value);
        PlayerPrefs.SetFloat("PulseInterval", PulseInterval.value);
        PlayerPrefs.SetFloat("TargetLife", TargetLife.value);
        PlayerPrefs.SetFloat("turretLimit", FireRate.value);
        PlayerPrefs.SetInt("maxTargetsmissed", (int)MaxMissed.value);
        PlayerPrefs.SetInt("WavesTillUpgrade", (int)Waves.value);
        PlayerPrefs.SetInt("UpgradeStep", (int)incrementStep.value);
        if (SpawnTars.isOn)
        {
            PlayerPrefs.SetInt("SpawnOnBreak", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SpawnOnBreak", 0);
        }

        if (Puls.isOn)
        {
            PlayerPrefs.SetInt("WillPulse", 1);
        }
        else
        {
            PlayerPrefs.SetInt("WillPulse", 0);
        }

        if (Upgrades.isOn)
        {
            PlayerPrefs.SetInt("Upgrades", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Upgrades", 0);
        }
        PlayerPrefs.SetFloat("pointsPerTarget", ScoreMulti * 100f);
    }

    public void UpdateTest()
    {
        NumPadsVertTxt.text = "Rows of pads : " + (NumPadsVert.value * 2 + 1).ToString();
        NumPadsHorTxt.text = "Columns of pads : " + (NumPadsHor.value * 2 + 1).ToString();
        PadSpacingTxt.text = "Pad spacing : " + Math.Round(PadSpacing.value, 2).ToString();
        TurrAnimationSpeedTxt.text = "Turret Animation Speed: " + Math.Round(TurrAnimationSpeed.value, 2).ToString();
        BulletSpeedTxt.text = "Bullet Speed: " + Math.Round(BulletSpeed.value, 2).ToString();
        PulseIntervalTxt.text = "Pulse Interval: " + Math.Round(PulseInterval.value, 2).ToString();
        TargetLifeTxt.text = "Target Life : " + Math.Round(TargetLife.value, 2).ToString();
        FireRateTxt.text = "Fire Rate: " + Math.Round(1f/FireRate.value, 2).ToString();
        MaxMissedTxt.text = "Max Targets missed: " + MaxMissed.value.ToString();
        WavesTxt.text = "Waves until upgrade: " + Waves.value.ToString();
        incrementStepTxt.text = "Upgrade Step: " + incrementStep.value.ToString();
        pointsPerTarget.text = "Points per Target :" + Math.Round((ScoreMulti*100), 1).ToString();
    }

    float ScoreMultiplier( float Value, float minVal, float maxVal, float maxMulti)
    {
        return (Math.Abs(Value - minVal) * maxMulti / (Math.Abs(maxVal - minVal)));
    }
    float ScoreMultiplier(int Value, int minVal, int maxVal, float maxMulti)
    {
        return (Math.Abs(Value - minVal) * maxMulti / (Math.Abs(maxVal - minVal)));
    }

    public void CalculateMulti(float score)
    {
        
        score +=ScoreMultiplier(NumPadsVert.value, 10, 2, 2);
        score += ScoreMultiplier(NumPadsHor.value, 10, 2, 2);
        score += ScoreMultiplier(TurrAnimationSpeed.value, 0.5f, 2f, 0.5f);
        score += ScoreMultiplier(BulletSpeed.value, 0.05f, 3f, 5f);
        score += ScoreMultiplier(PulseInterval.value, 10f, 0.5f, 2f) * PlayerPrefs.GetInt("WillPulse", 1) + PlayerPrefs.GetInt("WillPulse", 1);
        score += ScoreMultiplier(TargetLife.value, 10f, 0.5f, 3f) * (1-PlayerPrefs.GetInt("SpawnOnBreak",0));
        score += ScoreMultiplier(FireRate.value, 10f, 0.5f, 2f);
        score += ScoreMultiplier(TargetLife.value, 10, 0, 2);
        score += ScoreMultiplier(MaxMissed.value, 40, 0, 3);
        score += ScoreMultiplier(Waves.value, 10, 0, 2) * PlayerPrefs.GetInt("Upgrades", 1); 
        score += ScoreMultiplier(incrementStep.value,1,5,3) * PlayerPrefs.GetInt("Upgrades", 1);
        score += PlayerPrefs.GetInt("SpawnOnBreak", 0) * 1.5f;

        ScoreMulti = score;
    }
    void Update()
    {
        UpdateTest();
        UpdatePrefs();
        CalculateMulti(1f);




    }

}
