using UnityEngine;
using UnityEngine.InputSystem.XR;

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




    void Start()
    {
        speed = MinSpeed;
        controler = GetComponent<CharacterController>();


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


        inputVector.Normalize();

        transform.Translate(new Vector3(inputVector.x, inputVector.y, inputVector.z) * speed * Time.deltaTime);


        // Check if grounded and reset vertical velocity
        
    }
}

