using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PadController : MonoBehaviour
{
    public Light spot;
    public float Life,Speed,Strength;
    public Transform padTransform,frontemitter,leftemitter,rightemitter,backemitter;
    public Vector3 padPos;
    public bool Used,recFront,recBack,recRight,recLeft,Topulse,touchingPlayer;
    public Color ActionColor;
    public AnimationCurve ActionCurve;
    public PadController front,back,left,right;

    public Movement mov;

    public Scoring score;

    public VisualEffect pulsefx;
    // Start is called before the first frame update
    void Start()
    {
        padPos = padTransform.position;
        spot.intensity = 0f;

        RaycastHit hitright;
        if (Physics.Raycast(rightemitter.position, transform.TransformDirection(Vector3.right) , out hitright, 100f))
        {

            if (hitright.collider.tag == "Pads") {
                right = hitright.collider.GetComponent<PadController>();
            }
            

        }

        RaycastHit hitleft;
        if (Physics.Raycast(leftemitter.position, transform.TransformDirection(Vector3.left) , out hitleft, 100f))
        {

            if (hitleft.collider.tag == "Pads") {
                left = hitleft.collider.GetComponent<PadController>();
            }
            

        }

        RaycastHit hitfront;
        if (Physics.Raycast(frontemitter.position, transform.TransformDirection(Vector3.forward) , out hitfront, 100f))
        {

            if (hitfront.collider.tag == "Pads") {
                front = hitfront.collider.GetComponent<PadController>();
            }
            

        }

        RaycastHit hitback;
        if (Physics.Raycast(backemitter.position, transform.TransformDirection(-Vector3.forward) , out hitback, 100f))
        {

            if (hitback.collider.tag == "Pads") {
                back = hitback.collider.GetComponent<PadController>();
            }
            

        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            touchingPlayer = true;
            score = other.GetComponentInParent<Scoring>();
            mov = other.GetComponentInParent<Movement>();
            
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && !mov.CloseEnough(padPos, new Vector3 (mov.player.position.x,mov.groundval,mov.player.position.z), 1f)) {
            touchingPlayer = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    
        if (!Used) {
            spot.intensity = 0f;
            spot.color = Color.white;
        }
        if (Topulse) {
            Pulse();
        }
    }

    void Pulse() {
        Used = true;
        spot.color = ActionColor;
        Life += Speed*Time.deltaTime;
        spot.intensity = Strength*ActionCurve.Evaluate(Life);
        if (Life>= 1.5f) {
            Topulse = false;
            Used = false;
            Life = 0f;
            recBack = false;
            recFront = false;
            recLeft = false;
            recRight = false;
            pulsefx.Play();
            if (touchingPlayer) {
                score.GameOver();
            }
        } else if (Life >=0.8f) {
            if (!recFront && front!= null) {
                front.Topulse = true;
                front.recBack = true;
                recFront = true;
            }
            if (!recBack && back!= null) {
                back.Topulse = true;
                back.recFront = true;
                recBack = true;
            }
            if (!recLeft && left!= null) {
                left.recRight = true;
                left.Topulse = true;
                recLeft = true;
            }
            if (!recRight && right != null) {
                right.recLeft = true;
                right.Topulse = true;
                recRight = true;
            }

        } 

    }
}
