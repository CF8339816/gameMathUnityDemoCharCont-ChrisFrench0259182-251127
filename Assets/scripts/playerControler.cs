using JetBrains.Annotations;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class playercontroler : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float SprintSpeed = 15f;
    [SerializeField] float CrouchSpeed = 5f;
    [SerializeField] float RotateSpeed = 5f;
    [SerializeField] float gravity = -15f; //has to be neg because is downward force
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float StandHeight = 2f; // default ht of the character
    [SerializeField] float CrouchHeight = 1f; // target ht when crouched

    [SerializeField] float accelerationRate = 5f; //accelleration and decelleration rate
    [SerializeField] float movementSmoothTime = 0.1f; //time the accel & decel takes


    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform groundCheck; //checks the ground  objgect
    [SerializeField] float groundDistance = 0.2f; //grounding variance
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private float currentSpeed;
    private float xRotation = 0f;

    private float targetSpeed;
    private float currentHorizontalSpeed;
    private Vector3 currentMovementInput;
    private Vector3 smoothMoveVelocity; // vector for the SmoothDamp function
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center of screen
        currentSpeed = MoveSpeed; // presets speed at base move
        characterController.height = StandHeight;
        targetSpeed = MoveSpeed;
        currentHorizontalSpeed = MoveSpeed;
    }


    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //grounded check
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small downward force to keep grounded
        }

       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;// mouse rotation
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;     

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp vertical look

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        
        float x = Input.GetAxis("Horizontal");  //movement
        float z = Input.GetAxis("Vertical");

    
        currentMovementInput = transform.right * x + transform.forward * z;
      
        if (currentMovementInput.magnitude > 1)// normalizes movement input in order to prevent diagional magnatude speed increases
        {
            currentMovementInput.Normalize();
        }

        characterController.Move(currentMovementInput * currentHorizontalSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) // jump using input from input System
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

       
        velocity.y += gravity * Time.deltaTime; // applies gravity to player object
        characterController.Move(velocity * Time.deltaTime);

        
        HandleSpeedChanges();// calls the method below that has been used to adjust the sprint and crouch speeds  and limit sprinting while crouched

        currentHorizontalSpeed = Mathf.SmoothDamp(currentHorizontalSpeed,targetSpeed,ref smoothMoveVelocity.x,movementSmoothTime);//  uses SmoothDamp to adjust ease in and out of horizontal movements


        /* unity keeps telling me the shift and ctrl buttons  are not set up even though they are 
         * so  i will code these outside of the input controler but here is the code i wanted to use for the commands 
         * these are pre adding accelerration and decelleration so they are formated differently than the functions below
         * 
  
        //if (Input.GetButtonDown("Shift") && isGrounded)  //sprint using input from input System
        //{
        //    currentSpeed = SprintSpeed;
        //}
        //else if (Input.GetButtonUp("Shift") && isGrounded)
        //{
        //    currentSpeed = MoveSpeed;
        //}

   
        //if (Input.GetButtonDown("Control") && isGrounded) //crouch using input from input System
        //{
        //    isCrouching = !isCrouching;
        //    if (isCrouching)
        //    {
        //        characterController.height = CrouchHeight;
        //        currentSpeed = CrouchSpeed;
        //    }
        //    else if (Input.GetButtonUp("Control") && isGrounded)
        //    {
        //        characterController.height = StandHeight;
        //        currentSpeed = MoveSpeed;
        //    }
        //}
       * 
       * 
       * 
       * 
       */
       
                
    }
    //UM1
    private void HandleSpeedChanges()
    {
        
        if (isCrouching)
        {
            targetSpeed = CrouchSpeed;
            isSprinting = false; // cannot sprint while crouched
        }
        else if (isSprinting)
        {
            targetSpeed = SprintSpeed;
        }
        else
        {
            targetSpeed = MoveSpeed;
        }
              
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isSprinting = false;
        }
       
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                characterController.height = CrouchHeight;
            }
            else
            {
                characterController.height = StandHeight;
            }
        }
    }

}
    
    
    
    
 


