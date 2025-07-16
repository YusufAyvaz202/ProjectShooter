using UnityEngine;
namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Player Health Settings")]
        [SerializeField] private float _health = 100f; // Player's health

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
            }
        }

        private void Die()
        {
            Debug.Log("Player has died.");
        }
    }

}