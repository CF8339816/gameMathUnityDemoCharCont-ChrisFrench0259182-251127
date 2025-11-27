using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroler : MonoBehaviour
{

    public CharacterController characterController;
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float SprintSpeed = 15f;
    [SerializeField] float CrouchSpeed = 5f;
    [SerializeField] float RotateSpeed = 5f;
    float RotationY;
    private Vector3 speed;
    [SerializeField] float gravity = -15f; //has to be neg because is downward force
    [SerializeField] float jumpHeight = 2f;

    //code  recycled from previous  attempts to save  time above here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        ApplyGravity();

    }


    public void Move(Vector3 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;  //gets direction
        move = move * MoveSpeed * Time.deltaTime; //ensures consistant speed independant of framerate
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
                Debug.Log("Jump function was called! Input received."); // Add this line
                if (characterController.isGrounded)
                {
                    Debug.Log("Character is grounded. Applying jump velocity."); // Add this line
                    speed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
                else
                {
                    Debug.Log("Character is NOT grounded. Cannot jump right now."); // Add this line
                }
            }
        }
        //this also did not work
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    speed.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // calculate the jump velocity based on the ht input
        //}
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





}