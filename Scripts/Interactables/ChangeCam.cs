using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    [SerializeField] private int resolveCameraIndex = -1;
    [SerializeField] private VirtualCamerasManager cameraManager;

    private void OnTriggerEnter(Collider other)
    {
        cameraManager.SetVirtualCamera(resolveCameraIndex, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        cameraManager.resetCamera();
    }
}
