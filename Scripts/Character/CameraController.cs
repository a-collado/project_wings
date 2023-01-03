using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 11.0f;
    [SerializeField] private float sensitivityX = 30.0f;
    [SerializeField] private float sensitivityY = 10.0f;
    [SerializeField] private float angleMinY = 10.0f;
    [SerializeField] private float angleMaxY = 60.0f;
    [SerializeField] private InputActionReference mousePosition, mouseScroll;
    private float invertX = 1f;
    private float invertY = 1f;

    private float zoomSensitivity = 1.0f;
    private float zoomMin = 4.0f;
    private float zoomMax = 17.0f;


    private float currentX = 10;
    private float currentY = 0;
    private float currentDistance;
    private Camera cam;
    private Transform camTransform;

    private void Awake() {
        if (PlayerPrefs.HasKey("masterSenX")){
            sensitivityX = PlayerPrefs.GetFloat("masterSenX");
        }
        if (PlayerPrefs.HasKey("masterSenY")){
            sensitivityY = PlayerPrefs.GetFloat("masterSenY");
        }
        if (PlayerPrefs.HasKey("masterInvertX")){
            invertX = PlayerPrefs.GetFloat("masterInvertX");
        }
        if (PlayerPrefs.HasKey("masterInvertY")){
            invertY = PlayerPrefs.GetFloat("masterInvertY");
        }
    }

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        camTransform = transform;
        currentDistance = distance;
    }

    private void Update() {
        Vector2 position = mousePosition.action.ReadValue<Vector2>();
        currentX += position.x * (sensitivityX * 0.01f) * invertX;
        currentY += position.y * (sensitivityY * 0.01f * invertY);

        currentY = Mathf.Clamp(currentY, angleMinY, angleMaxY);

        float dist = mouseScroll.action.ReadValue<float>();
        currentDistance -= dist * (zoomSensitivity * 0.01f);

        currentDistance = Mathf.Clamp(currentDistance, zoomMin, zoomMax);
    }

    private void LateUpdate() 
    {
        Vector3 dir = new Vector3(0, 0, -currentDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = player.position + rotation * dir;
        camTransform.LookAt(player.position);
    }
}
