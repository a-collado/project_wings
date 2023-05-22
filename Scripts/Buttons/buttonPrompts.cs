using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPrompts : MonoBehaviour
{
    [SerializeField]
    private ActionsEnum actions;
    private Canvas buttonCanvas;
    private Image buttonImage;
    private FasTPS.PlayerInput input;
    //private Camera playerCamera;

    // Start is called before the first frame update
    private void Start() {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //playerCamera = player.GetComponent<Camera>();
        buttonCanvas = GetComponentInChildren<Canvas>();
        buttonImage = buttonCanvas.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(input);
        //bool keyboardMouse = input.isKeyboardAndMouse;
        /*Sprite img;

        if (keyboardMouse){
            img = Resources.Load<Sprite>("/Prompts/Keyboard/"+actions);
        }else{
            img = Resources.Load<Sprite>("/Prompts/Mando/"+actions);
        }

        buttonImage.sprite = img;
        //buttonCanvas.transform.LookAt(playerCamera.transform);*/

    }
    
    private void OnTriggerEnter(Collider other) {

        buttonCanvas.enabled = true;
        bool keyboardMouse = FasTPS.PlayerInput.isKeyboardAndMouse;

        Sprite img;

        if (keyboardMouse){
            img = Resources.Load<Sprite>("Keyboard/"+actions);
        }else{
            img = Resources.Load<Sprite>("Mando/"+actions);
        }

        buttonImage.sprite = img;
    }

    private void OnTriggerStay(Collider other) {
        buttonCanvas.enabled = true;
        bool keyboardMouse = FasTPS.PlayerInput.isKeyboardAndMouse;

        Sprite img;

        if (keyboardMouse){
            img = Resources.Load<Sprite>("Keyboard/"+actions);
        }else{
            img = Resources.Load<Sprite>("Mando/"+actions);
        }

        buttonImage.sprite = img;
    
    }

    private void OnTriggerExit(Collider other) {
        buttonCanvas.enabled = false;

    }
}
