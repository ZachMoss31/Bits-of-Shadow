using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    //------------------------------ TO DO::
    // Implement a way to have text triggers pass strings or even button prompts to this runner and display them
    // Clean the Enter/Exit triggering to not leave UI on the screen
    
    //Get the main canvas object to sent texts...
    public CanvasGroup _mainCanvas;
    public TextMeshProUGUI _promptText;

    //This will never clear the text.
    public bool exitTrigger = false;
    public bool keyClear = false;
    public int keysToCollect = 3;
    public string textToDisplay;

    int _keysAmount = 0;

    private void Awake()
    {
        _mainCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        _mainCanvas.gameObject.SetActive(false);
        _promptText.text = textToDisplay;
        _keysAmount = FindObjectOfType<GameplayManager>().keys;
    }

    private void OnTriggerStay(Collider other)
    {
        _keysAmount = FindObjectOfType<GameplayManager>().keys;
        if (keyClear && _keysAmount == keysToCollect)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            _mainCanvas.gameObject.SetActive(true);
            _promptText.text = textToDisplay;
            StopCoroutine(FadeOut());
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _keysAmount = FindObjectOfType<GameplayManager>().keys;
        if (keyClear && _keysAmount == keysToCollect)
        {
            return;
        }

        if (exitTrigger)
            return;

        if (other.CompareTag("Player"))
            StopCoroutine(FadeIn());
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        while (_mainCanvas.alpha < 1.0f)
        {
            _mainCanvas.alpha += Time.deltaTime * 2;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (_mainCanvas.alpha > 0.0f)
        {
            _mainCanvas.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        _mainCanvas.gameObject.SetActive(false);
    }
}
