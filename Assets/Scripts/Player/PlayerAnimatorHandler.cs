using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private string bool_isWalking = "isWalking";

    private void OnEnable()
    {
        playerMovement.StartedMoving.AddListener(OnStartedWalking);
        playerMovement.StoppedMoving.AddListener(OnStoppedWalking);
    }

    private void OnDisable()
    {
        playerMovement.StartedMoving.RemoveListener(OnStartedWalking);
        playerMovement.StoppedMoving.RemoveListener(OnStoppedWalking);
    }

    private void OnStartedWalking()
    {
        animator.SetBool(bool_isWalking, true);
    }

    private void OnStoppedWalking()
    {
        animator.SetBool(bool_isWalking, false);
    }
}