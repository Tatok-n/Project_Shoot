using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject easyText, medText, hardText;

    public void Easy()
    {
        PlayerPrefs.SetInt("Diff",0);
    }
    public void Medium()
    {
        PlayerPrefs.SetInt("Diff", 1);
    }
    public void hard()
    {
        PlayerPrefs.SetInt("Diff", 2);
    }

    public void EasyTextOn()
    {
        easyText.SetActive(true);
    }
    public void EasyTextOff()
    {
        easyText.SetActive(false);
    }

    public void MedTextOn()
    {
        medText.SetActive(true);
    }
    public void MedTextOff()
    {
        medText.SetActive(false);
    }

    public void HardTextOn()
    {
        hardText.SetActive(true);
    }
    public void HardTextOff()
    {
        hardText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
