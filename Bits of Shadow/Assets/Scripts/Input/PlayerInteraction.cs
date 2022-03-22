using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    #region Private Member Variables
    List<GameObject> _nearbyObjects;
    CinemachineVirtualCamera _playerCamera;
    GameObject _player;
    SphereCollider _playerInteractSphere;
    Vector3 _playerDir;
    Vector3 _playerCurDir;
    #endregion

    #region Public Member Variables
    public Transform player;
    public Transform camera;
    public bool showDotProd;
    public Canvas optionsMenu;
    #endregion

    //For Debug use...
    public List<DotProdTestable> testables;

    private void Awake()
    {
        optionsMenu.gameObject.SetActive(false);
        _player = this.gameObject;
        _playerCamera = this.GetComponentInChildren<CinemachineVirtualCamera>();
        _playerInteractSphere = this.GetComponent<SphereCollider>();
        _nearbyObjects = new List<GameObject>();
        _playerDir = _playerCamera.transform.position - _player.transform.position;
        _playerDir.Normalize();
    }

    private void FixedUpdate()
    {
        //This is the direction of the camera to the player...
        _playerDir = _playerCamera.transform.position - _player.transform.position;
        _playerDir.Normalize();
        //Debug.DrawLine(_playerCamera.transform.position, _player.transform.position, Color.white);
        if(showDotProd)
            ShowDotProd();
    }

    public void Interact()
    {
        Debug.Log("The Player interacted");
        foreach (var obj in _nearbyObjects)
        {
            //If the player is in look threshold logic, interaction can go here....
            //Could swap out camera.main with _playerCamera and see how that does...
            Vector3 camDir = Camera.main.transform.forward;
            Vector3 targObj = obj.transform.position - Camera.main.transform.position;

            var lookPercent = Vector3.Dot(camDir.normalized, targObj.normalized);
            float threshold = 0.97f;

            var interaction = obj.GetComponent<IInteractable>();
            if (interaction == null)
                continue;

            if(lookPercent > threshold)
            {
                interaction.Interact();
            }
        }
    }

    public void OptionsMenu()
    {
        //Add functionality to zoom in and NOT freeze timescale!
        if (!optionsMenu.isActiveAndEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            optionsMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            optionsMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    #region Triggering Events
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            _nearbyObjects.Add(other.gameObject);
            Debug.Log("Added a gameobject of name " + other.name);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            _nearbyObjects.Remove(other.gameObject);
            Debug.Log("Removed a gameobject of name " + other.name);
        }
    }
    #endregion

    #region Debug Options
    /// <summary>
    /// Shows the Dot Product of the Main Camera, relative to a list of objects
    /// Added to the player object manually to test.
    /// </summary>
    private void ShowDotProd()
    {
        for (int i = 0; i < testables.Count; i++)
        {
            Vector3 distToPlayer = testables[i].transform.position - _player.transform.position;
            Vector3 cameraDirection = Camera.main.transform.forward;
            Vector3 targetItem = testables[i].transform.position - Camera.main.transform.position;

            var lookPercent = Vector3.Dot(cameraDirection.normalized, targetItem.normalized);
            
            testables[i].lookPercent = lookPercent;
            testables[i].distToObj = distToPlayer.magnitude - 1.1f;
        }
    }

    /// <summary>
    /// Draws a vector from the camera to the player, and from the player forwards in direction of camera.
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector3 cameraDirection = player.transform.position - camera.transform.position;
        cameraDirection.Normalize();
        cameraDirection *= 2;
        Gizmos.DrawLine(player.transform.position, player.transform.position + cameraDirection);


        Vector3 cameraToPlayer = player.transform.position - camera.transform.position;
        cameraToPlayer.Normalize();
        Gizmos.DrawLine(camera.transform.position, camera.transform.position + cameraToPlayer);
    }
    #endregion
}
