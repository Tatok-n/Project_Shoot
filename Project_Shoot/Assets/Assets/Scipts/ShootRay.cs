using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
    public Transform Pos;
    public TargetController TargetBoi;
    public Scoring ScoreBoi;

    public GameObject explosion;



    


    // Start is called before the first frame update
    
    public Vector3 Shooter() {
        RaycastHit hit;
        Physics.Raycast(Pos.position, transform.TransformDirection(Vector3.forward), out hit);
        return hit.point;
    }
    
    public float Fire() {
        RaycastHit hit;
        if (Physics.Raycast(Pos.position, transform.TransformDirection(Vector3.forward) , out hit, 100f))
        {   
            Instantiate(explosion, hit.point, Pos.rotation);
            if (hit.collider.tag == "Targets") {
                float points = 0f;
                TargetBoi = hit.collider.GetComponent<TargetController>();
                points  = (ScoreBoi.ScoreCurve.Evaluate(TargetBoi.LifeTime/TargetBoi.MaxTime))*ScoreBoi.TargetPoints;
                TargetBoi.DestroyTheBoi();
                return points;
            }
            

        }

        return 0f;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
