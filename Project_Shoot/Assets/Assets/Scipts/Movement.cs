using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float mvmtX, mvmtY,mvmtScale,mvmtProgress,mvmtSpeed;
    public bool isMoving,canMove,transition,isAiming,dashed;
    public Transform player,front,back,left,right,cam;
    public Vector3 Update,initialPos,NewPos,Forward,Right,Spawn,Dashpoint;
    public AnimationCurve Smoother;
    public PadController[] pads;
    public PadController dashpad;
    public GameObject PadContainer;
    public ShootRay shooter;
    
     void OnMove (InputValue movementValue)
    {
        
        Vector2 movementVector = movementValue.Get<Vector2>();
        isMoving = false;

        if (movementVector.magnitude != 0 && !(movementVector.x != 0 && movementVector.y != 0)) {
            isMoving = true;    
        }

        (mvmtX,mvmtY) = (movementVector.x,movementVector.y);

    }
    // Start is called before the first frame update
    void Start()
    {
        player.position = Spawn;
        Forward = Vector3.forward;
        Right = Vector3.right;
        pads = PadContainer.GetComponentsInChildren<PadController>();
        dashed = false;
        Dashpoint = new Vector3 (-1f,-1f,-1f);
        dashpad = pads[0];

    }

    void Allign() {
        float angle = cam.eulerAngles.y;
        if (angle<=45f || angle >= 315f) { //Looking original Forward
            Forward = Vector3.forward;
            Right = Vector3.right;
        } else if (angle >=45f && angle <= 135f) //looking original Right 
        {
            Forward = Vector3.right;
            Right = -Vector3.forward;
        } else if (angle >= 135f && angle <= 225) //looking original behind
        {
            Forward = -Vector3.forward;
            Right = -Vector3.right;
        } else {
            Forward = -Vector3.right;
            Right = Vector3.forward;
        }
    }

    void CheckBounds() {
        if  (player.position.x >= left.position.x - 1.6f && mvmtX == 1)
        {
            mvmtX = 0;
        } 
        else if  (player.position.x <= right.position.x + 1.6f && mvmtX == -1)
        {
            mvmtX = 0;
        }
        if   (player.position.z <= front.position.z + 1.6f && mvmtY == -1) 
        {
            mvmtY = 0;
        }
        else if    (player.position.z >= back.position.z - 1.6f && mvmtY == 1) 
        {
            mvmtY = 0;
        }
    }

    void MovePlayer() {
        if (mvmtProgress == 0) {
            Update = (mvmtX*Right + mvmtY*Forward);
            initialPos = player.position;
            mvmtProgress += 0.001f;
            transition = true;
        }  
        else if (mvmtProgress >= 1) 
        {
            mvmtProgress = 0;
            transition = false;
        }
        else{
            mvmtProgress += mvmtSpeed/60;
            NewPos = (Smoother.Evaluate(mvmtProgress)*Update*mvmtScale) + initialPos;
            player.position = NewPos;
        }
        
    }

    public bool CloseEnough(Transform Curr, Transform Target, float range) {
        Vector3 target = Target.position;
        Vector3 current = Curr.position;
        if (Vector3.Distance(target,current)<=range) {
            return true;
        }
        return false;
    }

    void CurrentSpot() {

        foreach (PadController padboi in pads) {
        
        if (CloseEnough(player,padboi.padTransform,1.5f)) {
            padboi.spot.intensity = 5000;
            
        } else if (!isAiming) {
            padboi.spot.intensity = 0;
        }
    } 
    }

    void OnDash(InputValue Button) {
         if (Button.Get<float>() == 0f) {
            isAiming = false;
            return;
         }
         isAiming = true;
    }
    void FixedUpdate()
    {
    
     Allign();
     if (isAiming) {
        float dist = 100f;
        Dashpoint = shooter.Shooter();
        dashpad.spot.intensity = 0;
        dashpad.spot.color = Color.white;
        foreach (PadController padboi in pads) {
            if (Vector3.Distance(padboi.padPos, Dashpoint) < dist) {
                dist = (Vector3.Distance(padboi.padPos, Dashpoint));
                dashpad = padboi;
            } else {
                padboi.spot.intensity = 0;
                padboi.spot.color = Color.white;
            }
        }
        dashpad.spot.intensity = 5000;
        dashpad.spot.color = Color.red;
     } else if (!isAiming && Dashpoint != new Vector3 (-1f,-1f,-1f)) {
        player.position = dashpad.padPos;
        dashpad.spot.color = Color.white;
        dashpad.spot.intensity = 0;
        Dashpoint = new Vector3 (-1f,-1f,-1f);
     }

     CurrentSpot();


     if ( transition || (canMove && isMoving ) ) {
        CheckBounds();
        MovePlayer();
        canMove = false;

     } else if (!isMoving) {
        canMove = true;
     }

    }
}
