using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraMover : MonoBehaviour
{
    public Transform mainCamera;

    public Transform tempCamera;

    [Header("Audio Settings")]
    public AudioSource mainAudio;
    public AudioClip backClick;
    public AudioClip forwardClick;

    public float camSpeed = .1f;

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

    public Canvas patchNotesCanvas;
    public Button patchNotesOpen;
    public Button patchNotesClose;


    void Start()
    {
        mainCamera = Camera.main.gameObject.transform;
        CloseEachCanvas();
        //rgbSetColor.SetColor("_EmissionColor", Color.cyan * 7);
        //rgbSetLight.color = Color.cyan;
        //lightSO.UsrColor = 0;
    }

    public void ZoomBackOut()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, tempCamera.position, 1f);
        mainCamera.transform.rotation = tempCamera.rotation;
        EnableMainButtons();
        CloseEachCanvas();

        //Audio Methods
        mainAudio.clip = backClick;
        mainAudio.pitch = .5f;
        mainAudio.Play();
    }

    public void OptionsZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, optionsHalt.position, 1f);
        mainCamera.transform.rotation = optionsHalt.rotation;
        optionsCanvas.gameObject.SetActive(true);

        DisableMainButtons();

        //Audio Methods
        mainAudio.clip = forwardClick;
        mainAudio.pitch = 1f;
        mainAudio.Play();
    }

    public void OptionsBack()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, tempCamera.position, 1f);
        mainCamera.transform.rotation = tempCamera.rotation;
        optionsCanvas.gameObject.SetActive(false);
        EnableMainButtons();

        //Audio Methods
        mainAudio.clip = backClick;
        mainAudio.pitch = .5f;
        mainAudio.Play();
    }

    public void PlayZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, playHalt.position, 1f);
        mainCamera.transform.rotation = playHalt.rotation;
        playCanvas.gameObject.SetActive(true);

        DisableMainButtons();

        //Audio Methods
        mainAudio.clip = forwardClick;
        mainAudio.pitch = 1f;
        mainAudio.Play();
    }

    public void QuitZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, quitHalt.position, 1f);
        mainCamera.transform.rotation = quitHalt.rotation;
        quitCanvas.gameObject.SetActive(true);

        DisableMainButtons();

        //Audio Methods
        mainAudio.clip = forwardClick;
        mainAudio.pitch = 1f;
        mainAudio.Play();
    }

    public void MultiplayerZoomIn()
    {
        mainCamera.transform.position = Vector3.Lerp(transform.position, multiplayerHalt.position, 1f);
        mainCamera.transform.rotation = multiplayerHalt.rotation;
        multiplayerCanvas.gameObject.SetActive(true);

        DisableMainButtons();

        //Audio Methods
        mainAudio.clip = forwardClick;
        mainAudio.pitch = 1f;
        mainAudio.Play();
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
        patchNotesCanvas.gameObject.SetActive(false);
    }

    public void OpenPatchNotes()
    {
        patchNotesCanvas.gameObject.SetActive(true);
        patchNotesOpen.gameObject.SetActive(false);
        patchNotesClose.gameObject.SetActive(true);
    }

    public void ClosePatchNotes()
    {
        patchNotesCanvas.gameObject.SetActive(false);
        patchNotesOpen.gameObject.SetActive(true);
        patchNotesClose.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}