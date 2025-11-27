using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class inputcontroler : MonoBehaviour
{

   [SerializeField] playercontroler CharacterController;

    InputAction moveAction;
    InputAction lookAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(movementVector);



    }
}
