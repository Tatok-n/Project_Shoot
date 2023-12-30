using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
    
{

    public Movement player;
    public AnimationCurve ShootingCurve,MovingCurve;
    public bool fire;
    public Transform gun,cam;
    public Vector3 originalPos,originalRot, newPos,newRot,offset;
    public float ShootingTime,ShootingSpeed,ShootingScale,rotScale,MovingTime,MovingSpeed,MovingScale;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = gun.position;
        originalRot= gun.localEulerAngles;
        offset = gun.localPosition - cam.localPosition;
    }
    void FireAnimation() {
        
        newRot = new Vector3 (originalRot.x + ShootingCurve.Evaluate(ShootingTime)*rotScale, originalRot.y  , originalRot.z);
        newPos = new Vector3 ((cam.localPosition + offset).x, (cam.localPosition + offset).y + ShootingCurve.Evaluate(ShootingTime)*ShootingScale, (cam.localPosition + offset).z);
        gun.transform.localPosition = newPos;
        gun.transform.localEulerAngles = newRot;
        if (ShootingTime < 1f) {
            ShootingTime += ShootingSpeed/60;
        } else {
            fire = false;
            ShootingTime = 0f;
            gun.localPosition = cam.localPosition + offset;
            gun.localEulerAngles = originalRot;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (fire) {
            FireAnimation();
        }
    }
}
