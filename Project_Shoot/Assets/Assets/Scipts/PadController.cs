using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public Light spot;
    public Transform padTransform;
    public Vector3 padPos;
    // Start is called before the first frame update
    void Start()
    {
        padPos = padTransform.position;
        spot.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
