using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.ProBuilder.MeshOperations;

public class FPCSControler : MonoBehaviour
{
    [SerializeField] Transform target;
   // [SerializeField] float followRange;
    [SerializeField]  float speed;
    Vector3 playerspeed;
    [SerializeField] float AccelSpeed;
    [SerializeField] float DecelSpeed;
    [SerializeField] float MinSpeed;
    [SerializeField] float MaxSpeed;
    //float m_Height;
    CharacterController controler;
    [SerializeField] float Gravity = 9.81f; //meters per second squared gravity coeficcent
    [SerializeField] float JumpHt;
    [SerializeField] float JumpDis;
    [SerializeField] float SprintMultiplier;
    [SerializeField] float mouseSensitivity;
    //private float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    float mouseX;
    [SerializeField] float rotationX = 0f;

    void Start()
    {
        speed = MinSpeed;
        controler = GetComponent<CharacterController>();
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;


    }

  

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = Vector3.zero;



        if (Input.GetKey(KeyCode.W))
        {
           
                speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
                inputVector.z += speed;
            
        }
        if (Input.GetKey(KeyCode.S))
        {

            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.z -= speed;

        }
        if (Input.GetKey(KeyCode.A))
        {

            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {

            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.x += speed;
        }

        //while ((Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.RightShift)))
        //{

        //    speed = 2 * Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
        //inputVector.x += speed;
        //inputVector.x -= speed;
        //inputVector.y += speed;
        //inputVector.y -= speed;

        //}


        if (Input.GetKey(KeyCode.Space))
        {
            //inputVector.y += 2;
            if (controler.isGrounded && playerspeed.y < 0)
            {
                playerspeed.y = -2f; // Small downward force to keep grounded
            }


            playerspeed.y += Gravity * Time.deltaTime; // Apply gravity


            Vector3 moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");// Apply movement (including gravity)
            controler.Move((moveDirection * speed + playerspeed) * Time.deltaTime);


            if (Input.GetKey(KeyCode.Space) && controler.isGrounded) //smoothing logic
            {
                playerspeed.y = Mathf.Sqrt(JumpHt * -2f * Gravity);
            }


        }   


        //else
        //{
        //    inputVector.y = 0;
        //}

        if ((Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)))  //allows for either ctrl key to be used for crouch since the rubric did not specify left or right
        {
            inputVector.y -= .5F;

        }


        //while ((Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)))  //allows for either ctrl key to be used for crouch since the rubric did not specify left or right
        // {
        //     //inputVector.y -= .5F;

        //     //CharacterController.m_Height= ;
        // }
        //else
        //{
        //    inputVector.y += .5F;
        //}


        if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.RightShift)))
        {
            
               // speed = SprintMultiplier * speed;
                MaxSpeed = SprintMultiplier * MaxSpeed;
               // AccelSpeed = SprintMultiplier * AccelSpeed;
                speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
                inputVector.z += speed;
            

        }
       
        //while ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.RightShift)))
        //{

        //    speed = SprintMultiplier * speed;
        //    MaxSpeed = SprintMultiplier * MaxSpeed;
        //    AccelSpeed = SprintMultiplier * AccelSpeed;
        //    speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
        //    inputVector.z += speed;

        //}

        if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.RightShift)))
        {

            speed = SprintMultiplier * speed;
            MaxSpeed = SprintMultiplier * MaxSpeed;
            AccelSpeed = SprintMultiplier * AccelSpeed;
            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.z -= speed;
                     
        }

        if ((Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.RightShift)))
        {

            speed = SprintMultiplier * speed;
            MaxSpeed = SprintMultiplier * MaxSpeed;
            AccelSpeed = SprintMultiplier * AccelSpeed;
            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.x += speed;

        }      

        if ((Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.RightShift)))
        {

            speed = SprintMultiplier * speed;
            MaxSpeed = SprintMultiplier * MaxSpeed;
            AccelSpeed = SprintMultiplier * AccelSpeed;
            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);
            inputVector.x -= speed;

         }

        //if (mouseX != 0)
        //{
        //   // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        //    transform.Rotate(new Vector3(0, mouseX, 0 ));

        //}

        inputVector.Normalize();

        transform.Translate(new Vector3(inputVector.x, inputVector.y, inputVector.z) * speed * Time.deltaTime);


        // Check if grounded and reset vertical velocity
        
    }
}

