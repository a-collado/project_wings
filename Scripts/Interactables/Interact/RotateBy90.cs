using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBy90 : MonoBehaviour, IInteractable
{

    private System.Diagnostics.Stopwatch stopWatch;
    private Quaternion targetRotation;
    [Range(0, 1)]
    [SerializeField]
    private float rotationSpeed = 0.1f;
    public int currentPosition = 0;

    private void Awake() {
        targetRotation = transform.rotation;
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
    }

    public void activate(bool flag)
    {
        this.enabled = flag;  
    }

    public AnimationsEnum Interact()
    {
        
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            stopWatch.Restart();
            targetRotation =  transform.rotation * Quaternion.Euler(90, 0, 0);
            currentPosition++;
            if(currentPosition == 4) currentPosition = 0;
            return AnimationsEnum.NONE;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public bool isActive()
    {
        return this.enabled;
    }

    public bool isCompleted()
    {
       return isActive();
    }

    private void Update() {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed);
    }

    public void Power()
    {
    }

    void IInteractable.Update()
    {
        throw new System.NotImplementedException();
    }
}
