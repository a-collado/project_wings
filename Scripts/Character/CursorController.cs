using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{

    public enum Cursors
    {
        normalCursor,
        cursorClicked
    }

    [SerializeField] private Texture2D cursor;
    [SerializeField] private Texture2D cursorClicked;
    [SerializeField] private InputActionReference mouseButton;

    private void Update() {

       // if (!mouseButton.action.IsPressed())
       //     ChangeCursor(Cursors.normalCursor);
    }

    private void Awake() {
        ChangeCursor(Cursors.normalCursor);

        //Cursor.lockState = CursorLockMode.Confined;     // Esto hace que el cursor no se pueda salir de la pantalla
    }

    public void ChangeCursor(Cursors cursors){    // Cambiamos la imagen del cursor

        Texture2D usedCursor;

        switch (cursors)
        {
            case Cursors.cursorClicked: usedCursor = cursorClicked;
                break;
            default: usedCursor = cursor;
                break;
        }
        
        Cursor.SetCursor(usedCursor, Vector2.zero, CursorMode.Auto);
    }

}
