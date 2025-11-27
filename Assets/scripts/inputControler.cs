using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class inputcontroler : MonoBehaviour
{

   [SerializeField] playercontroler CharacterController;
    [SerializeField] camLookControler CameraController;
   public InputAction moveAction;
   public InputAction lookAction;


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

        Vector3 movementVector = moveAction.ReadValue<Vector3>();
        CharacterController.Move(movementVector);

        Vector3 lookVector = lookAction.ReadValue<Vector3>();
        CharacterController.Rotate(movementVector);



    }
}
