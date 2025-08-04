using Managers;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        [Header("Input Action Asset References")]
        [SerializeField] private InputActionAsset inputActions;

        [Header("Action References")]
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _throwAction;

        #region MovementPerforms

        private void OnMovePerformed(InputAction.CallbackContext callbackContext)
        {
            // Read the move input from the callback context and pass it to the event manager
            Vector2 moveInput = callbackContext.ReadValue<Vector2>();
            EventManager.OnMovePerformed(moveInput);
        }


        private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
        {
            EventManager.OnJumpPerformed();
        }

        #endregion

        #region AttackPerforms

        private void OnAttackPerformed(InputAction.CallbackContext callbackContext)
        {
            EventManager.OnAttackPerformed();
        }

        private void ThrowActionPerformed(InputAction.CallbackContext callbackContext)
        {
            EventManager.ThrowActionPerformed?.Invoke();
        }

        #endregion

        #region Initialization and Cleanup

        /// <summary> Initializes the input controller by subscribing to input events. </summary>
        private void SubscribeToEvents()
        {
            _moveAction.started += OnMovePerformed;
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMovePerformed;


            _jumpAction.started += OnJumpPerformed;
            _jumpAction.performed += OnJumpPerformed;
            _jumpAction.canceled += OnJumpPerformed;

            //_attackAction.started += OnAttackPerformed;
            _attackAction.performed += OnAttackPerformed;
            //_attackAction.canceled += OnAttackPerformed;
            
            _throwAction.performed += ThrowActionPerformed;
        }

        private void UnsubscribeFromEvents()
        {
            _moveAction.started -= OnMovePerformed;
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMovePerformed;


            _jumpAction.started -= OnJumpPerformed;
            _jumpAction.performed -= OnJumpPerformed;
            _jumpAction.canceled -= OnJumpPerformed;

            //_attackAction.started -= OnAttackPerformed;
            _attackAction.performed -= OnAttackPerformed;
            //_attackAction.canceled -= OnAttackPerformed;
            
            _throwAction.performed -= ThrowActionPerformed;
        }

        private void OnEnable()
        {
            if (inputActions != null)
            {
                inputActions.Enable();

                // Find the actions by their names defined in Consts.cs
                _moveAction = inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true);
                _jumpAction = inputActions.FindAction(Consts.JUMP_ACTION, throwIfNotFound: true);
                _attackAction = inputActions.FindAction(Consts.ATTACK_ACTION, throwIfNotFound: true);
                _throwAction = inputActions.FindAction(Consts.THROW_ACTION, throwIfNotFound: true);
            }
            else
            {
                Debug.LogWarning("InputActionAsset is not assigned in the InputController.");
            }

            SubscribeToEvents();
        }
        private void OnDisable()
        {
            if (inputActions != null)
            {
                inputActions.Disable();
            }
            else
            {
                Debug.LogWarning("InputActionAsset is not assigned in the InputController.");
            }

            UnsubscribeFromEvents();
        }

        #endregion


    }
}