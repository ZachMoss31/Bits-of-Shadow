using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitStreamBus : MonoBehaviour, IInteractable
{
    GameObject _player;
    public GameObject char1;
    public GameObject char2;

    public void Interact()
    {
        Debug.Log("Changed my color!");
        char1.gameObject.SetActive(false);
        char2.gameObject.SetActive(true);
    }

    void Awake()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
