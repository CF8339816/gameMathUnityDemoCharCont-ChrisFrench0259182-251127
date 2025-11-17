using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxYRotation = 90f; // Limit vertical rotation

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Apply horizontal rotation to the player/parent object
        transform.parent.Rotate(Vector3.up * mouseX);

        // Apply vertical rotation to the camera itself
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxYRotation, maxYRotation); // Clamp vertical rotation

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}