using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    GameObject _player;
    Rigidbody _curRb;
    public float force;
    public float upwardThrust = .5f;
    public void Interact()
    {
        Debug.Log("I got Interacted with.");
        if(_curRb != null)
        {
            //The direction of player to this obj...
            Vector3 forceDir = this.transform.position - _player.transform.position;
            forceDir.y = 0f;

            _curRb.AddForceAtPosition(forceDir * force, this.transform.position, ForceMode.Impulse);
            _curRb.AddForceAtPosition(upwardThrust * this.transform.up, this.transform.position, ForceMode.Impulse);
        }
    }

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curRb = this.GetComponent<Rigidbody>();
        force = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
