using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroler : MonoBehaviour
{
    
    public CharacterController characterController;
    [SerializeField] float MoveSpeed= 10f;
    [SerializeField] float SprintSpeed = 15f;
    [SerializeField] float CrouchSpeed = 5f;
    [SerializeField] float RotateSpeed = 5f;
    float RotationY;

    //code  recycled from previous  attempts to save  time below  here
    [SerializeField] float AccelSpeed = 4.0f;
    [SerializeField] float DecelSpeed = 6.0f;
    [SerializeField] float Gravity = -9.810f;  //gravity negative because itr is negin  the inspector
    [SerializeField] float JumpHt = 2.0f;
    [SerializeField] float standing = 2.0f;
    [SerializeField] float crouching = 1.0f;
    private Camera firstPersonCam;
    private float DefaultHt;

    private bool isSprinting;
    private bool isCrouching;
    private Vector3 moveInput;
    private Vector3 playerspeed;
    private Vector2 rotateInput;

    [SerializeField] float mouseResponsiveness = 100f;
    [SerializeField] float pitchLim = 80f;
    //[SerializeField] float yAxisClamp = 0.0f;
    //code  recycled from previous  attempts to save  time above here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        DefaultHt = standing;
    }

    public void Move(Vector3 moveVector)
    {

        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;  //gets direction
        move = move * MoveSpeed * Time.deltaTime; //ensures consistant speed independant of framerate
        characterController.Move(move);// moves char

    }


    public void Rotate(Vector2 rotateVector)
    {

        RotationY = rotateVector.x * RotateSpeed * Time.deltaTime;  //
        transform.localRotation = Quaternion.Euler(0, RotationY, 0);    // rotates character on the 

    }

   // code recycled from previous  attempts to save time below here


    void Update()
    {

        CrouchyCrouchCrouch();
        MoveyMoveMove();
        JumpyJumpJump();
        //RotateyTateTate();
    }

    private void MoveyMoveMove()
    {
        float FPCSSpeed = isSprinting ? SprintSpeed : CrouchSpeed;

        if (moveInput == Vector3.zero)
        {
            MoveSpeed = Mathf.MoveTowards(MoveSpeed, 0, DecelSpeed * Time.deltaTime); //handles no keyinput
        }
        else
        {
            MoveSpeed = Mathf.MoveTowards(MoveSpeed, FPCSSpeed, AccelSpeed * Time.deltaTime); // acceleration
        }

        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;// Creates mvmnt rel to plyr fwd

        //setting upmovement  speed
        playerspeed.x = moveDirection.x * MoveSpeed;
        playerspeed.z = moveDirection.z * MoveSpeed;
    }

    private void JumpyJumpJump()
    {
        if (!characterController.isGrounded)// applies gravity if not on ground
        {
            playerspeed.y += Gravity * Time.deltaTime;// simulated gravity constant
        }
        else
        {
            if (playerspeed.y < 0)
            {
                playerspeed.y = -2f;// small neg force to ensure ground contact of player.
            }
        }
        characterController.Move(playerspeed * Time.deltaTime);// uses controler object for movement
    }

    private void CrouchyCrouchCrouch()
    {
        DefaultHt = isCrouching ? crouching : standing; // det crouch or stand  movement direction

        characterController.height = Mathf.Lerp(characterController.height, DefaultHt, Time.deltaTime * 5f);// ht change for crouch

        if (firstPersonCam != null) //helps control the child fps cam during jump and crouch
        {
            float DefaultCamY = isCrouching ? crouching * 0.5f : standing * 0.8f;
            Vector3 newCamPos = firstPersonCam.transform.localPosition;
            newCamPos.y = Mathf.Lerp(newCamPos.y, DefaultCamY, Time.deltaTime * 5f);
            firstPersonCam.transform.localPosition = newCamPos;
        }

        if (isCrouching)
        {
            isSprinting = false; // S.E.
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed || context.started;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            playerspeed.y = Mathf.Sqrt(JumpHt * 2f * Mathf.Abs(Gravity));// because grav is neg in inspector, so use Mathf.Abs() to compensate
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = !isCrouching;
        }
    }


    private void LateUpdate()
    {
        // Calculate rotation based on input and mouse responsiveness
        float mouseX = rotateInput.x * mouseResponsiveness * Time.deltaTime;
        float mouseY = rotateInput.y * mouseResponsiveness * Time.deltaTime;


        //xAxisClamp += mousex;

        //if (xAxisClamp > pitchLim)
        //{
        //    xAxisClamp = pitchLim;
        //    mouseY = 0.0f; // Stop moving if over limit
        //    ClampXAxisRotation(270f); // Adjust rotation values for clamping
        //}
        //else if (xAxisClamp < -pitchLim)
        //{
        //    xAxisClamp = -pitchLim;
        //    mouseY = 0.0f;
        //    ClampXAxisRotation(90f);
        //}

        firstPersonCam.transform.Rotate(Vector2.up * mouseX);


    }



}


        //code  recycled from previous  attempts to save  time above here


    