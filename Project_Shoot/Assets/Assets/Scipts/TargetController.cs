using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    
    public GameObject Player,ThisBoi;
    public Scoring ScoreBoi;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ScoreBoi = Player.GetComponent<Scoring>();
    }

    public void DestroyTheBoi() {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
