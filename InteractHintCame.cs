using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHintCame : MonoBehaviour, IInteractable
{

    [SerializeField] private int resolveCameraIndex = -1;
    [SerializeField] private VirtualCamerasManager cameraManager;

    void Start()
    {
        
    }
    public void activate(bool flag)
    {
        //Switch to hint camera for x secs
        if (resolveCameraIndex != -1 && cameraManager != null)
            cameraManager.SetVirtualCamera(resolveCameraIndex, 5f);
    }

    public GameObject getGameObject()
    {
        throw new System.NotImplementedException();
    }

    public AnimationsEnum Interact()
    {
        return AnimationsEnum.NONE;
    }

    public bool isActive()
    {
        return this.enabled;
    }

    public bool isCompleted()
    {
       return true;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
    }

    
}
