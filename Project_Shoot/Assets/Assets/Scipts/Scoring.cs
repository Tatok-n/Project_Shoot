using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scoring : MonoBehaviour
{
    public ShootRay shooter;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
    }
    void OnFire() {
        score += shooter.Fire();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
