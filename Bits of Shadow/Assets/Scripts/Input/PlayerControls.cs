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
    private float _tmpSpeed;
    [SerializeField]
    private float sprintSpeed = 4.0f;
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
        _tmpSpeed = playerSpeed;
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
            _animator.SetBool("isSprinting", true);
        }

        else
        {
            _animator.SetBool("isSprinting", false);
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        //Jump Control (TO DO:: Add to method)
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //_animator.SetBool("isJumping", true);
            _animator.Play("PlayerJump");
        }

        //Interaction control (TO DO:: Add to method)
        if (interactControl.action.triggered)
        {
            if(_interactor.GetInteractableAmount() > 0)
            {
                //_interactor.Interact();
                _animator.Play("PlayerPhasePush");
                StartCoroutine(DelayedInteract());
            }
            else if(_interactor.GetClickableAmount() > 0)
            {
                _interactor.Interact();
                SetAllAnimations();

                _animator.SetBool("isInteracting", true);
                _animator.SetBool("isIdle", true);
                StartCoroutine(CutAnimation());
            }
        }

        //Escape control
        if (escapeControl.action.triggered)
        {
            _interactor.OptionsMenu();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
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

    IEnumerator DelayedInteract()
    {
        yield return new WaitForSeconds(.7f);
        _interactor.Interact();
    }

    IEnumerator CutAnimation()
    {
        yield return new WaitForSecondsRealtime(.3f);
        _animator.enabled = false;
        _animator.enabled = true;
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isRespawning", false);
        _animator.SetBool("isInteracting", false);
    }

    public void SetAllAnimations()
    {
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isSprinting", false);
        _animator.SetBool("isJumping", false);
        _animator.SetBool("isDead", false);
        _animator.SetBool("isInteracting", false);
    }

    public void StopPlayer()
    {
        playerSpeed = 0f;
        playerVelocity.y = 0f;
        gravityValue = 0f;
    }

    public void RestorePlayerValues()
    {
        playerSpeed = _tmpSpeed;
        gravityValue = -33f;
    }
}
