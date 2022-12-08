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
    [SerializeField] private InputActionReference mousePosition;


    private float currentX = 10;
    private float currentY = 0;
    private Camera cam;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        camTransform = transform;
        //directionNormalized = gameObject.transform.position.normalized;
    }

    private void Update() {
        Vector2 position = mousePosition.action.ReadValue<Vector2>();
        currentX += position.x * (sensitivityX * 0.01f);
        currentY += position.y * (sensitivityY * 0.01f);

        currentY = Mathf.Clamp(currentY, angleMinY, angleMaxY);
    }

    private void LateUpdate() 
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = player.position + rotation * dir;
        camTransform.LookAt(player.position);
    }
}
