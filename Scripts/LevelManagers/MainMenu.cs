    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private float transitionTime = 1.0f; 


    [Header("Volume Setting")]
    [SerializeField]
    private TMP_Text volumeTextValue;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private float defaultVolume = 0.5f;

    [Header("Confirmation")]
    [SerializeField]
    private GameObject confirmationPrompt;

    [SerializeField]
    private TMP_Text CameraXSensText; 
    [SerializeField]
    private TMP_Text CameraYSensText; 
    [SerializeField]
    private Slider CameraXSlider;
    [SerializeField]
    private Slider CameraYSlider;
    private int CameraXSens;
    private int CameraYSens;

    private float minX = 50.0f;
    private float maxX = 400.0f;
    private float minY = 1.0f;
    private float maxY = 5.0f;
    private float defaultCameraXSens = 300.0f;  // De 50 a 400. Esta el 300 marcado
    private float defaultCameraYSens = 3.0f;    // De 1 a 5. Esta el 3 marcado

    private int _qualityLevel;
    private bool _isFullScreen;

    [SerializeField]
    private Toggle invertX;
    [SerializeField]
    private Toggle invertY;

    [Header("Resolutions Dropwdown")]
    public TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;

    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private Toggle fullScreenToggle;

    private void Start() {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option =resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    private int newGameLevel;
    private int levelToLoad;

    private void Awake() {
        Time.timeScale = 1;
        newGameLevel = 1;
    }
    
    public void play(){
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
          transition.SetTrigger("start");

          yield return new WaitForSeconds(transitionTime); 

          if (PlayerPrefs.HasKey("SavedLevel") && PlayerPrefs.GetInt("SavedLevel") != 0)
        {
            levelToLoad = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        } else 
        {
            SceneManager.LoadScene(newGameLevel);
        } 
    }

    public void exit(){
        Application.Quit();
    }

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(confirmationBox());
    }

    public IEnumerator confirmationBox(){
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void resetButton(string MenuType)
    {
        switch(MenuType){
            case "Audio":
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeTextValue.text = defaultVolume.ToString("0.0");
                volumeApply();
            break;
            case "Gameplay":
                invertX.isOn = false;
                invertY.isOn = true;
                float masterSenX = 10 * (defaultCameraXSens - minX)/(maxX-minX);
                float masterSenY = 10 * (defaultCameraYSens - minY)/(maxY-minY);
                CameraXSlider.value = masterSenX;
                CameraYSlider.value = masterSenY;
                CameraXSensText.text = masterSenX.ToString("0");
                CameraYSensText.text = masterSenY.ToString("0");
                gamePlayApply();
            break;
            case "Graphics":
                qualityDropdown.value = 1;
                QualitySettings.SetQualityLevel(1);

                fullScreenToggle.isOn = false;
                Screen.fullScreen = false;

                Resolution currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
                resolutionDropDown.value = resolutions.Length;
                graphicsApply();
            break;
        }
    }

    public void setCameraXSen(float sensitivity){

        CameraXSens = Mathf.RoundToInt(sensitivity);
        CameraXSensText.text = CameraXSens.ToString("0");

    }

    public void gamePlayApply(){
        if(invertY.isOn)
        {
            PlayerPrefs.SetFloat("masterInvertY", -1f);
        }else{
            PlayerPrefs.SetFloat("masterInvertY", 1f);
        }

        if(invertX.isOn)
        {
            PlayerPrefs.SetFloat("masterInvertX", -1f);
        }else{
            PlayerPrefs.SetFloat("masterInvertX", 1f);
        }

        float masterSenX = CameraXSens * (maxX - minX)/10 + minX;
        float masterSenY = CameraYSens * (maxY - minY)/10 + minY;

        PlayerPrefs.SetFloat("masterSenX", masterSenX);
        PlayerPrefs.SetFloat("masterSenY", masterSenY);
        StartCoroutine(confirmationBox());

    }

    public void setCameraYSen(float sensitivity){
        
        CameraYSens = Mathf.RoundToInt(sensitivity);
        CameraYSensText.text = CameraYSens.ToString("0");

    }

    public void setFullscreen(bool isFullScreen){
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex){
        _qualityLevel = qualityIndex;
    }

    public void graphicsApply(){

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(confirmationBox());
    }

    public void setLevel(int level){
        PlayerPrefs.SetInt("SavedLevel", level);
    }
}
