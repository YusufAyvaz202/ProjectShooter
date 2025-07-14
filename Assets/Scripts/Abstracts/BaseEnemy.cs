using Interfaces;
using Misc;
using ScriptableObjects;
using UnityEngine;
namespace Abstracts
{
    public abstract class BaseEnemy : MonoBehaviour, IAttackType, IPoolable
    {
        [Header("Enemy Settings")]
        [SerializeField] private EnemyDataSO myEnemyData;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        protected EnemyType enemyType;

        private void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Spawn()
        {
            //throw new System.NotImplementedException();
        }
        public void Despawn()
        {
            throw new System.NotImplementedException();
        }

        private void Die()
        {
            throw new System.NotImplementedException();
        }

        #region Initalize & Cleanup

        private void OnEnable()
        {
            _health = myEnemyData.health;
            _damage = myEnemyData.damage;
            enemyType = myEnemyData.enemyType;
        }

        #endregion


    }
}