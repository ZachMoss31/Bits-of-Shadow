using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitStreamBus : MonoBehaviour, IInteractable
{
    Material _material;
    public void Interact()
    {
        Debug.Log("Changed my color!");
        _material.color = Color.white;
    }

    void Awake()
    {
        _material = this.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
