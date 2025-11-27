using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroler : MonoBehaviour
{
    
    CharacterController characterController;
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
    [SerializeField] Camera firstPersonCam;
    public float DefaultHt;

    public bool isSprinting;
    public bool isCrouching;
    Vector2 moveInput;
    Vector3 playerspeed;



    //code  recycled from previous  attempts to save  time above here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        DefaultHt = standing;
    }

   public void Move(Vector3 moveVector)
    {

        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x ;  //gets direction
        move=move*MoveSpeed*Time.deltaTime; //ensures consistant speed independant of framerate
        characterController.Move(move);// moves char



    }


   public void Rotate(Vector3 rotateVector)
    {

        RotationY = rotateVector.x*RotateSpeed*Time.deltaTime;  //
        transform.localRotation=Quaternion.Euler(0,RotationY,0);    // rotates character on the 
       




    }

    //code  recycled from previous  attempts to save  time below  here


    void Update()
    {

        CrouchyCrouchCrouch();
        MoveyMoveMove();
        JumpyJumpJump();

    }

    private void MoveyMoveMove()
    {
        float FPCSSpeed = isSprinting ? SprintSpeed : CrouchSpeed;

        if (moveInput == Vector2.zero)
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

  




}


        //code  recycled from previous  attempts to save  time above here


    