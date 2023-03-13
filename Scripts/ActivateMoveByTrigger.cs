using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMoveByTrigger : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool("activate") && !this.animator.GetBool("triggerActivate"))
        {
            animator.SetBool("triggerActivate", true);
            Debug.Log("TriggerActivated");

            this.enabled = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
    }
}
