using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    // Estan los tres separados para que se vea bien en  el editor
    [Header ("Reference GameObjects")]
    [SerializeField] private InputActionReference moveWASD;
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference jump;

    [SerializeField] private Camera cam;

    [Header ("Movement Settings")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 5.0f;
    [SerializeField] private float jumpButtonGracePeriod = 0.2f;
    [SerializeField] private float jumpHorizontalSpeed = 3.0f;

    private Animator animator;
    private CharacterController controller;

    private float velocityY;

    // Jumping variables
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    // Jumping animation variables
    private bool isJumping;
    private bool isGrounded;


    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        animator.applyRootMotion = true;
        controller = gameObject.GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
    }
    

    private void Update() 
    {

        #region MOVEMENT_INPUT
        Vector2 moveInput = moveWASD.action.ReadValue<Vector2>();

        Vector3 movementDirection = new Vector3(moveInput.x, 0 , moveInput.y);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (run.action.ReadValue<float>() <= 0.0f) 
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        #endregion

        #region JUMP
        velocityY += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (jump.action.WasPressedThisFrame())
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
            velocityY = -0.5f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);
        

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                velocityY = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            controller.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && velocityY < 0) || velocityY < -2)
            {
                animator.SetBool("isFalling", true);
            }
        }

        #endregion

        #region MOVEMENT
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } 
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (!isGrounded)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = velocityY;

            controller.Move(velocity * Time.deltaTime);
        }

        #endregion

    }

    

    private void OnAnimatorMove() 
    {
        if (isGrounded)
        {
        Vector3 velocity = animator.deltaPosition; 
        velocity.y = velocityY * Time.deltaTime;

        controller.Move(velocity);
        }

    }
}
