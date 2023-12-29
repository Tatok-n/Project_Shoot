using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
    public Transform Pos;
    public TargetController TargetBoi;

    


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

            if (hit.collider.tag == "Targets") {
                TargetBoi = hit.collider.GetComponent<TargetController>();
                TargetBoi.DestroyTheBoi();
                return 100f;
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
