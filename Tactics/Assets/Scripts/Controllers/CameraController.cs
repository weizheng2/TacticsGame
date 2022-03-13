using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform followTransform;
    public Transform cameraTransform;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public static CameraController GetInstance()
    {
        if (instance == null)
            instance = new CameraController();

        return instance;
    }

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMouseInput();
            HandleMovementInput();

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    void HandleMouseInput()
    {
        // Zoom
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        // Movement
        // Mouse button first click
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float point))
            {
                dragStartPosition = ray.GetPoint(point);
            }
        }
        // Mouse button held down
        if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float point))
            {
                dragCurrentPosition = ray.GetPoint(point);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        // Rotation
        /*if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (difference.x / 5f));
        }*/
    }

    void HandleMovementInput()
    {
        // Movement
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        // Zoom
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }
    }
}
