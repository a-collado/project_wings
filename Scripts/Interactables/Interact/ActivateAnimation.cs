using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour, IInteractable
{

    [SerializeField]
    Animator animator;

    public void activate(bool flag)
    {
    }

    public AnimationsEnum Interact()
    {
        animator.SetBool("activate", true);
        return AnimationsEnum.NONE;
    }

    public bool isActive()
    {
        return true;
    }

    public bool isCompleted()
    {
        return true;
    }

    public void Power()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IInteractable.Update()
    {
    }
}
