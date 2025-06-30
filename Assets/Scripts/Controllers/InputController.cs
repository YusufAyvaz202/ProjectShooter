using Managers;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Input Action References")]
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        // Read the move input from the callback context and pass it to the event manager
        Vector2 moveInput = callbackContext.ReadValue<Vector2>();
        EventManager.OnMovePerformed(moveInput);
    }


    #region Initialization and Cleanup
    /// <summary> Initializes the input controller by subscribing to input events. </summary>
    private void SubscribeToEvents()
    {
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).started += OnMovePerformed;
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).performed += OnMovePerformed;
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).canceled += OnMovePerformed;
    }
    
    private void UnsubscribeFromEvents()
    {
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).started -= OnMovePerformed;
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).performed -= OnMovePerformed;
        inputActions.FindAction(Consts.MOVE_ACTION, throwIfNotFound: true).canceled -= OnMovePerformed;
    }
    
    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Enable();
        }
        else
        {
            Debug.LogWarning("InputActionAsset is not assigned in the InputController.");
        }
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
