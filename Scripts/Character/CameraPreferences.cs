using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPreferences : MonoBehaviour
{

    private CinemachineFreeLook thirdPersonCamera;
    private float sensitivityX;
    private float sensitivityY;
    private float invertX;
    private float invertY;
    
    private void Awake() {
        this.thirdPersonCamera = GetComponent<CinemachineFreeLook>();

        if (PlayerPrefs.HasKey("masterSenX")){
            Debug.Log("1: " + PlayerPrefs.GetFloat("masterSenX"));
            sensitivityX = PlayerPrefs.GetFloat("masterSenX");
        } else {
            sensitivityX = 300.0f;
        }
        if (PlayerPrefs.HasKey("masterSenY")){
            Debug.Log("2: " + PlayerPrefs.GetFloat("masterSenY"));
            sensitivityY = PlayerPrefs.GetFloat("masterSenY");
        }
        else {
            sensitivityY = 3.0f;
        }
        if (PlayerPrefs.HasKey("masterInvertX")){
            Debug.Log("3: " + PlayerPrefs.GetFloat("masterInvertX"));
            invertX = PlayerPrefs.GetFloat("masterInvertX");
        }
        else {
            invertX = 1f;
        }
        if (PlayerPrefs.HasKey("masterInvertY")){
            Debug.Log("4: " + PlayerPrefs.GetFloat("masterInvertY"));
            invertY = PlayerPrefs.GetFloat("masterInvertY");
        }
        else {
            invertY = -1f;
        }

        thirdPersonCamera.m_YAxis.m_MaxSpeed = sensitivityY;
        if(invertY == 1.0f){
            thirdPersonCamera.m_YAxis.m_InvertInput = false;
        } else {
            thirdPersonCamera.m_YAxis.m_InvertInput = true;
        }

        thirdPersonCamera.m_XAxis.m_MaxSpeed = sensitivityX;
        if(invertX == 1.0f){
            thirdPersonCamera.m_XAxis.m_InvertInput = false;
        } else {
            thirdPersonCamera.m_XAxis.m_InvertInput = true;
        }

    }


}
