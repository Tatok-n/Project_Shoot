using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
using System;



public class SettingsSetter : MonoBehaviour
{  
    public VolumeProfile  volumes;
    public FirstPersonCameraRotation cameracont;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
       
        SetSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPresets() {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Preset",1)+1, true);
    }

    void SetSSR(int setValue) {
        ScreenSpaceReflection ssr;         
        if (volumes.TryGet(out ssr)) {
            if (setValue == 0) {
            ssr.active = true; // Enable HDRP SSR
            ssr.tracing.value = RayCastingMode.RayTracing; //Sets RTX
            } else if (setValue == 1) {
            ssr.active = true;
            ssr.tracing.value = RayCastingMode.RayMarching;
            ssr.rayMaxIterations = 64;
            } else if (setValue == 2) {
            ssr.active = true;
            ssr.tracing.value = RayCastingMode.RayMarching;
            ssr.rayMaxIterations= 32;
            } else if (setValue == 3) {
            ssr.active = true;
            ssr.tracing.value = RayCastingMode.RayMarching;
            ssr.rayMaxIterations = 16;
            } else if (setValue == 4) {
            ssr.active = false;
            }
        }
    }

    void SetGI(int setvalue) {
        GlobalIllumination ssgi; 
        if (volumes.TryGet(out ssgi)) {
            if (setvalue == 0) {
            ssgi.active = true; 
            ssgi.tracing.value = RayCastingMode.RayTracing; 
        } else if (setvalue == 1) {
            ssgi.active = true;
            ssgi.tracing.value = RayCastingMode.RayMarching;
            ssgi.maxRaySteps= 128; 
        } else if (setvalue == 2 ) {
            ssgi.active = true;
            ssgi.tracing.value = RayCastingMode.RayMarching;
            ssgi.maxRaySteps= 64; 
        } else if (setvalue == 3 ) {
            ssgi.active = true;
            ssgi.tracing.value = RayCastingMode.RayMarching;
            ssgi.maxRaySteps= 32; 
        } else if (setvalue == 4 ) {
            ssgi.active = false;
        }
        }
    }
    
    void setAO(int SetValue) {

        ScreenSpaceAmbientOcclusion AO; 
        if (volumes.TryGet(out AO)) {
            if (SetValue == 0) {
                AO.active = true;
                AO.rayTracing.value = true;
                AO.intensity.value = 3f;
            } else if (SetValue == 1) {
                AO.active = true;
                AO.rayTracing.value = false;
                AO.intensity.value = 2f;
                AO.maximumRadiusInPixels = 20;
                AO.stepCount = 8;
            } else if (SetValue == 2 ) {
                AO.active = true;
                AO.rayTracing.value = false;
                AO.intensity.value = 2f;
                AO.maximumRadiusInPixels = 10;
                AO.stepCount = 4;
            } else if (SetValue == 3) {
                AO.active = true;
                AO.rayTracing.value = false;
                AO.intensity.value = 2f;
                AO.maximumRadiusInPixels = 5;
                AO.stepCount = 2;
            } else if (SetValue == 1) {
                AO.active = false;
                
            }
            
        }
    }

    void SetBloom(float parameters) {
        Bloom bloom; 
        if (volumes.TryGet(out bloom)) { 
            bloom.intensity.value = parameters;
        }
    }

   
    void SetChroma(int param) {
        ChromaticAberration chroma;
        if (volumes.TryGet(out chroma)) {
            if (param == 1) {
                chroma.active = true;
            } else {
                chroma.active = false;
            }
        }
    }

    public void SetSettings() {
        cameracont.Sensitivity = PlayerPrefs.GetFloat("Sens",2f);
        SetPresets();
        SetSSR(PlayerPrefs.GetInt("SSR", 2));
        SetGI(PlayerPrefs.GetInt("GI", 2));
        setAO(PlayerPrefs.GetInt(("AO"),2));
        SetBloom(PlayerPrefs.GetFloat(("Bloom"),0.5f));
        SetChroma(PlayerPrefs.GetInt(("Chroma"),1));

    }
}
