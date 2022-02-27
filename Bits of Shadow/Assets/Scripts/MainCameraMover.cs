using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraMover : MonoBehaviour
{
    public Transform mainCamera;

    public Transform tempCamera;

    public Material rgbSetColor;
    public Light rgbSetLight;
    public SO_Lighting lightSO;

    public Transform optionsHalt;
    public Button optionsButton;
    public Canvas optionsCanvas;

    public Transform playHalt;
    public Button playButton;
    public Canvas playCanvas;

    public Transform quitHalt;
    public Button quitButton;
    public Canvas quitCanvas;

    public Transform multiplayerHalt;
    public Button multiplayerButton;
    public Canvas multiplayerCanvas;


    void Start()
    {
        mainCamera = Camera.main.gameObject.transform;
        CloseEachCanvas();
        rgbSetColor.SetColor("_EmissionColor", Color.cyan * 7);
        rgbSetLight.color = Color.cyan;
        lightSO.UsrColor = 0;
    }

    public void ZoomBackOut()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, tempCamera.position, 1f);
        mainCamera.transform.rotation = tempCamera.rotation;
        EnableMainButtons();
        CloseEachCanvas();
    }

    public void OptionsZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, optionsHalt.position, 1f);
        mainCamera.transform.rotation = optionsHalt.rotation;
        optionsCanvas.gameObject.SetActive(true);

        DisableMainButtons();
    }

    public void OptionsBack()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, tempCamera.position, 1f);
        mainCamera.transform.rotation = tempCamera.rotation;
        optionsCanvas.gameObject.SetActive(false);

        EnableMainButtons();
    }

    public void PlayZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, playHalt.position, 1f);
        mainCamera.transform.rotation = playHalt.rotation;
        playCanvas.gameObject.SetActive(true);

        DisableMainButtons();
    }

    public void QuitZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, quitHalt.position, 1f);
        mainCamera.transform.rotation = quitHalt.rotation;
        quitCanvas.gameObject.SetActive(true);

        DisableMainButtons();
    }

    public void MultiplayerZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, multiplayerHalt.position, 1f);
        mainCamera.transform.rotation = multiplayerHalt.rotation;
        multiplayerCanvas.gameObject.SetActive(true);

        DisableMainButtons();
    }

    public void DisableMainButtons()
    {
        optionsButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        multiplayerButton.gameObject.SetActive(false);
    }

    public void EnableMainButtons()
    {
        optionsButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        multiplayerButton.gameObject.SetActive(true);
    }

    public void CloseEachCanvas()
    {
        optionsCanvas.gameObject.SetActive(false);
        playCanvas.gameObject.SetActive(false);
        quitCanvas.gameObject.SetActive(false);
        multiplayerCanvas.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}