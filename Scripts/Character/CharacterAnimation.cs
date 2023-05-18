using System;
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
    private MeshCollider powerCollider;

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

    [SerializeField] private bool _toOne = false;
    [SerializeField] private bool _toOneRight = false;

    [Header("Power Particles")] [SerializeField]
    private ParticleSystem powerParticles;


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
        powerCollider = powerAnim.GetComponent<MeshCollider>();
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
            if(input.Power && !powerParticles.IsAlive(true)) powerParticles.Play(true);
            if(!input.Power && powerParticles.IsAlive(true)) powerParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        leftHand.weight = leftHandWeight;
        rightHand.weight = rightHandWeight;

        if (_toOne && leftHandWeight <= 1) leftHandWeight += (Time.deltaTime);
        else if (leftHandWeight >= 0) leftHandWeight -= (Time.deltaTime);
        
        if (_toOneRight && rightHandWeight <= 1) rightHandWeight += (Time.deltaTime);
        else if (rightHandWeight >= 0) rightHandWeight -= (Time.deltaTime);

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
            case AnimationsEnum.GRAB_LOW: playerAnimator.SetTrigger("pickFeather"); break; 
            case AnimationsEnum.PRESS_BTN: playerAnimator.SetTrigger("press"); break;
            case AnimationsEnum.GRAB_TORCH: playerAnimator.SetTrigger("pickTorch"); break;
            case AnimationsEnum.DROP_ONE_MID: playerAnimator.SetTrigger("dropTorch"); break;
            case AnimationsEnum.GRAB_HIGH: playerAnimator.SetTrigger("pickHigh"); break;
            case AnimationsEnum.PICK_TWO_LOW: playerAnimator.SetTrigger("pickLow"); break;
            case AnimationsEnum.DROP_TWO_HIGH: playerAnimator.SetTrigger("dropOrb"); break;
            case AnimationsEnum.PICK_HIGH_TWO: playerAnimator.SetTrigger("pickBear"); break;
            case AnimationsEnum.DROP_TWO_LOW: playerAnimator.SetTrigger("dropBear"); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animation), animation, null);
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

    public void changeWeight()
    {
        _toOne = !_toOne;
    }
    
    public void changeWeightRight()
    {
        _toOneRight = !_toOneRight;
    }




}
