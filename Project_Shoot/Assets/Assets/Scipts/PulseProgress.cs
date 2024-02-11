using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PulseProgress : MonoBehaviour
{
    public RectTransform progressBar, fullBar;
    public Movement mov;
    public float ratioPulse;
    public GameObject Panel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (mov.willPulse == 0)
        {
            Panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ratioPulse = mov.TimeSincePulse / mov.PulseInterval;
        progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,ratioPulse * fullBar.rect.width);
    }
}
