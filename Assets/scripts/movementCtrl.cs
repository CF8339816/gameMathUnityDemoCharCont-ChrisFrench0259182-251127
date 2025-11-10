using NUnit;
using UnityEngine;

public class movementCtrl : MonoBehaviour
{


    [SerializeField] GameObject FirstPerContGrp;
    [SerializeField]  float forward = 5f;
    [SerializeField]  float back = 5f;
    [SerializeField]  float left = 5f;
    [SerializeField]  float right = 5f;
    static float fwd = forward;
     static float bak =back;
     static float lft = left;
     static float rgt= right;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    static void movement(Transform transform)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * fwd * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left* lft * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * bak * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * rgt * Time.deltaTime);
        }
    }







}
