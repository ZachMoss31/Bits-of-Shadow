using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour, IInteractable, IClickable
{
    #region Lists of Linked Items
    /// <summary>
    /// A grouping of lists for the Platform Controller to be able access
    /// </summary>
    public List <GameObject> platformList;
    public List<GameObject> indicatorMaterial;
    public List<GameObject> activatedLights;
    #endregion

    #region Indicator Variables
    /// <summary>
    /// Materials to change and set for indicator ball / lighting
    /// </summary>
    public Material onMaterial;
    public Material offMaterial;
    public Material rgbMaterial;
    public Animator indicatorAnimator;
    public Light glowingLight;
    #endregion

    #region Private Members
    private bool _isActive = false;
    #endregion

    #region Public Members
    public Transform playerReposition;
    #endregion

    /// <summary>
    /// Interaction of turning on a platform controller module.
    /// </summary>
    public void Interact()
    {
        Debug.Log("I got interacted with.");
        PositionPlayer();
    } 

    public void ClickInteract()
    {
        Interact();
    }
    
    //Trying to forcefully move the player to the interaction button and face that way...
    private void PositionPlayer()
    {
        var player = GameObject.Find("Player");
        Vector3 dir = (transform.position - player.transform.position).normalized;
        var input = player.GetComponent<PlayerControls>();
        input.enabled = false;

        Vector3 rotation = Quaternion.LookRotation(dir).eulerAngles;
        rotation.x = rotation.z = 0;
        player.transform.rotation = Quaternion.Euler(rotation);
        StartCoroutine(ActivateController());
    }

    IEnumerator ActivateController()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (!_isActive)
        {
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

        //Give Player their control back
        var player = GameObject.Find("Player");
        var input = player.GetComponent<PlayerControls>();
        input.enabled = true;
    }
}
