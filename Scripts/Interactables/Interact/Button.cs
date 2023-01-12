using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    
    private Animator buttonAnimation;

    [SerializeField]
    private Toggleable objectToToggle;
    
    private System.Diagnostics.Stopwatch stopWatch;


    void Awake() {
        buttonAnimation = GetComponent<Animator>();
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();      
    }

    public AnimationsEnum Interact()
    {
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            press();
            stopWatch.Restart();
            return AnimationsEnum.NONE;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    private void press(){
        buttonAnimation.SetTrigger("press");
        objectToToggle.toggle();

        //Podriamos bloquear el boton para que no se vuelva a hacer toggle
    }
    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        
    }
    public bool isActive()
    {
        return true;
    }

    public void activate(bool flag)
    {
    }
}
