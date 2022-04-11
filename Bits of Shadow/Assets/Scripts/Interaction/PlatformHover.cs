using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHover : MonoBehaviour
{
    public bool canHover;
    public float hoverSpeed = 2f;
    public bool indexOverride = false;
    public int indexStartOverride = 0;
    public GameObject platformWaypointsArray;
    Transform[] hoverTargets;
    Vector3 curTarget;
    int _targIndex = 0;

    /// <summary>
    /// Fills the array of targets and sets override index if desired.
    /// </summary>
    private void Awake()
    {
        hoverTargets = new Transform[platformWaypointsArray.transform.childCount];
        for (int i = 0; i < hoverTargets.Length; i++)
        {
            hoverTargets[i] = platformWaypointsArray.transform.GetChild(i);
        }

        if (indexOverride)
        {
            _targIndex = indexStartOverride;
        }
        curTarget = hoverTargets[_targIndex].position;
        Debug.Log("Amount of targets is " + hoverTargets.Length);
        Debug.Log("Aimed at " + _targIndex);
    }

    /// <summary>
    /// Will call HoverPlatform() to modify the platform's position chasing a target
    /// </summary>
    void FixedUpdate()
    {
        if (canHover)
        {
            HoverPlatform();
        }
    }


    /// <summary>
    /// Chases a target's transform as well as modify the current target
    /// </summary>
    void HoverPlatform()
    {
        if (Vector3.Distance(transform.position, curTarget) <= 0.3f)
        {
            if(_targIndex >= hoverTargets.Length - 1)
            {
                _targIndex = 0;
                curTarget = hoverTargets[_targIndex].position;
            }
            else
            {
                curTarget = hoverTargets[++_targIndex].position;
            }
        }
        Vector3 dir = curTarget - transform.position;
        transform.Translate(dir.normalized * hoverSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player has found the platform.");
            collision.gameObject.transform.SetParent(this.transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player left platform");
        other.gameObject.transform.SetParent(null, true);
    }
}
