using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour
{
    public int alphaVal = 0;

    Image _image;
    Animator _animator;
    float _fadeSpeed;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _animator = GetComponentInChildren<Animator>();
        _fadeSpeed = _animator.speed;
    }

    private void Start()
    {
        StartFade(0);
    }

    public void StartFade(int alpha)
    {
        StartCoroutine(Fading(alphaVal));
    }

    public void FadeOutAndBack()
    {
        StartCoroutine(FadeFull());
    }

    IEnumerator Fading(int alpha)
    {
        switch (alpha)
        {
            case 0:
                _animator.SetBool("Fade", true);
                yield return new WaitUntil(() => _image.color.a == 0);
                break;
            case 1:
                _animator.SetBool("Fade", false);
                yield return new WaitUntil(() => _image.color.a == 1);
                break;
        }
    }

    IEnumerator FadeFull()
    {
        float fullFadeSpeed = _fadeSpeed * 1.8f;
        _animator.speed = fullFadeSpeed;
        _animator.SetBool("Fade", false);
        yield return new WaitUntil(() => _image.color.a == 1);

        _animator.SetBool("Fade", true);
        yield return new WaitUntil(() => _image.color.a == 0);
        _animator.speed = _fadeSpeed;
    }
}
