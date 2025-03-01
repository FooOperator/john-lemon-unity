using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private InputAction moveAction;
    
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        playerMovement.SetMovementVector(moveAction.ReadValue<Vector2>());
    }
}
