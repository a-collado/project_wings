using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] private InputActionReference powerButton;
    [SerializeField] private GameObject powerAnim;
    private Animator playerAnimator;
    private NavMeshAgent player;
    private bool objectPicked;

    private Vector2 velocity;
    private Vector2 smoothDeltaPosition;

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
        player = gameObject.GetComponent<NavMeshAgent>();

        playerAnimator.applyRootMotion = true;
        player.updatePosition = false;
        player.updateRotation = true;
    }

    private void Start() {
        
        playerAnimator.SetFloat("turn", 0.0f);
        
        objectPicked = false;
        powerAnim.SetActive(true);
    }

    private  void OnAnimatorMove() {
        Vector3 rootPosition = playerAnimator.rootPosition;
        rootPosition.y = player.nextPosition.y;
        transform.position = rootPosition;
        player.nextPosition = rootPosition;
    }

    void Update()
    {
        //Debug.Log(velocity);
        SynchronizeAnimationAndAgent();
        powerAnim.SetActive(powerButton.action.ReadValue<float>() > 0);

    }

    private void SynchronizeAnimationAndAgent(){

        Vector3 wordDeltaPosition = player.nextPosition - transform.position;
        wordDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, wordDeltaPosition);
        float dy = Vector3.Dot(transform.forward, wordDeltaPosition);
        Vector2 deltaPosition = new Vector3(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        velocity = smoothDeltaPosition / Time.deltaTime;
        if (player.remainingDistance <= player.stoppingDistance)
        {
            velocity = Vector2.Lerp(
                Vector2.zero, 
                velocity, 
                player.remainingDistance / player.stoppingDistance);
        }

        bool shouldMove = velocity.magnitude > 0.5f
            && player.remainingDistance > player.stoppingDistance;

        playerAnimator.SetBool("isWalking", shouldMove);
        //playerAnimator.SetFloat("locomotion", velocity.magnitude);
        playerAnimator.SetFloat("velocityX", velocity.x);
        playerAnimator.SetFloat("velocityZ", velocity.y);

        float deltaMagnitude = wordDeltaPosition.magnitude;
        if (deltaMagnitude > player.radius / 2f)
        {
            transform.position = Vector3.Lerp(
                playerAnimator.rootPosition,
                player.nextPosition,
                smooth
            );
        }
            
    }

    public void Interact(){
        playerAnimator.SetTrigger("interact");
    }

    public void pickObject(bool b){
        objectPicked = b;
        playerAnimator.SetBool("objectPicked", b);
        Debug.Log(playerAnimator.GetBool("objectPicked"));
    }


}
