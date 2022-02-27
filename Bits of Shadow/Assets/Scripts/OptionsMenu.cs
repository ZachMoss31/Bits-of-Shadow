using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Material rgbColor;
    public Light rgbLight;
    public SO_Lighting lightSO;

    private void Start()
    {
        rgbColor.SetColor("_EmissionColor", Color.cyan * 8);
        rgbLight.color = Color.cyan;
    }

    public void ChangeLight(int usrSelectedLight)
    {
        switch (usrSelectedLight)
        {
            case 1:
                rgbColor.SetColor("_EmissionColor", Color.red * 8);
                rgbLight.color = Color.red;
                lightSO.UsrColor = 1;
                break;
            case 2:
                rgbColor.SetColor("_EmissionColor", Color.green * 8);
                rgbLight.color = Color.green;
                lightSO.UsrColor = 2;
                break;
            case 3:
                rgbColor.SetColor("_EmissionColor", Color.magenta * 8);
                rgbLight.color = Color.magenta;
                lightSO.UsrColor = 3;
                break;
            case 4:
                rgbColor.SetColor("_EmissionColor", Color.blue * 9);
                rgbLight.color = Color.blue;
                lightSO.UsrColor = 4;
                break;
            case 0:
            default:
                rgbColor.SetColor("_EmissionColor", Color.cyan * 7);
                rgbLight.color = Color.cyan;
                lightSO.UsrColor = 0;
                break;
        }
    }

}
