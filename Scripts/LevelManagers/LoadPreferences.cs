 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPreferences : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenu menuController;

    [Header("Volume Setting")]
    [SerializeField]
    private TMP_Text volumeTextValue;
    [SerializeField]
    private Slider volumeSlider;

    [Header("Sensitivity Setting")]
    [SerializeField]
    private TMP_Text CameraXSensText; 
    [SerializeField]
    private TMP_Text CameraYSensText; 
    [SerializeField]
    private Slider CameraXSlider;
    [SerializeField]
    private Slider CameraYSlider;
    [SerializeField]
    private float DefaultCameraXSens = 300;
    [SerializeField]
    private float DefaultCameraYSens = 3;
    private float minX = 0.05f;
    private float maxX = 0.5f;
    private float minY = 1.0f;
    private float maxY = 5.0f;

    [Header("Invert Setting")]
    [SerializeField]
    private Toggle invertX;
    [SerializeField]
    private Toggle invertY;

    [Header("Quality Level Setting")]
    [SerializeField]
    private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField]
    private Toggle fullScreenToggle;


    private void Awake() 
    {
        if(canUse)
        {
            if(PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.resetButton("Audio");
            }

            if(PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
            if(PlayerPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if (localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }
            else
            {
                Screen.fullScreen = true;
                fullScreenToggle.isOn = true;
            }

            if(PlayerPrefs.HasKey("masterSenX"))
            {
                float localSensitivityX = PlayerPrefs.GetFloat("masterSenX");
                float newSensitvityX = 10 * (localSensitivityX - minX) / (maxX - minX);
                CameraXSensText.text = newSensitvityX.ToString("0");
                CameraXSlider.value = newSensitvityX;
                menuController.setCameraXSen(newSensitvityX);
            }else {
                CameraXSensText.text = DefaultCameraXSens.ToString("0");
                CameraXSlider.value = DefaultCameraXSens;
                menuController.setCameraXSen(DefaultCameraXSens);
            }
            if(PlayerPrefs.HasKey("masterSenY"))
            {
                float localSensitivityY = PlayerPrefs.GetFloat("masterSenY");
                float newSensitvityY = 10 * (localSensitivityY - minY) / (maxY - minY);
                CameraYSensText.text = newSensitvityY.ToString("0");
                CameraYSlider.value = newSensitvityY;
                menuController.setCameraYSen(newSensitvityY);
            }else{
                CameraYSensText.text = DefaultCameraYSens.ToString("0");
                CameraYSlider.value = DefaultCameraYSens;
                menuController.setCameraYSen(DefaultCameraYSens);
            }
            if(PlayerPrefs.HasKey("masterInvertX"))
            {
                float localInvertX = PlayerPrefs.GetFloat("masterInvertX");
                
                if(localInvertX == 1) invertX.isOn = false;
                else invertX.isOn = true;

            }
            if(PlayerPrefs.HasKey("masterInvertY"))
            {
                float localInvertY = PlayerPrefs.GetFloat("masterInvertY");

                if(localInvertY == 1) invertY.isOn = false;
                else invertY.isOn = true;

            }

        }
    }
}
