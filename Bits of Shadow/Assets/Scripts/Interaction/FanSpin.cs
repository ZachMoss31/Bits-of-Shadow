using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;

    void Update()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
