using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public Light spot;
    public Transform padTransform;
    public Vector3 padPos;
    public bool Used;
    // Start is called before the first frame update
    void Start()
    {
        padPos = padTransform.position;
        spot.intensity = 0f;
    }
    void OnTriggerEnter(Collider collision)
    {
        //EndGame
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Used) {
            spot.intensity = 0f;
            spot.color = Color.white;
        }
    }
}
