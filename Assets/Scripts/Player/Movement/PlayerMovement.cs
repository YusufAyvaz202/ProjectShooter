using Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbody Settings")]
    private Rigidbody _rigidbody;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 720f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SubscribeToEvents();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleMove(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    private void MovePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            // Calculate the movement direction based on input
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            // Move the player
            //transform.Translate(moveDirection * (moveSpeed * Time.fixedDeltaTime), Space.World);
            _rigidbody.MovePosition(transform.position + moveDirection * (Time.fixedDeltaTime * moveSpeed));
        }
    }

    #region Initialization and Cleanup

    private void SubscribeToEvents()
    {
        EventManager.OnMovePerformed += HandleMove;
    }

    private void UnsubscribeFromEvents()
    {
        EventManager.OnMovePerformed -= HandleMove;
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    #endregion
}
