using Managers;
using UnityEngine;

namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Rigidbody Settings")]
        private Rigidbody _rigidbody;
        
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpSpeed = 5f;
        private bool _isGrounded = true;
        private Vector2 _moveInput;

        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 720f;
        private Vector2 _rotationInput;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _rigidbody = GetComponent<Rigidbody>();
            SubscribeToEvents();
        }

        void FixedUpdate()
        {
            MovePlayer();
            RotationPlayer();
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
                Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;

                // Move the player
                _rigidbody.MovePosition(transform.position + moveDirection.normalized * (Time.fixedDeltaTime * moveSpeed));
            }

            EventManager.PlayerMoveAnimationParameterChanged(Mathf.Abs(_moveInput.x) + Mathf.Abs(_moveInput.y));
        }

        private void HandleRotation(Vector2 rotationInput)
        {
            _rotationInput = rotationInput;
        }

        private void RotationPlayer()
        {
            if (_rotationInput != Vector2.zero)
            {
                // Calculate the rotation based on input
                Quaternion targetRotation = Quaternion.Euler(0, _rotationInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
                _rigidbody.MoveRotation(_rigidbody.rotation * targetRotation);
            }
        }

        private void HandleJump()
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                _rigidbody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }

        #region Initialization and Cleanup

        private void SubscribeToEvents()
        {
            EventManager.OnMovePerformed += HandleMove;
            EventManager.OnLookPerformed += HandleRotation; // Assuming you want to handle look input as well
            EventManager.OnJumpPerformed += HandleJump;
        }

        private void UnsubscribeFromEvents()
        {
            EventManager.OnMovePerformed -= HandleMove;
            EventManager.OnLookPerformed -= HandleRotation;
            EventManager.OnJumpPerformed -= HandleJump;
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion
    }
}