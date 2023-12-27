using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float mvmtX, mvmtY,mvmtScale,mvmtProgress,mvmtSpeed;
    public bool isMoving,canMove,transition;
    public Transform player,front,back,left,right;

    public Vector3 Update,initialPos,NewPos;
    public AnimationCurve Smoother;
    
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
        player.position = Vector3.zero;
    }

    void CheckBounds() {
        if  (player.position.x >= left.position.x - 0.5f && mvmtX == 1)
        {
            mvmtX = 0;
        } 
        else if  (player.position.x <= right.position.x + 0.5f && mvmtX == -1)
        {
            mvmtX = 0;
        }
        else if   (player.position.z <= front.position.z - 0.5f && mvmtY == -1) 
        {
            mvmtY = 0;
        }
        else if    (player.position.z >= back.position.z + 0.5f && mvmtY == 1) 
        {
            mvmtY = 0;
        }
    }

    void MovePlayer() {
        if (mvmtProgress == 0) {
            Update = (mvmtX*Vector3.right + mvmtY*Vector3.forward);
            initialPos = player.position;
            mvmtProgress += 0.001f;
            transition = true;
        } else if (mvmtProgress>1) {
            mvmtProgress = 0;
            transition = false;
        } 
        else {
            NewPos = (Smoother.Evaluate(mvmtProgress)*Update*mvmtScale) + initialPos;
            mvmtProgress += mvmtSpeed/60;
            player.position = NewPos;
        }
    }
    void FixedUpdate()
    {
     if ( transition || (canMove && isMoving ) ) {
        CheckBounds();
        MovePlayer();
        canMove = false;

     } else if (!isMoving) {
        canMove = true;
     }
    }
}
