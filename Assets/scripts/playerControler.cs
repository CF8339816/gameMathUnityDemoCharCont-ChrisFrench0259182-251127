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

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private float currentSpeed;
    private float xRotation = 0f;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center of screen
        currentSpeed = MoveSpeed; // presets speed at base move

        //targetHeight = StandHeight; // start in standing position
        characterController.height = StandHeight;
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

        Vector3 move = transform.right * x + transform.forward * z;


        /* unity keeps telling me the shift and ctrl buttons  are not set up even though they are 
         * so  i will code these outside of the input controler but here is the code i wanted to use for the commands 
         * 
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


        if (Input.GetKey(KeyCode.LeftShift)) //sprinting using direct button input
        {
            currentSpeed = SprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = MoveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))//crouching usiing direct button movement
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                characterController.height = CrouchHeight;
                currentSpeed = CrouchSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                characterController.height = StandHeight;
                currentSpeed = MoveSpeed;
            }
        }



        characterController.Move(move * currentSpeed * Time.deltaTime);

    
        if (Input.GetButtonDown("Jump") && isGrounded) // jump using input from input System
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
    
    
    
    
 


