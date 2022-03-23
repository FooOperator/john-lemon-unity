using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VerticalMovement
{
    None,
    Up,
    Down,
}
public enum HorizontalMovement
{
    None, 
    Left,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> Walking;
    public event Action<Rigidbody, Vector3, Quaternion> WhilstWalking;

    [SerializeField] private Player player;

    public Rigidbody rb;

    private Vector3 movementVector;
    private Quaternion rotation = Quaternion.identity;

    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float movementSpeed = 4f;

    private float verticalDirection;
    private float horizontalDirection;

    [SerializeField] private HorizontalMovement horizontalMovement;
    [SerializeField] private VerticalMovement verticalMovement;
    
    [SerializeField] private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player.WalkPerformed += SetMovementVector;
    }

    void Update()
    {
        SetHorizontalMovement();
        SetVerticalMovement();
        CheckIfIsWalking();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementVector, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        OnWalking(isWalking);
        OnWhilstWalking(rb, movementVector, rotation);
    }

    private void SetMovementVector(Vector2 directionVector)
    {
        horizontalDirection = directionVector.x;
        verticalDirection = directionVector.y;

        movementVector.Set(-horizontalDirection * movementSpeed, 0f, -verticalDirection * movementSpeed);
        movementVector.Normalize();
    }
    private void SetVerticalMovement()
    {
        switch (verticalDirection)
        {
            case -1:
                verticalMovement = VerticalMovement.Down;
                break;
            case 1:
                verticalMovement = VerticalMovement.Up;
                break;
            default:
                verticalMovement = VerticalMovement.None;
                break;
        }
    }
    private void SetHorizontalMovement()
    {
        switch (horizontalDirection)
        {
            case -1:
                horizontalMovement = HorizontalMovement.Left;
                break;
            case 1:
                horizontalMovement = HorizontalMovement.Right;
                break;
            default:
                horizontalMovement = HorizontalMovement.None;
                break;
        }
    }
    private void CheckIfIsWalking()
    {
        isWalking = verticalMovement != VerticalMovement.None || horizontalMovement != HorizontalMovement.None;
    }

    protected virtual void OnWhilstWalking(Rigidbody rb, Vector3 movementVector, Quaternion rotation)
    {
        if (WhilstWalking != null)
        {
            WhilstWalking(rb, movementVector, rotation);
        }
    }
    protected virtual void OnWalking (bool value)
    {
        if (Walking != null)
        {
            Walking(value);
        }
    }
}

