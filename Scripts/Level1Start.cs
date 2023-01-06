using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Start : MonoBehaviour
{
    [SerializeField]
    private Canvas transitionCanvas;
    [SerializeField]
    private GameObject player;
    private Animator playerAnimator;

    private void Awake() {
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("getUp");
    }

    private void Start() {
        transitionCanvas.gameObject.SetActive(true);

    }
}
