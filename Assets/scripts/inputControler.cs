using UnityEngine;
using UnityEngine.InputSystem;
public class inputcontroler : MonoBehaviour
{

    [SerializeField] playercontroler CharacterController;
    [SerializeField] camLookControler CameraController;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");  // adds the look action to the input controler script to assist binding , not that it works... fml

        lookAction = InputSystem.actions.FindAction("Look"); //                           ||
        jumpAction = InputSystem.actions.FindAction("Jump"); //                          ||
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        //Vector2 lookVector = lookAction.ReadValue<Vector2>();
        //CharacterController.Rotate(InputAction.CallbackContext context);

        //Vector2 jumpVector = jumpAction.ReadValue<Vector2>();
        //CharacterController.OnJump(movementVector);

    }
}
