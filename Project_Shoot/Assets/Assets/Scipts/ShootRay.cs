using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
    public Transform Pos;

    // Start is called before the first frame update
    
    public Vector3 Shooter() {
        RaycastHit hit;
        Physics.Raycast(Pos.position, transform.TransformDirection(Vector3.forward), out hit,50f);
        return hit.point;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
