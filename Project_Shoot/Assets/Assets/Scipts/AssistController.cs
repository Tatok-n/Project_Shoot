using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistController : MonoBehaviour
{
    public bool isLeft, isRight, isBack,triggered;
    public GameObject leftIm, rightIm, backIm;
    public float Rotation;
    public Movement boi;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClearOrder()
    {
        isLeft = false;
        isRight = false;
        isBack = false; 
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (triggered)
        {
            leftIm.SetActive(isLeft);
            rightIm.SetActive(isRight);
            backIm.SetActive(isBack);
            
        }
        else { 
            if (isLeft)
            {
                leftIm.SetActive(false);
            } else if (isRight)
            {
                rightIm.SetActive(false);
            } else if (isBack)
            {
                backIm.SetActive(false);    
            }
            
            
            
        }
    }
}
