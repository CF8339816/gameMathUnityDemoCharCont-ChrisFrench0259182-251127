using UnityEngine;
using UnityEngine.InputSystem;

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
    
    private float targetHeight;
    private float RotationY;
    private Vector3 speed;
    private float standSpeed = 5f;

    private bool isCrouch = false;
    private bool isSprint = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        targetHeight = StandHeight; // Start in standing position
        characterController.height = StandHeight;
    }


    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, standSpeed * Time.deltaTime); // defines the stand to crouch speed 
    }


    public void Move(Vector3 moveVector)
    {

        float currentSpeed = isCrouch ? CrouchSpeed : MoveSpeed;

        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;  //gets direction
        move = move * currentSpeed * Time.deltaTime; //ensures consistant speed independant of framerate
        Vector3 finalMovement = move + speed * Time.deltaTime; // Apply gravity over time
        characterController.Move(finalMovement); // gravity applied movement

    }

    //
    public void Rotate(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>(); 
        float mouseX = mouseDelta.x * RotateSpeed * Time.deltaTime;//sets x movement by rotating on y axis
        transform.Rotate(Vector3.up * mouseX);// applies rotation to players move
    }

    public void OnJump(InputAction.CallbackContext context)// listens for inputas defined in the input asset
    {
        if (context.performed && characterController.isGrounded) // could not get this to set up correctly trying original method as that did push player up in air // checks to see if the player groundeed beefore jump, prevents double jump
        {
            if (context.performed) // Check if the button was pressed
            {
                if (!isCrouch) // Only allow jumping if not crouching
                {
                    speed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
        }
        //this also did not work
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    speed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // calculate the jump velocity based on the ht input
        //}
    }
        
    public void OnCrouch(InputAction.CallbackContext context)
    {
        // Check if the Ctrl key is held down (performed means the button was just pressed/held)
        if (context.performed)
        {
            isCrouch = true;
            targetHeight = CrouchHeight;
        }
        // Check if the Ctrl key is released (canceled means the button was released)
        else if (context.canceled)
        {
            isCrouch = false;
            targetHeight = StandHeight;
        }
    }
    
    //UM1
    private void ApplyGravity()
    {
        // Apply gravity if not grounded
        if (!characterController.isGrounded)
        {
            speed.y += gravity * Time.deltaTime; // gravity applied here
        }
        else
        {
           
            if (speed.y < 0) // sets vertical movement to just below 0 if on ground to stay grounded
            {
                speed.y = -2f;
            }
        }
    }

    //UM2





}