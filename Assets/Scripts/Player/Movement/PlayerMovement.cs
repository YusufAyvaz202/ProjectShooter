using Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbody Settings")]
    private Rigidbody _rigidbody;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 _moveInput;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 720f;
    private Vector2 _rotationInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SubscribeToEvents();
    }

    void FixedUpdate()
    {
        MovePlayer();
        Look();
    }

    private void HandleMove(Vector2 moveInput)
    {
        this._moveInput = moveInput;
    }

    private void MovePlayer()
    {
        if (_moveInput != Vector2.zero)
        {
            // Calculate the movement direction based on input
            //Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;

            // Move the player
            //transform.Translate(moveDirection * (moveSpeed * Time.fixedDeltaTime), Space.World);
            _rigidbody.MovePosition(transform.position + moveDirection * (Time.fixedDeltaTime * moveSpeed));
        }
    }
    
    private void HandleRotation(Vector2 rotationInput)
    {
        _rotationInput = rotationInput;
    }

    private void Look()
    {
        if (_rotationInput != Vector2.zero)
        {
            // Calculate the rotation based on input
            Quaternion targetRotation = Quaternion.Euler(0, _rotationInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
            _rigidbody.MoveRotation(_rigidbody.rotation * targetRotation);
        }
    }

    #region Initialization and Cleanup

    private void SubscribeToEvents()
    {
        EventManager.OnMovePerformed += HandleMove;
        EventManager.OnLookPerformed += HandleRotation; // Assuming you want to handle look input as well
    }

    private void UnsubscribeFromEvents()
    {
        EventManager.OnMovePerformed -= HandleMove;
        EventManager.OnLookPerformed -= HandleRotation;
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    #endregion
}
