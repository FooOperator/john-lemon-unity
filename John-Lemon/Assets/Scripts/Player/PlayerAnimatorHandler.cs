using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private string bool_isWalking = "isWalking";
    private Rigidbody m_rb;
    private Vector3 m_movementVector;
    private Quaternion m_rotation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();


        playerMovement.Walking += SetIsWalkingBool;
        playerMovement.WhilstWalking += SetForOnAnimatorMove;
    }

    private void SetIsWalkingBool(bool value)
    {
        animator.SetBool(bool_isWalking, value);
    }

    private void SetForOnAnimatorMove(Rigidbody rb, Vector3 movementVector, Quaternion rotation)
    {
        m_rb = rb;
        m_movementVector = movementVector;
        m_rotation = rotation;
    }

    private void OnAnimatorMove()
    {

        m_rb.MovePosition(m_rb.position + m_movementVector * animator.deltaPosition.magnitude);
        m_rb.MoveRotation(m_rotation);
    }
}
