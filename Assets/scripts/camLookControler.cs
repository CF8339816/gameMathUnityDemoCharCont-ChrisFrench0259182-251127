using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class camLookControler : MonoBehaviour
{
    float RotationX;
    public float RotateSpeed = 5f;

    //code from old attempt is below here 
    [SerializeField] playercontroler CharacterController;
    [SerializeField] Camera firstPersonCam;
    [SerializeField] float mouseResponsiveness = 100f;
    [SerializeField] float pitchLim = 80f;
    [SerializeField] float xAxisClamp = 0.0f;
    public Vector2 lookInput;


    //Code from old  attempt is above here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Rotate(Vector2 rotateVector)
    {

        RotationX = rotateVector.y * RotateSpeed * Time.deltaTime;  //
        transform.localRotation = Quaternion.Euler(0, RotationX, 0);    // rotates character on the 

    }


        // Update is called once per frame
        void Update()
    {
        
    }


    // code from old attempt is  below  here 

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        // Calculate rotation based on input and mouse responsiveness
        float mouseX = lookInput.x * mouseResponsiveness * Time.deltaTime;
        float mouseY = lookInput.y * mouseResponsiveness * Time.deltaTime;


        xAxisClamp += mouseY;

        if (xAxisClamp > pitchLim)
        {
            xAxisClamp = pitchLim;
            mouseY = 0.0f; // Stop moving if over limit
            ClampXAxisRotation(270f); // Adjust rotation values for clamping
        }
        else if (xAxisClamp < -pitchLim)
        {
            xAxisClamp = -pitchLim;
            mouseY = 0.0f;
            ClampXAxisRotation(90f);
        }

        firstPersonCam.transform.Rotate(Vector2.left * mouseY);

        // Handle Yaw (Horizontal rotation)
        CharacterController.Rotate(Vector2.up * mouseX);
    }

    private void ClampXAxisRotation(float value)


    {
        Vector2 eulerRotation = firstPersonCam.transform.eulerAngles;
        eulerRotation.x = value;
        firstPersonCam.transform.eulerAngles = eulerRotation;
    }


//code frrom oldattempt is above hgere






}
