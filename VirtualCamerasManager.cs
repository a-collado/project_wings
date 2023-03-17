using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamerasManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    // Start is called before the first frame update
    void Start()
    {
        if (virtualCameras.Length < 0)
        {
            Debug.LogError("[VirtualCamerasManager]: No virtual cameras found");
            return;
        }
    }

    public void SetVirtualCamera(int index, float time = 5f)
    {
        if (index < 0 || index >= virtualCameras.Length)
        {
            Debug.LogError("[VirtualCamerasManager]: Invalid index");
            return;
        }
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            if (i == index)
            {
                virtualCameras[i].Priority = 11;
            }
            
            else
            {
                virtualCameras[i].Priority = 0;
            }
        }

        if (time > 0)
        {
            StartCoroutine(ResetMainCamera(time));
        }


    }

    IEnumerator ResetMainCamera(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var virtualCamera in virtualCameras)
        {
            virtualCamera.Priority = 0;
        }
    }
}
