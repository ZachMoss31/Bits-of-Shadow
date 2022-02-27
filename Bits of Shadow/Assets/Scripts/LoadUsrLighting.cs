using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUsrLighting : MonoBehaviour
{
    public SO_Lighting lightSO;
    public Light curLight;

    void Start()
    {
        SetLightColor(lightSO.UsrColor);
    }

    void SetLightColor(int lightNumber)
    {
        switch (lightNumber)
        {
            case 1:
                curLight.color = Color.red;
                break;
            case 2:
                curLight.color = Color.green;
                break;
            case 3:
                curLight.color = Color.magenta;
                break;
            case 4:
                curLight.color = Color.blue;
                break;
            case 0:
            default:
                curLight.color = Color.cyan;
                break;
        }
    }
}
