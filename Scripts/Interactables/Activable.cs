using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    [SerializeField] private GameObject toActivate;
    private Animator animator;

    
    void Start()
    {
        if (toActivate == null)
        {
            toActivate = gameObject;
        }
        animator = toActivate.GetComponent<Animator>();
    }
    public void Activate()
    {
        Debug.Log("[Activable] Activated!]");
        animator.SetBool("activate", true);
        toActivate.GetComponent<IInteractable>().activate(true);
    
    }
}
