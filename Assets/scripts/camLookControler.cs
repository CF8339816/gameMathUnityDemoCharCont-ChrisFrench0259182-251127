using UnityEngine;
using UnityEngine.InputSystem; // Needed to read input context

public class camLookControler : MonoBehaviour
{
    [SerializeField] float MouseSensitivity = 5f; // Use a similar sensitivity
    [SerializeField] float VerticalClampAngle = 80f; // Limit how far up/down you can look (e.g., 80 degrees)

    private float _verticalRotation = 0f; // Track our current vertical angle

    // This method needs to be hooked up to the *same* 'Rotate' Input Action as the player script
    public void Look(InputAction.CallbackContext context)
    {
        // Read the full Vector2 mouse input
        Vector2 mouseDelta = context.ReadValue<Vector2>();

        // We only care about the vertical (Y-axis) movement of the mouse for the camera
        float mouseY = mouseDelta.y * MouseSensitivity * Time.deltaTime;

        // Invert the mouse Y input if needed (standard FPS look is inverted Y)
        _verticalRotation -= mouseY;

        // Clamp the rotation so you can't flip the camera upside down (gimbal lock prevention)
        _verticalRotation = Mathf.Clamp(_verticalRotation, -VerticalClampAngle, VerticalClampAngle);

        // Apply the rotation to the *camera's* local X-axis (pitch)
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
}














//using UnityEngine;
//using UnityEngine.InputSystem;

//public class camLookControler : MonoBehaviour
//{
//    float RotationX;
//    public float RotateSpeed = 5f;

//    //code from old attempt is below here 
//    [SerializeField] playercontroler CharacterController;
//    [SerializeField] Camera firstPersonCam;
//    [SerializeField] float mouseResponsiveness = 100f;
//    [SerializeField] float pitchLim = 80f;
//    [SerializeField] float xAxisClamp = 0.0f;
//    public Vector2 lookInput;


//    //Code from old  attempt is above here

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    void Rotate(Vector2 rotateVector)
//    {

//        RotationX = rotateVector.y * RotateSpeed * Time.deltaTime;  //
//        transform.localRotation = Quaternion.Euler(0, RotationX, 0);    // rotates character on the 

//    }


//    // Update is called once per frame
//    void Update()
//    {

//    }


//    // code from old attempt is  below  here 

//    public void OnLook(InputAction.CallbackContext context)
//    {
//        lookInput = context.ReadValue<Vector2>();
//    }

//    private void LateUpdate()
//    {
//        // Calculate rotation based on input and mouse responsiveness
//        float mouseX = lookInput.x * mouseResponsiveness * Time.deltaTime;
//        float mouseY = lookInput.y * mouseResponsiveness * Time.deltaTime;


//        xAxisClamp += mouseY;

//        if (xAxisClamp > pitchLim)
//        {
//            xAxisClamp = pitchLim;
//            mouseY = 0.0f; // Stop moving if over limit
//            ClampXAxisRotation(270f); // Adjust rotation values for clamping
//        }
//        else if (xAxisClamp < -pitchLim)
//        {
//            xAxisClamp = -pitchLim;
//            mouseY = 0.0f;
//            ClampXAxisRotation(90f);
//        }

//        firstPersonCam.transform.Rotate(Vector2.left * mouseY);


//    }

//    private void ClampXAxisRotation(float value)


//    {
//        Vector2 eulerRotation = firstPersonCam.transform.eulerAngles;
//        eulerRotation.x = value;
//        firstPersonCam.transform.eulerAngles = eulerRotation;
//    }


//    //code frrom oldattempt is above hgere






//}
