using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamHit : MonoBehaviour
{
    public bool completed = false;
    private int triggers = 0;

    public void Complete(){
        this.completed = true;
        Debug.Log("Completed");
    }

    private void Update()
    {
        if (triggers == 2)
        {
            Complete();
        }
    }

    void onTriggerEnter(Collider other){
        Debug.Log("LightBeamHit");
    }

    private void OnTriggerEnter(Collider other)
    {
        triggers++;
    }

    private void OnTriggerExit(Collider other)
    {
        triggers--;
    }
}
