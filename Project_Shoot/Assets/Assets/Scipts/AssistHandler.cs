using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistHandler : MonoBehaviour
{
    public float Rotation;
    public Movement boi;
    public AssistController rightAss, leftAss, backAss, frontAss;
    public AssistController[] Asslist;


    // Start is called before the first frame update
    void Awake()
    {
        AssistController[] Asslists = {rightAss, leftAss, frontAss, backAss};
        Asslist = Asslists;

    }

    // Update is called once per frame
    void Update()
    {
        Rotation = boi.rotation;
        foreach (AssistController Assist in Asslist)
        {
            Assist.ClearOrder();
        }
        switch (Rotation)
        {
            case 0f: //is looking front
                rightAss.isRight = true;
                leftAss.isLeft = true;
                backAss.isBack = true;
                break;
            case 90f: //is looking right
                frontAss.isLeft = true;
                leftAss.isBack = true;
                backAss.isRight = true;
                break;
            case 180f: //is looking back
                frontAss.isBack = true;
                rightAss.isLeft = true;
                leftAss.isRight = true;
                break;
            case 270f: //is looking left
                backAss.isLeft = true;
                frontAss.isRight = true;
                rightAss.isBack = true;
                break;
        }
    }
}
