using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewController : MonoBehaviour
{
    public Transform thisboi;
    public Vector3 thisboipos, newpos;

    public float front,right,speed;

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
        thisboipos = thisboi.position;
        newpos = new Vector3 (thisboipos.x + right*speed, thisboipos.y, thisboipos.z + front*speed);
        thisboi.position = newpos;
    }

    void OnCollisionEnter(Collision collision) {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            score.GameOver();
        } else if (!(other.tag == "Pads" || other.tag =="Targets" || other.tag=="PewPew")) {
            Destroy(this.gameObject);
        }
    }
}
