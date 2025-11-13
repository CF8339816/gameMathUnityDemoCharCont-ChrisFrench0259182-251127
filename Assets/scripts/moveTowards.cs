using UnityEngine;

public class moveTowards : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float followRange;
    /*[SerializeField]*/ float speed;
    [SerializeField] float AccelSpeed;
    [SerializeField] float DecelSpeed;
    [SerializeField] float MinSpeed;
    [SerializeField] float MaxSpeed;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = MinSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        float TargetDistance = Vector3.Distance(transform.position, target.transform.position);

        



        if (TargetDistance <= followRange)
        {
            transform.LookAt(target);


            speed = Mathf.MoveTowards(speed, MaxSpeed, AccelSpeed * Time.deltaTime);


            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);


        }
        else
        {
            

            speed = Mathf.MoveTowards(speed, MinSpeed, DecelSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        }
        




    }
}
