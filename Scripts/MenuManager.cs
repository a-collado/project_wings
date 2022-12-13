using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private InputActionReference pauseButton;
    [SerializeField] private Canvas pauseCanvas;

    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CameraController cameraController;

    private System.Diagnostics.Stopwatch stopWatch;
    private bool paused;

    private void Awake() {
        paused = false;
        stopWatch = new System.Diagnostics.Stopwatch();
        pauseCanvas.gameObject.SetActive(paused);
        stopWatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(pauseButton.action.ReadValue<float>() > 0){
            if(time > 0.2)
            {
                pauseGame();
            }
        stopWatch.Restart();
        }
    }

    private void LateUpdate() {
        managePause();
    }

    private void managePause()
    {
        pauseCanvas.gameObject.SetActive(paused);
        characterMovement.enabled = !paused;
        cameraController.enabled = !paused;
        
        if(paused)  Time.timeScale = 0;
        else Time.timeScale = 1;
        
    }

    public void pauseGame()
    {
        paused = !paused;
    }



}
