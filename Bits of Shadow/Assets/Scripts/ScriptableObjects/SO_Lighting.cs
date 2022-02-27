using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Lighting : ScriptableObject
{
    //0 for default, 1 for red, etc...
    [SerializeField]
    private int _usrColor = 0;

    public int UsrColor
    {
        get { return _usrColor; }
        set { _usrColor = value; }
    }
}
