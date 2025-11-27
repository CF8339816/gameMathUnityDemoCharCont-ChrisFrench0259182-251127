using UnityEngine;
using UnityEngine.InputSystem;
// Removed unnecessary or redundant using statements:
// using System.Threading;
// using UnityEngine.InputSystem.XR;
// using UnityEngine.ProBuilder.MeshOperations;
// using UnityEngine.SceneManagement;
// using static UnityEditorInternal.ReorderableList; // Editor-only, not needed in runtime scripts

public class FPCSControler : MonoBehaviour
{
    // had exponential accellerationn
    // andspeed  in oldewr  code . so read
    // more on controler and looked at a
    // few more tutoruials and rewrote the code  

    CharacterController controler;
    [SerializeField] Camera firstPersonCam;
    // Removed redundant serialized fields for components found via GetComponent
    // [SerializeField] Transform FPCS; 

    [SerializeField] float speed;
    [SerializeField] float AccelSpeed = 4.0f;
    [SerializeField] float DecelSpeed = 6.0f;
    [SerializeField] float MinSpeed = 5.0f;
    [SerializeField] float MaxSpeed = 10.0f;
    //[SerializeField] float Gravity = 9.81f; //meters per second squared gravity coeficcent
    [SerializeField] float Gravity = -15.0f;  //gravity valiue reccomended in turtorial 
    [SerializeField] float JumpHt = 2.0f;
    // [SerializeField] float JumpDis; // Unused field
    // [SerializeField] float SprintMultiplier; // Unused field
    [SerializeField] float standing = 2.0f;
    [SerializeField] float crouching = 1.0f;

    [SerializeField] float mouseResponsiveness = 100f;
    [SerializeField] float pitchLim = 80f;
    float xAxisClamp = 0.0f; // Made private as it is internal logic

    float DefaultHt;

    Vector2 lookInput;
    Vector3 playerspeed;
    Vector2 moveInput;

    bool isSprinting;
    bool isCrouching;

    void Start()
    {
        DefaultHt = standing;
        speed = MinSpeed;
        controler = GetComponent<CharacterController>();

        // Ensure components are assigned correctly if not done in the Inspector
        if (transform.parent == null) // FPCS should refer to this object (the character controller)
                                      // FPCS = transform; // This line was redundant
            if (firstPersonCam == null)
                firstPersonCam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // locks and hides cursor in thecenter of the screen
    }

    // Update is called once per frame
    void Update()
    {
        CrouchyCrouchCrouch();
        MoveyMoveMove();
        JumpyJumpJump();

        // **Removed the conflicting Input.GetKey logic.** 
        // The Input System callbacks (OnMove, OnSprint) handle input state 
        // more effectively and consistently with the rest of the script's design.
    }

    private void MoveyMoveMove()
    {
        float FPCSSpeed = isSprinting ? MaxSpeed : MinSpeed;

        if (moveInput == Vector2.zero)
        {
            speed = Mathf.MoveTowards(speed, 0, DecelSpeed * Time.deltaTime); //handles no keyinput
        }
        else
        {
            speed = Mathf.MoveTowards(speed, FPCSSpeed, AccelSpeed * Time.deltaTime); // acceleration
        }

        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;// Creates mvmnt rel to plyr fwd

      
        playerspeed.x = moveDirection.x * speed;  //setting upmovement  speed
        playerspeed.z = moveDirection.z * speed; //ditto
    }

    private void JumpyJumpJump()
    {
       
        if (!controler.isGrounded) // Applies gravity if not on ground
        {
            
            playerspeed.y += Gravity * Time.deltaTime;
        }
        else
        {
            if (playerspeed.y < 0)
            {
                playerspeed.y = -2f; // Small neg force to ensure ground contact of player.
            }
        }
        
        controler.Move(playerspeed * Time.deltaTime);
    }

    private void CrouchyCrouchCrouch()
    {
        DefaultHt = isCrouching ? crouching : standing; // det crouch or stand  movement direction

        controler.height = Mathf.Lerp(controler.height, DefaultHt, Time.deltaTime * 5f);// ht change for crouch

        if (firstPersonCam != null) //helps control the child fps cam during jump and crouch
        {
            float DefaultCamY = isCrouching ? crouching * 0.5f : standing * 0.8f;
            Vector3 newCamPos = firstPersonCam.transform.localPosition;
            newCamPos.y = Mathf.Lerp(newCamPos.y, DefaultCamY, Time.deltaTime * 5f);
            firstPersonCam.transform.localPosition = newCamPos;
        }

        if (isCrouching)
        {
            isSprinting = false; // Prevents sprinting while crouching
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controler.isGrounded)
        {
            // Calculation for jump velocity based on desired height and gravity
            playerspeed.y = Mathf.Sqrt(JumpHt * 2f * Mathf.Abs(Gravity));
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = !isCrouching; // Toggle crouch state
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    // --- Camera/Look Logic ---

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
            ClampXAxisRotation(pitchLim); // Use pitch lim for clamping
        }
        else if (xAxisClamp < -pitchLim)
        {
            xAxisClamp = -pitchLim;
            mouseY = 0.0f;
            ClampXAxisRotation(-pitchLim); // Use neg pitch lim for clamping
        }

      
        firstPersonCam.transform.Rotate(Vector3.left * mouseY);

       
        transform.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotation(float value)
    {
        Vector3 eulerRotation = firstPersonCam.transform.eulerAngles;
       
        firstPersonCam.transform.localRotation = Quaternion.Euler(value,firstPersonCam.transform.localEulerAngles.y,firstPersonCam.transform.localEulerAngles.z);

      
    }
}










//using System.Threading;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.XR;
//using UnityEngine.ProBuilder.MeshOperations;
//using UnityEngine.SceneManagement;
//using static UnityEditorInternal.ReorderableList;

//public class FPCSControler : MonoBehaviour
//{
//    // had exponential accellerationn
//    // andspeed  in oldewr  code . so read
//    // more on controler and looked at a
//    // few more tutoruials and rewrote the code  



//    CharacterController controler;
//    [SerializeField] Camera firstPersonCam;
//    [SerializeField] Transform FPCS;

//    [SerializeField] float speed;
//    [SerializeField] float AccelSpeed = 4.0f;
//    [SerializeField] float DecelSpeed = 6.0f;
//    [SerializeField] float MinSpeed = 5.0f;
//    [SerializeField] float MaxSpeed = 10.0f;
//    //[SerializeField] float Gravity = 9.81f; //meters per second squared gravity coeficcent
//    [SerializeField] float Gravity = -15.0f;  //gravity valiue reccomended in turtorial 
//    [SerializeField] float JumpHt = 2.0f;
//    [SerializeField] float JumpDis;
//    [SerializeField] float SprintMultiplier;
//    [SerializeField] float standing = 2.0f;
//    [SerializeField] float crouching = 1.0f;

//    [SerializeField] float mouseResponsiveness = 100f;
//    [SerializeField] float pitchLim = 80f;
//    [SerializeField] float xAxisClamp = 0.0f;



//    float DefaultHt;

//    Vector2 lookInput;
//    Vector3 playerspeed;
//    Vector2 moveInput;

//    bool isSprinting;
//    bool isCrouching;

//    void Start()
//    {

//        DefaultHt = standing;
//        speed = MinSpeed;
//        controler = GetComponent<CharacterController>();

//        FPCS = transform;
//        firstPersonCam = GetComponentInChildren<Camera>();


//        Cursor.lockState = CursorLockMode.Locked; // locks and hides cursor in thecenter of the screen
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        CrouchyCrouchCrouch();
//        MoveyMoveMove();
//        JumpyJumpJump();

//        Vector3 inputVector = Vector3.zero;

//        if (Input.GetKey(KeyCode.W))
//{
//    speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
//    inputVector.z += speed;

// }
//if (Input.GetKey(KeyCode.S))
//{

//    speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
//    inputVector.z -= speed;

//}
//if (Input.GetKey(KeyCode.A))
//{

//    speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
//    inputVector.x -= speed;
//}
//if (Input.GetKey(KeyCode.D))
//{

//    speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
//    inputVector.x += speed;
//}
//    }





//private void MoveyMoveMove()
//    {
//        float FPCSSpeed = isSprinting ? MaxSpeed : MinSpeed;

//        if (moveInput == Vector2.zero)
//        {
//            speed = Mathf.MoveTowards(speed, 0, DecelSpeed * Time.deltaTime); //handles no keyinput
//        }
//        else
//        {
//            speed = Mathf.MoveTowards(speed, FPCSSpeed, AccelSpeed * Time.deltaTime); // acceleration
//        }

//        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;// Creates mvmnt rel to plyr fwd

//        //setting upmovement  speed
//        playerspeed.x = moveDirection.x * speed;
//        playerspeed.z = moveDirection.z * speed;
//    }

//    private void JumpyJumpJump()
//    {
//        if (!controler.isGrounded)// applies gravity if not on ground
//        {
//            playerspeed.y += Gravity * Time.deltaTime;// simulated gravity constant
//        }
//        else
//        {
//            if (playerspeed.y < 0)
//            {
//                playerspeed.y = -2f;// small neg force to ensure ground contact of player.
//            }
//        }
//        controler.Move(playerspeed * Time.deltaTime);// uses controler object for movement
//    }

//    private void CrouchyCrouchCrouch()
//    {
//         DefaultHt = isCrouching ? crouching : standing; // det crouch or stand  movement direction

//        controler.height = Mathf.Lerp(controler.height, DefaultHt, Time.deltaTime * 5f);// ht change for crouch

//        if (firstPersonCam != null) //helps control the child fps cam during jump and crouch
//        {
//            float DefaultCamY = isCrouching ? crouching * 0.5f : standing * 0.8f;
//            Vector3 newCamPos = firstPersonCam.transform.localPosition;
//            newCamPos.y = Mathf.Lerp(newCamPos.y, DefaultCamY, Time.deltaTime * 5f);
//            firstPersonCam.transform.localPosition = newCamPos;
//        }

//        if (isCrouching)
//        {
//            isSprinting = false; // S.E.
//        }
//    }

//    public void OnMove(InputAction.CallbackContext context)
//    {
//        moveInput = context.ReadValue<Vector2>();
//    }

//    public void OnSprint(InputAction.CallbackContext context)
//    {
//        isSprinting = context.performed || context.started;
//    }
//    public void OnJump(InputAction.CallbackContext context)
//    {
//        if (context.performed && controler.isGrounded)
//        {
//            playerspeed.y = Mathf.Sqrt(JumpHt * 2f * Mathf.Abs(Gravity));// because grav is neg in inspector, so use Mathf.Abs() to compensate
//        }
//    }

//    public void OnCrouch(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            isCrouching = !isCrouching;
//        }
//    }

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

//        firstPersonCam.transform.Rotate(Vector3.left * mouseY);

//        // Handle Yaw (Horizontal rotation)
//        FPCS.Rotate(Vector3.up * mouseX);
//    }

//     private void ClampXAxisRotation(float value)


//    {
//        Vector3 eulerRotation = firstPersonCam.transform.eulerAngles;
//        eulerRotation.x = value;
//        firstPersonCam.transform.eulerAngles = eulerRotation;
//    }







//}