using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour, IInteractable
{
    public List <GameObject> platformList;
    public List<GameObject> indicatorMaterial;
    public List<GameObject> activatedLights;

    private bool _isActive = false;

    //For the indicator...
    public Material onMaterial;
    public Material offMaterial;
    public Material rgbMaterial;
    public Animator indicatorAnimator;
    public Light glowingLight;

    /// <summary>
    /// Interaction of turning on a platform controller module.
    /// </summary>
    public void Interact()
    {
        Debug.Log("I got interacted with.");
        if (!_isActive) {
            Debug.Log("Platforms turned on");
            indicatorAnimator.enabled = true;
            glowingLight.gameObject.SetActive(true);
            _isActive = true;
            foreach (var platform in platformList)
            {
                platform.GetComponent<PlatformHover>().enabled = true;
            }
            foreach (var indicator in indicatorMaterial)
            {
                var mat = indicator.gameObject.GetComponent<MeshRenderer>();
                mat.material = onMaterial;
            }
            foreach (var light in activatedLights)
            {
                var mat = light.gameObject.GetComponent<MeshRenderer>();
                mat.material = onMaterial;
            }
        }
        else
        {
            Debug.Log("Platforms turned off");
            indicatorAnimator.enabled = false;
            glowingLight.gameObject.SetActive(false);
            _isActive = false;
            foreach (var platform in platformList)
            {
                platform.GetComponent<PlatformHover>().enabled = false;
            }
            foreach (var indicator in indicatorMaterial)
            {
                var mat = indicator.gameObject.GetComponent<MeshRenderer>();
                mat.material = offMaterial;
            }
            foreach (var light in activatedLights)
            {
                var mat = light.gameObject.GetComponent<MeshRenderer>();
                mat.material = rgbMaterial;
            }
        }
    }       
}
