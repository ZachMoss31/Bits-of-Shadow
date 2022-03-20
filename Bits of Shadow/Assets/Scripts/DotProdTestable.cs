using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DotProdTestable : MonoBehaviour
{
    [SerializeField]
    TextMeshPro _text;
    GameObject _player;
    bool _showDotProd;

    public float lookPercent;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _showDotProd = _player.GetComponent<PlayerInteraction>().showDotProd;
        if(!_showDotProd)
        {
            _text.gameObject.SetActive(false);
        }
        else
        {
            _text.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        _text.text = lookPercent.ToString("F3");
    }
}
