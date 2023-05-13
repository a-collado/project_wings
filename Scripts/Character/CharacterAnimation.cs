using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] private GameObject powerAnim;
    
    private Animator playerAnimator;
    
    private FasTPS.PlayerInput input;
    private FasTPS.CharacterMovement movement;

    private bool objectPicked;

    private PlayerSoundController playerSound;

    private Vector2 velocity;
    private Vector2 smoothDeltaPosition;

    private int LAYER_Carrying_OneHand = 2;
    
    public TwoBoneIKConstraint leftHand, rightHand;
    public Transform leftHandTarget, leftHandPicker, rightHandTarget, rightHandPicker;
    [Range(0f, 1f)]
    public float leftHandWeight, rightHandWeight;

    private bool _toZero = false;
    private bool _toOne = false;

/*
    private void OnEnable() {
        IInteractable._interaction += Interact();
    }

    private void OnDisable() {
        IInteractable._interaction -= Interact();

    }
*/

    private void Awake() {
        playerAnimator = gameObject.GetComponent<Animator>();
        //playerAnimator.applyRootMotion = true;
        input = GetComponentInParent<FasTPS.PlayerInput>();
        movement = GetComponent<FasTPS.CharacterMovement>();
        leftHandWeight = 0f;
        rightHandWeight = 0f;
    }

    private void Start() 
    {     
        objectPicked = false;
        //powerAnim.SetActive(true);
    }

    void Update()
    {
        if (!movement.MenuOpen) {
            powerAnim.SetActive(input.Power);
        }

        leftHand.weight = leftHandWeight;
    }

    public void Interact()
    {
        playerAnimator.SetTrigger("interact");
    }

    public void animateLocomotion(float inputMagnitude)
    {
        playerAnimator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
    }

    public void SetBool(string name, bool state)
    {
        playerAnimator.SetBool(name, state);
    }

    public Vector3 getDeltaPosition()
    {
        return playerAnimator.deltaPosition;
    }

    public void playAnimation(AnimationsEnum animation){
        
        switch(animation){
            case AnimationsEnum.NONE: break;
            case AnimationsEnum.GRAB_LOW: playerAnimator.SetTrigger("grabLow"); break; 
            case AnimationsEnum.PRESS_BTN: playerAnimator.SetTrigger("press"); break;
            case AnimationsEnum.GRAB_TORCH: playerAnimator.SetTrigger("pickTorch"); break;
        }
    }

    private void pickObject(bool b){
        objectPicked = b;
        playerAnimator.SetBool("objectPicked", b);
    }

    public void setLeftHandWeight(float weight)
    {
        leftHandWeight = weight;
    }




}
