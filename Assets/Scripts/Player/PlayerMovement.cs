using UnityEngine.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region enums

    public enum HorizontalMovement
    {
        None,
        Left,
        Right
    }

    public enum VerticalMovement
    {
        None,
        Up,
        Down,
    }

    #endregion

    public CharacterController cc;
    public UnityEvent StartedMoving = new();
    public UnityEvent StoppedMoving = new();

    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private HorizontalMovement horizontalMovement;
    [SerializeField] private VerticalMovement verticalMovement;

    private Vector3 movementVector;
    private Quaternion rotation;
    private Camera mainCamera;

    private bool isWalking;
    private Vector3 desiredForward;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var wasWalking = isWalking;

        CheckIfIsWalking();
        SetHorizontalMovement();
        SetVerticalMovement();

        var moveInput = GetTransformedMoveInput();
        GetPlayerRotation(moveInput);

        cc.Move(moveInput * (movementSpeed * Time.deltaTime));
        transform.rotation = rotation;

        if (wasWalking && !isWalking)
        {
            StoppedMoving.Invoke();
        }
        else if (!wasWalking && isWalking)
        {
            StartedMoving.Invoke();
        }
    }

    private Vector3 GetTransformedMoveInput()
    {
        var transformedMovementInput = mainCamera.transform.TransformDirection(movementVector);
        transformedMovementInput = new(transformedMovementInput.x, 0f, transformedMovementInput.y);
        return transformedMovementInput;
    }

    private void GetPlayerRotation(Vector3 moveInput)
    {
        desiredForward =
            Vector3.RotateTowards(transform.forward, moveInput, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    public void SetMovementVector(Vector2 directionVector)
    {
        movementVector.Set(directionVector.x, 0f, directionVector.y);
    }

    private void SetVerticalMovement()
    {
        verticalMovement = movementVector.z switch
        {
            < 0 => VerticalMovement.Down,
            > 0 => VerticalMovement.Up,
            _ => VerticalMovement.None
        };
    }

    private void SetHorizontalMovement()
    {
        horizontalMovement = movementVector.x switch
        {
            < 0 => HorizontalMovement.Left,
            > 0 => HorizontalMovement.Right,
            _ => HorizontalMovement.None
        };
    }

    private void CheckIfIsWalking()
    {
        isWalking = verticalMovement != VerticalMovement.None || horizontalMovement != HorizontalMovement.None;
    }
}