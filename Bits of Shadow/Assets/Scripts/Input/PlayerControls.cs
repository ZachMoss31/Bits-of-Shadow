using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    //Added to involve sprinting
    [SerializeField]
    private InputActionReference sprintControl;
    [SerializeField]
    private InputActionReference jumpControl;
    //Added to involve interaction
    [SerializeField]
    private InputActionReference interactControl;
    //Q button optionality
    [SerializeField]
    private InputActionReference escapeControl;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float sprintSpeed = 3.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    private CharacterController controller;

    //Added to control animations
    private Animator _animator;
    
    //Added for interaction manager
    private PlayerInteraction _interactor;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Transform cameraMainTransform;

    private void OnEnable()
    {
        movementControl.action.Enable();
        sprintControl.action.Enable();
        jumpControl.action.Enable();
        interactControl.action.Enable();
        escapeControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        sprintControl.action.Disable();
        jumpControl.action.Disable();
        interactControl.action.Disable();
        escapeControl.action.Disable();
    }

    private void Start()
    {
        //Locking the main camera...
        Cursor.lockState = CursorLockMode.Locked;
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
        _interactor = gameObject.GetComponent<PlayerInteraction>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            _animator.SetBool("isJumping", false);
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;

        //Added the sprint control action and put the following into a if / else (was just controller.move outside of if.
        //Player Move / Sprint (TO DO:: Add to method)
        if (sprintControl.action.IsPressed())
        {
            controller.Move(move * Time.deltaTime * (playerSpeed + sprintSpeed));
           
        }

        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        //Jump Control (TO DO:: Add to method)
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            _animator.SetBool("isJumping", true);
        }

        //Interaction control (TO DO:: Add to method)
        if (interactControl.action.triggered)
        {
            _interactor.Interact();
        }

        //Escape control
        if (escapeControl.action.triggered)
        {
            _interactor.OptionsMenu();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }
}
