using UnityEngine;
using UnityEngine.InputSystem;
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

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(movementVector);



    }
}
