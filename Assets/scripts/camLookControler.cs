using UnityEngine;
using UnityEngine.InputSystem; // Needed to read input context

public class camLookControler : MonoBehaviour
{

  
    [SerializeField] float mouseSensitivity = 100f;
    private float xRotation = 0f;



    void Update()
    {


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;// mouse rotation
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp vertical look

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


        float x = Input.GetAxis("Horizontal");  //movement
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


    }

}