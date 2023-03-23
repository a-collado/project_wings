using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour, IInteractable
{
    
    private Animator buttonAnimation;

    [SerializeField]
    private Toggleable[] objectsToToggle;
    
    //[SerializeField] private Camera puzzleCam;
    private System.Diagnostics.Stopwatch stopWatch;
    private bool toggled = false;


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
        //Camera.main.enabled = false;
        //puzzleCam.enabled = true;
        Debug.Log("Press");
        buttonAnimation.SetTrigger("press");
        foreach (var objectToToggle in objectsToToggle)
        {
            objectToToggle.toggle();

        }
        toggled = !toggled;
        activate(false);

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

    public bool isCompleted()
    {
        return !toggled;
    }
    public void activate(bool flag)
    {
        //this.enabled = flag;
        if (flag){
            this.gameObject.layer = LayerMask.NameToLayer (LayerMask.LayerToName(3));
        }else{
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
