using Managers;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        [Header("Input Action References")]
        [SerializeField] private InputActionAsset inputActions;
    
        [Header("Action References")]
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;

        private void OnMovePerformed(InputAction.CallbackContext callbackContext)
        {
            // Read the move input from the callback context and pass it to the event manager
            Vector2 moveInput = callbackContext.ReadValue<Vector2>();
            EventManager.OnMovePerformed(moveInput);
        }
    
        private void OnLookPerformed(InputAction.CallbackContext callbackContext)
        {
            // Read the look input from the callback context and pass it to the event manager
            Vector2 lookInput = callbackContext.ReadValue<Vector2>();
            EventManager.OnLookPerformed(lookInput);
        }

        private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
        {
            EventManager.OnJumpPerformed();
        }

        #region Initialization and Cleanup
        /// <summary> Initializes the input controller by subscribing to input events. </summary>
        private void SubscribeToEvents()
        {
            _moveAction.started += OnMovePerformed;
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMovePerformed;
        
            _lookAction.started += OnLookPerformed;
            _lookAction.performed += OnLookPerformed;
            _lookAction.canceled += OnLookPerformed;

            _jumpAction.started += OnJumpPerformed;
            _jumpAction.performed += OnJumpPerformed;
            _jumpAction.canceled += OnJumpPerformed;
            
        }
    
        private void UnsubscribeFromEvents()
        {
            _moveAction.started -= OnMovePerformed;
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMovePerformed;
        
            _lookAction.started -= OnLookPerformed;
            _lookAction.performed -= OnLookPerformed;
            _lookAction.canceled -= OnLookPerformed;
            
            _jumpAction.started -= OnJumpPerformed;
            _jumpAction.performed -= OnJumpPerformed;
            _jumpAction.canceled -= OnJumpPerformed;
        }
    
        private void OnEnable()
        {
            if (inputActions != null)
            {
                inputActions.Enable();
            
                // Find the actions by their names defined in Consts.cs
                _moveAction = inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true);
                _lookAction = inputActions.FindAction(Consts.LOOK_ACTION, throwIfNotFound: true);
                _jumpAction = inputActions.FindAction(Consts.JUMP_ACTION, throwIfNotFound: true);
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
