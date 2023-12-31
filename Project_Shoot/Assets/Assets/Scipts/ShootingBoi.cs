using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShootingBoi : MonoBehaviour
{
    public float life,flashStr;
    public AnimationCurve lightStr;
    public Color lightcolor;
    public bool hasFlash;

    public Light flash;
    // Start is called before the first frame update
    void Start()
    {
        //gunanimation.Play();
        flash.color = lightcolor;
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        flash.intensity = lightStr.Evaluate(life) * flashStr;
        if (life>1) {
            Destroy(this.gameObject);
        }
    }
}
