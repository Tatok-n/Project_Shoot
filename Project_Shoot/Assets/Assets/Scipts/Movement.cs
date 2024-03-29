using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float mvmtX, mvmtY,mvmtScale,mvmtProgress,mvmtSpeed,groundval,jumpprog,jumpspeed,jumpscale,rotation,TimeSincePulse,PulseInterval,boundX, boundY;
    public bool isMoving,canMove,transition,isAiming,dashed,jump;
    public Transform player,front,back,left,right,cam;
    public Vector3 Update,initialPos,NewPos,Forward,Right,Spawn,Dashpoint;
    public AnimationCurve Smoother,JumpCurve;
    public PadController[] pads;
    public PadController dashpad;
    public GameObject PadContainer,PauseBoi,TurretContainer;
    public ShootRay shooter;
    public Color DashColor;
    public GunController gun;
    public TurretController[] turrets;
    public Pause PauseHandler;
    public int willPulse;
    public LevelGenerator lvlGen;

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
        turrets = TurretContainer.GetComponentsInChildren<TurretController>();
        TimeSincePulse = 0f;
        player.position = Spawn;
        Forward = Vector3.forward;
        Right = Vector3.right;
        pads = PadContainer.GetComponentsInChildren<PadController>();
        dashed = false;
        Dashpoint = new Vector3 (-1f,-1f,-1f);
        dashpad = pads[0];
        groundval = player.position.y;
        boundX = lvlGen.padSpacingHor-0.2f;
        boundY = lvlGen.padSpacingVert-0.2f;
        mvmtScale = lvlGen.padSpacingVert;

    }
    void OnPause(InputValue Button) {
        if (PauseBoi.activeInHierarchy)
        {
            PauseHandler.ExitPauseMenu();
        } else
        {
            PauseHandler.StartPauseMenu();
        }
        
        
    }
    void Allign() {
        float angle = cam.eulerAngles.y;
        if (angle<=45f || angle >= 315f) { //Looking original Forward
            Forward = Vector3.forward;
            Right = Vector3.right;
            rotation= 0f;  
            
        } else if (angle >=45f && angle <= 135f) //looking original Right 
        {
            Forward = Vector3.right;
            Right = -Vector3.forward;
            rotation= 90f;
            

        } else if (angle >= 135f && angle <= 225) //looking original behind
        {
            Forward = -Vector3.forward;
            Right = -Vector3.right;
            rotation = 180f;
        } else { //looking left
            Forward = -Vector3.right;
            Right = Vector3.forward;
            rotation = 270f;
        }
    }

    void CheckBounds() {
        float DirX = 0;
        float DirY = 0;
        if (rotation == 0f) {
            DirX = mvmtX;
            DirY = mvmtY;
        } else if (rotation == 90f) {
            DirX = mvmtY;
            DirY = -mvmtX;
        } else if (rotation == 180f) {
            DirX = -mvmtX;
            DirY = -mvmtY;
        } else {
            DirY = mvmtX;
            DirX = -mvmtY;
        }
        if  (player.position.x >= left.position.x - boundX && DirX == 1)
        {
            if (rotation == 0f || rotation == 180f) {
                mvmtX = 0;
            } else {
                mvmtY = 0;
            }
        } 
        else if  (player.position.x <= right.position.x + boundX && DirX == -1)
        {
            if (rotation == 0f || rotation == 180f) {
                mvmtX = 0;
            } else {
                mvmtY = 0;
            }
        }
        if   (player.position.z <= front.position.z + boundY && DirY == -1) 
        {
            if (rotation == 0f || rotation == 180f) {
                mvmtY = 0;
            } else {
                mvmtX = 0;
            }
        }
        else if    (player.position.z >= back.position.z - boundY && DirY == 1) 
        {
            if (rotation == 0f || rotation == 180f) {
                mvmtY = 0;
            } else {
                mvmtX = 0;
            }
        }
    }

    void MovePlayer() {
        if (mvmtProgress == 0) {
            Update = mvmtX*Right + mvmtY*Forward;
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

    public bool CloseEnough(Vector3 Curr, Vector3 Target, float range) {
        if (Vector3.Distance(Curr,Target)<=range) {
            return true;
        }
        return false;
    }

    void CurrentSpot() {

        foreach (PadController padboi in pads) {
        
        if (CloseEnough(player.position,padboi.padTransform.position,1.5f)) {
            padboi.spot.intensity = 5000;
            padboi.Used = true;
            
        } else if (!isAiming) {
            padboi.Used = false;

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

    void OnJump(InputValue jumpval) {
        if (jumpval.Get<float>() == 1f && player.position.y == groundval) {
            jump = true;
        }
    }

    void Jump() {
        if (jumpprog >= 1f) {
            jumpprog = 0f;
            jump = false;
            Vector3 Defpos = new Vector3 (0f,0f,0f);
            Defpos.x = player.position.x;
            Defpos.z = player.position.z;
            Defpos.y = groundval;
            player.position = Defpos;
            return;
        }
        Vector3 Jumpupdate = new Vector3 (0f,0f,0f);
        Jumpupdate.y = jumpscale*JumpCurve.Evaluate(jumpprog);
        jumpprog += jumpspeed/60;
        Jumpupdate.x = player.position.x;
        Jumpupdate.z = player.position.z;
        player.position = Jumpupdate;
    }
    
    void FixedUpdate() 
    {
    pads = PadContainer.GetComponentsInChildren<PadController>();
     Allign();
     if (isAiming) {
        float dist = 100f;
        Dashpoint = shooter.Shooter();
        dashpad.Used = false;
        foreach (PadController padboi in pads) {
            if (Vector3.Distance(padboi.padPos, Dashpoint) < dist) {
                dist = (Vector3.Distance(padboi.padPos, Dashpoint));
                dashpad = padboi;
            } else {
                padboi.Used = false;
            }
        }
        dashpad.Used = true;
        dashpad.spot.intensity = 5000;
        dashpad.spot.color = DashColor;
     } else if (!isAiming && Dashpoint != new Vector3 (-1f,-1f,-1f)) {
        player.position = dashpad.padPos;
        dashpad.Used = false;
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

     if (jump) {
        Jump();
     } else if (player.position.y != groundval) {
        player.position = new Vector3 (player.position.x, groundval, player.position.z);
        jump = false; 
     } else {
        jump = false;
     }

     if (mvmtX!= 0 || mvmtY!= 0) {
        gun.move = true;
        gun.MovingTime = 0;
        gun.Xmov = mvmtX;
        gun.ZMov = mvmtY;
     }

     if (gun.move == true) {
        gun.MoveAnimation();
     }

     TimeSincePulse += Time.deltaTime*willPulse;
     if (TimeSincePulse >= PulseInterval) {
        TimeSincePulse = 0f;
        int randpick = 0;
        System.Random rnd = new System.Random();
        randpick = rnd.Next(pads.Length);
        pads[randpick].Topulse = true;
        pads[randpick].pulseSound.Play();
        }

    }
}
