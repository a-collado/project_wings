using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private int defaultCameraXSens = 25;
    private int defaultCameraYSens = 5;

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


    private string newGameLevel;
    private string levelToLoad;

    private void Awake() {
        newGameLevel = "Level1";
    }
    
    public void play(){
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
          transition.SetTrigger("start");

          yield return new WaitForSeconds(transitionTime); 

          if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
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
                invertY.isOn = false;
                CameraXSlider.value = defaultCameraXSens;
                CameraYSlider.value = defaultCameraYSens;
                CameraXSensText.text = defaultCameraXSens.ToString("0");
                CameraYSensText.text = defaultCameraYSens.ToString("0");
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

        PlayerPrefs.SetFloat("masterSenX", CameraXSens);
        PlayerPrefs.SetFloat("masterSenY", CameraYSens);
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
}
