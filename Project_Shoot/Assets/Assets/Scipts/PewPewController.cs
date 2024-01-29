using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewController : MonoBehaviour
{
    public Transform thisboi;
    public Vector3 thisboipos, newpos, delta;

    public float front,right,speed,triggerRotation;

    public Scoring score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Player").GetComponent<Scoring>();
        front = GetComponentInParent<TurretController>().dirFront;
        right = GetComponentInParent<TurretController>().dirRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thisboipos = thisboi.position;                        // base projectile movement 
        newpos = new Vector3 (thisboipos.x + right*speed, thisboipos.y, thisboipos.z + front*speed);
        thisboi.position = newpos;

        delta = new Vector3((Mathf.Round((thisboipos.x - newpos.x) / speed)), 0f, Mathf.Round((thisboipos.z - newpos.z) / speed)); 
        
        
        if (delta ==new Vector3 (1f,0f, 0f))  //increasing X 
        {
            triggerRotation = 90;
        } else if (delta == new Vector3(-1f, 0f, 0f))
        {
            triggerRotation = 270; //decreasing X 
        } else if (delta == new Vector3(-1f, 0f, 0f)) { //increasing Z 
            triggerRotation = 0;
        }
        else //decreasing Z
        {
            triggerRotation = 180;
        }
           

    }

    void OnCollisionEnter(Collision collision) {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            score.GameOver();
        } else if (other.tag == "Assist") {
            if (other.GetComponent<AssistController>().isLeft && triggerRotation==270)
            other.GetComponent<AssistController>().triggered = true;
            else if (other.GetComponent<AssistController>().isRight && triggerRotation == 90)
                other.GetComponent<AssistController>().triggered = true;
            else if (other.GetComponent<AssistController>().isBack && triggerRotation == 180)
                other.GetComponent<AssistController>().triggered = true;
        } else if (!(other.tag == "Pads" || other.tag == "Targets" || other.tag == "PewPew")) {
            Destroy(this.gameObject);
        } 
    }
     
   
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Assist")
        {
            other.GetComponent<AssistController>().triggered = false;
        }
    }
}
