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




    void Start()
    {
    }

  

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.z += 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.z -= 1;

        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            inputVector.y += 2;
        }
        //else
        //{
        //    inputVector.y = 0;
        //}
        while ((Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)))  //allows for either ctrl key to be used for crouch since the rubric did not specify left or right
        {
            inputVector.y -= .5f;
        }
        //else
        //{
        //    inputVector.y += .5F;
        //}





        inputVector.Normalize();

        transform.Translate(new Vector3(inputVector.x, inputVector.y, inputVector.z) * speed * Time.deltaTime);

    }
}
