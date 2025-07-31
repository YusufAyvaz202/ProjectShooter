using UnityEngine;
namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Reference Settings")]
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _orientationTransform;
        [SerializeField] private Transform _playerVisualTransform;
        [SerializeField] private Transform _combatLookAtTransform;

        [Header("Camera Settings")]
        [SerializeField] private float rotationSpeed;

        private void LateUpdate()
        {
            SetupCameraRotation();
        }

        private void SetupCameraRotation()
        {
            Vector3 directionToTarget =
                _combatLookAtTransform.position - new Vector3(transform.position.x, _combatLookAtTransform.position.y, transform.position.z);

            Vector3 smoothDirection = Vector3.Slerp(
                _orientationTransform.forward,
                directionToTarget.normalized,
                rotationSpeed * Time.deltaTime);

            _orientationTransform.forward = smoothDirection;
            _playerVisualTransform.forward = smoothDirection;
        }

    }
}