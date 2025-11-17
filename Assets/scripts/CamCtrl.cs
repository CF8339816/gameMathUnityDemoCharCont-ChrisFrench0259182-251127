using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxYRotation = 90f; // verticle limit

    private float rotationX = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
    }

    void Update()
    {
        // Get mouse input
       
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

       
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxYRotation, maxYRotation); // Clamp vertical rotation

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}