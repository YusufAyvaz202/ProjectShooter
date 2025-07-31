using Interfaces;
using UnityEngine;
namespace Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [Header("Player Interaction Settings")]
        private PlayerHealthController _playerHealthController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectible collectible))
            {
                collectible.Collect(this);
            }
        }

        #region HelpersMethods

        public void MakeHealthPickup(float healthAmount)
        {
            _playerHealthController.SetHealth(healthAmount);
        }

        #endregion


        #region Initialize & Cleanup

        private void OnEnable()
        {
            _playerHealthController = GetComponent<PlayerHealthController>();
        }

        #endregion
    }
}