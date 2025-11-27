using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroler : MonoBehaviour
{

    public CharacterController characterController;
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float SprintSpeed = 15f;
    [SerializeField] float CrouchSpeed = 5f;
    [SerializeField] float RotateSpeed = 5f;
    float RotationY;

 
   
   
    //code  recycled from previous  attempts to save  time above here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
      
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

}
  