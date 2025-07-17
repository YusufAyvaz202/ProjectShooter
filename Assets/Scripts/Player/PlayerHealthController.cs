using Interfaces;
using UnityEngine;
namespace Player
{
    public class PlayerHealthController : MonoBehaviour, IAttackable
    {
        [Header("Player Health Settings")]
        [SerializeField] private float _health = 100f;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player has died.");
        }
    }

}