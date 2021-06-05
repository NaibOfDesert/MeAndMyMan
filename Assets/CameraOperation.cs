using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraOperation : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 zoomAmount;
    public float movementTime;
    public float rotationAmount;
    // public float movementSpeed;

    public Vector3 newZoom; 
    public Quaternion newRotation;
    // public Vector3 newPosition;

    private void Start()
    {
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        // newPosition = transform.position;
    }

    private void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newZoom += zoomAmount;
            // newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newZoom -= zoomAmount;
            // newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            // newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            // newPosition += (transform.right * -movementSpeed);
        }
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        // transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
}
