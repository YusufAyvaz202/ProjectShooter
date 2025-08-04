using Managers;
using UnityEngine;
namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Rigidbody Settings")]
        private Rigidbody _rigidbody;
        
        [Header("Movement Settings")]
        [SerializeField] private Transform _orientationTransform;
        [SerializeField] private float _currentSpeed = 5f;
        private float _defaultSpeed = 5f;
        [SerializeField] private float jumpSpeed = 5f;
        private bool _isGrounded = true;
        private Vector2 _moveInput;

        [Header("Other References")]
        private PlayerAnimationController _playerAnimationController;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _rigidbody = GetComponent<Rigidbody>();
            _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
            SubscribeToEvents();
        }

        void FixedUpdate()
        {
            MovePlayer();
            //RotationPlayer();
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
                Vector3 moveDirection = _orientationTransform.forward * _moveInput.y + _orientationTransform.right * _moveInput.x;

                // Move the player
                _rigidbody.MovePosition(_rigidbody.position + moveDirection.normalized * (Time.fixedDeltaTime * _currentSpeed));
            }
            
            Vector2 animationInput = new Vector2(Mathf.Abs(_moveInput.x), Mathf.Abs(_moveInput.y));
            _playerAnimationController.SetMoveAnimation(animationInput.magnitude);
        }


        private void HandleJump()
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                _rigidbody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
                _playerAnimationController.PlayJumpAnimation();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }

        #region HelpersMethods

        public void SetSpeed(float speed)
        {
            _currentSpeed = speed;
        }

        public void ResetSpeed()
        {
            Invoke(nameof(ResetSpeedInvoke), 2f);   
        }
        
        private void ResetSpeedInvoke()
        {
            _currentSpeed = _defaultSpeed;
        }

        #endregion

        #region Initialization and Cleanup

        private void SubscribeToEvents()
        {
            EventManager.OnMovePerformed += HandleMove;
            EventManager.OnJumpPerformed += HandleJump;
            
            _defaultSpeed = _currentSpeed;
        }

        private void UnsubscribeFromEvents()
        {
            EventManager.OnMovePerformed -= HandleMove;
            EventManager.OnJumpPerformed -= HandleJump;
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion
    }
}