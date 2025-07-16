using Managers;
using UnityEngine;
namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Reference Settings")]
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _orientationTransform;
        [SerializeField] private Transform _playerVisualTransform;

        [Header("Camera Settings")]
        [SerializeField] private float rotationSpeed;
        private float horizontalAngle;
        private float verticalAngle;

        private void Update()
        {
            SetupCameraRotation();
        }
        private void SetupCameraRotation()
        {
            Vector3 viewDirection
                = _target.position - new Vector3(transform.position.x, _target.transform.position.y, transform.position.z);

            _orientationTransform.forward = viewDirection.normalized;

            Vector3 inputDirection
                = _orientationTransform.forward * verticalAngle + _orientationTransform.right * horizontalAngle;

            if (inputDirection != Vector3.zero)
            {
                _playerVisualTransform.forward
                    = Vector3.Slerp(_playerVisualTransform.forward, inputDirection, rotationSpeed * Time.deltaTime);
            }
        }

        private void ReadMoveInput(Vector2 moveInput)
        {
            horizontalAngle = moveInput.x;
            verticalAngle = moveInput.y;
        }

        #region Initalize & Cleanup

        private void OnEnable()
        {
            EventManager.OnMovePerformed += ReadMoveInput;
        }

        private void OnDisable()
        {
            EventManager.OnMovePerformed -= ReadMoveInput;
        }

        #endregion
    }
}