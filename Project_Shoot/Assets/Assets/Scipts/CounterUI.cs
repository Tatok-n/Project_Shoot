using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using System.Security.Cryptography;

public class CounterUI : MonoBehaviour
{
    public Scoring scoring;
    public List<TargetController> temp = new List<TargetController>();
    public GameObject CounterText;
    public Transform parent;
    public TextMeshProUGUI CounterTxt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.GetComponentsInChildren<Transform>())
        {
            if (this.GetComponent<Transform>().Equals(child)) { continue; }
            Destroy(child.gameObject);
        }
        scoring.targets.RemoveAll(x => x == null);
        int i = 0;
        foreach (TargetController tarcont in scoring.targets)
        { 
            CounterTxt = Instantiate(CounterText, parent).GetComponent<TextMeshProUGUI>();
            CounterTxt.text = "Target ";
            CounterTxt.text += i.ToString();
            CounterTxt.text += " : ";
            CounterTxt.text += Math.Round((tarcont.MaxTime-tarcont.LifeTime),1).ToString();
            i++;
        }



    }
}
