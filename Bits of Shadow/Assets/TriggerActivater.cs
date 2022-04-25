using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivater : MonoBehaviour
{
    PlatformHover _hoverControl;
    private void Awake()
    {
        _hoverControl = GetComponent<PlatformHover>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _hoverControl.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _hoverControl.enabled = false;
        }
    }
}
