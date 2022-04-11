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
    public float distToObj;

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
        _text.text = "dotprod: " + lookPercent.ToString("F3") + "\ndist: " + distToObj.ToString("F1");
    }
}
