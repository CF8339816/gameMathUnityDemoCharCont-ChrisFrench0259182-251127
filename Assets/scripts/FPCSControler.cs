using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using static UnityEditorInternal.ReorderableList;

public class FPCSControler : MonoBehaviour
{
    // had exponential accellerationn
    // andspeed  in oldewr  code . so read
    // more on controler and looked at a
    // few more tutoruials and rewrote the code  



    CharacterController controler;
    [SerializeField] Camera firstPersonCam;
    [SerializeField] Transform target;

    [SerializeField] float speed;
    [SerializeField] float AccelSpeed = 4.0f;
    [SerializeField] float DecelSpeed = 6.0f;
    [SerializeField] float MinSpeed = 5.0f;
    [SerializeField] float MaxSpeed = 10.0f;
    //[SerializeField] float Gravity = 9.81f; //meters per second squared gravity coeficcent
    [SerializeField] float Gravity = -15.0f;  //gravity valiue reccomended in turtorial 
    [SerializeField] float JumpHt = 2.0f;
    [SerializeField] float JumpDis;
    [SerializeField] float SprintMultiplier;
    [SerializeField] float standing = 2.0f;
    [SerializeField] float crouching = 1.0f;

       float DefaultHt;

    Vector3 playerspeed;
    Vector2 moveInput;

    bool isSprinting;
    bool isCrouching;

    void Start()
    {

        DefaultHt = standing;
        speed = MinSpeed;
        controler = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; // locks and hides cursor in thecenter of the screen
    }

    // Update is called once per frame
    void Update()
    {
        CrouchyCrouchCrouch();
        MoveyMoveMove();
        JumpyJumpJump();
    }

    private void MoveyMoveMove()
    {
        float targetSpeed = isSprinting ? MaxSpeed : MinSpeed;

        if (moveInput == Vector2.zero)
        {
            speed = Mathf.MoveTowards(speed, 0, DecelSpeed * Time.deltaTime); //handles no keyinput
        }
        else
        {
            speed = Mathf.MoveTowards(speed, targetSpeed, AccelSpeed * Time.deltaTime); // acceleration
        }
               
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;// Creates mvmnt rel to plyr fwd

        //setting upmovement  speed
        playerspeed.x = moveDirection.x * speed;
        playerspeed.z = moveDirection.z * speed;
    }

    private void JumpyJumpJump()
    {
        if (!controler.isGrounded)// applies gravity if not on ground
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
        controler.Move(playerspeed * Time.deltaTime);// uses controler object for movement
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
        if (context.performed && controler.isGrounded)
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