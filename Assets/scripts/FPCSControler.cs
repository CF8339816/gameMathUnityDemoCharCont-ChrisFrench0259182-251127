using UnityEngine;

public class FPCSControler : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followRange;
    [SerializeField]  float speed;
    [SerializeField] float AccelSpeed;
    [SerializeField] float DecelSpeed;
    [SerializeField] float MinSpeed;
    [SerializeField] float MaxSpeed;
    //float m_Height;



    void Start()
    {
        speed = MinSpeed;



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
            inputVector.y += 2;
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

    }
}
