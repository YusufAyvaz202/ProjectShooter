using Interfaces;
using Misc;
using UnityEngine;
namespace Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [Header("Player Interaction Settings")]
        private PlayerHealthController _playerHealthController;
        private PlayerMovementController _playerMovementController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectible collectible))
            {
                collectible.Collect(OnCollectibleCollected);
            }
        }
        
        private void OnCollectibleCollected(CollectibleType collectibleType)
        {
            switch (collectibleType)
            {
                case CollectibleType.Health:
                    Debug.Log("Health Collected");
                    MakeHealthPickup(Random.Range(5, 20)); 
                    break;
                case CollectibleType.Speed:
                    Debug.Log("Speed Collected");
                    MakeSpeedPickup(10f);
                    break;
                default:
                    Debug.LogWarning($"Unhandled collectible type: {collectibleType}");
                    break;
            }
        }

        #region HelpersMethods

        private void MakeHealthPickup(float healthAmount)
        {
            _playerHealthController.SetHealth(healthAmount);
        }
        
        private void MakeSpeedPickup(float speedAmount)
        {
            _playerMovementController.SetSpeed(speedAmount);
            _playerMovementController.ResetSpeed(); 
        }

        #endregion


        #region Initialize & Cleanup

        private void OnEnable()
        {
            _playerHealthController = GetComponent<PlayerHealthController>();
            _playerMovementController = GetComponent<PlayerMovementController>();
        }

        #endregion
    }
}