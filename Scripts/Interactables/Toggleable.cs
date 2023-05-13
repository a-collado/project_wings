using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour, IInteractable
{

    [SerializeField] private bool toggled;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator!= null){
            animator.SetBool("toggled", toggled);
        }else {
            this.gameObject.SetActive(toggled);
        }
    }

    // Update is called once per frame
    public void toggle()
    {
        Debug.Log("Toggle");
        toggled = !toggled;
        if (animator!= null){
            animator.SetBool("toggled", toggled);
        }else {
            this.gameObject.SetActive(toggled);
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
        return !this.toggled;
    }

    public bool isCompleted()
    {
        return !this.toggled;
    }
    
    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
}
