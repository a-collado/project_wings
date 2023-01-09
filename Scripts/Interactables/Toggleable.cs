using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour, IInteractable
{

    [SerializeField] private bool hidden;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator!= null){
            animator.SetBool("hidden", hidden);
        }else {
            this.gameObject.SetActive(hidden);
        }
    }

    // Update is called once per frame
    public void toggle()
    {
        hidden = !hidden;
        if (animator!= null){
            animator.SetBool("hidden", hidden);
        }else {
            this.gameObject.SetActive(hidden);
        }
        
    }

    void IInteractable.Update()
    {
    }

    AnimationsEnum IInteractable.Interact()
    {
        return AnimationsEnum.NONE;
    }

    void IInteractable.Power()
    {
    }

    void IInteractable.activate(bool flag)
    {
        this.enabled = flag;
    }

    bool IInteractable.isActive()
    {
        return !this.gameObject.activeSelf;
    }
}
