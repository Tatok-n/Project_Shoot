using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class SettingsHandler : MonoBehaviour
{
    public TMP_Dropdown PerformancePreset, SSR,SSSAO,GI;
    public Slider Bloom,Sens;
    public Toggle Chroma;

    public GameObject Settings,PreviousMenu;
    public TextMeshProUGUI BloomUI,SensUI;
    
    // Start is called before the first frame update

   
   public void SetAOPref() {
    PlayerPrefs.SetInt("AO", SSSAO.value);
   }

   public void SetOverallPref() {
    PlayerPrefs.SetInt("Preset", PerformancePreset.value);
   }
    public void SetSens() {
        PlayerPrefs.SetFloat("Sens",Sens.value);
    }

    public void SetBloom () {
        PlayerPrefs.SetFloat("Bloom", Bloom.value);
    }
    public void SetSSR () {
        PlayerPrefs.SetInt("SSR", SSR.value);
    }

    public void GlobalIllumi () {
        PlayerPrefs.SetInt("GI", GI.value);
    }

    public void SetChroma() {
        if (Chroma.isOn) {
        PlayerPrefs.SetInt("Chroma", 1);
        } else {
        PlayerPrefs.SetInt("Chroma", 0);
        }
        
    }

     public void GetBloomDisplay() {
        BloomUI.text = Math.Round(PlayerPrefs.GetFloat(("Bloom"),0.15f),2).ToString();
    }

    public void GetSensDisplay() {
        SensUI.text = Math.Round(PlayerPrefs.GetFloat(("Sens"),0.15f),2).ToString();
    }

    public void InitiateMenus() {
        Settings.SetActive(true);
        PreviousMenu.SetActive(false);
        PerformancePreset.value = PlayerPrefs.GetInt("Preset", 1);
        SSR.value = PlayerPrefs.GetInt("SSR", 0);
        GI.value = PlayerPrefs.GetInt("GI", 3);
        SSSAO.value =  PlayerPrefs.GetInt("AO",2);
        Bloom.value = PlayerPrefs.GetFloat("Bloom", 0.69f);
        Sens.value = PlayerPrefs.GetFloat("Sens", 0.11f);
        if (PlayerPrefs.GetInt("Chroma",1) == 1) {
            Chroma.isOn = true;
        } else {
            Chroma.isOn = false;
        }

    }

    public void saveValues() {
        PlayerPrefs.SetInt("Preset", PerformancePreset.value);
        PlayerPrefs.SetInt("SSR", SSR.value);
        PlayerPrefs.SetInt("GI", GI.value);
        PlayerPrefs.SetInt("AO",SSSAO.value);
        PlayerPrefs.SetFloat("Bloom", Bloom.value);
        PlayerPrefs.SetFloat("Sens", Sens.value);
        if (Chroma.isOn) {
            PlayerPrefs.SetInt("Chroma",1);
            
        } else {
            PlayerPrefs.SetInt("Chroma",0);
        }
    }
}
