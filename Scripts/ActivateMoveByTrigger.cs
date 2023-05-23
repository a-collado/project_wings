using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMoveByTrigger : MonoBehaviour
{

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
        if (animator == null)
        animator = gameObject.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            if (animator.GetBool("activate") && !this.animator.GetBool("triggerActivate"))
            {
                animator.SetBool("triggerActivate", true);

                this.enabled = false;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
    }
}
