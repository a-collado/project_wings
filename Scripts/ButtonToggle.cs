using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject toToggle;
    private Animator animator;
    public void activate(bool flag)
    {
        enabled = flag;
    }

    public AnimationsEnum Interact()
    {
        animator.SetBool("activate", !animator.GetBool("activate"));
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
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = toToggle.GetComponent<Animator>();
    }

    void IInteractable.Update()
    {
    }
}
