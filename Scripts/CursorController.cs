using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{

    [SerializeField] private Texture2D cursor;
    [SerializeField] private Texture2D cursorClicked;
    [SerializeField] private InputActionReference mouseButton;

    private void Update() {

        if (mouseButton.action.IsPressed())
        {
            ChangeCursor(cursorClicked);
        } else {
            ChangeCursor(cursor);
        }
    }

    private void Awake() {
        ChangeCursor(cursor);
        //Cursor.lockState = CursorLockMode.Confined;     // Esto hace que el cursor no se pueda salir de la pantalla
    }

    private void ChangeCursor(Texture2D cursorType){    // Cambiamos la imagen del cursor
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

}
