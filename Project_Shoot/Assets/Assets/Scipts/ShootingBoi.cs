using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShootingBoi : MonoBehaviour
{
    public VisualEffect  gunanimation;
    public float life;
    // Start is called before the first frame update
    void Start()
    {
        gunanimation.Play();
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life>1) {
            Destroy(this.gameObject);
        }
    }
}
