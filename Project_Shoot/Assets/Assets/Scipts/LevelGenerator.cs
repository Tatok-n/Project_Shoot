using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Material FloorMat;
    public Transform FloorTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FloorMat.SetFloat("_ScaleX", FloorTransform.localScale.x);
        FloorMat.SetFloat("_ScaleY", FloorTransform.localScale.y);
    }
}
