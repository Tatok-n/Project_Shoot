using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    
    public GameObject Player,ThisBoi;
    public Scoring ScoreBoi;

    public float MaxTime, LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ScoreBoi = Player.GetComponent<Scoring>();
        MaxTime = ScoreBoi.TargetLife;
        LifeTime = 0f;
    }

    public void DestroyTheBoi() {
        if (ScoreBoi.spawnNewTargetsOnBreak) {
            ScoreBoi.spawnTarget();
        } 
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime+= Time.deltaTime;
        if (LifeTime>= MaxTime) {
            ScoreBoi.missedTargets +=1;
            ScoreBoi.UpdateUI();
            DestroyTheBoi();
        }
    }
}
