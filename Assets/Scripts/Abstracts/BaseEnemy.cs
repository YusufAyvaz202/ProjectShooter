using Interfaces;
using Misc;
using Player.Movement;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
namespace Abstracts
{
    public abstract class BaseEnemy : MonoBehaviour, IAttackType, IPoolable
    {
        [Header("Enemy Settings")]
        [SerializeField] private EnemyDataSO myEnemyData;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        protected EnemyType enemyType;

        [Header("AI Settings")]
        private NavMeshAgent _navMeshAgent;
        private Transform _targetTransform;
        private Vector3 _destination;
        private float _attackRange = 5f;
        private float _attackCooldown = 2f;

        private void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Update()
        {
            if (_navMeshAgent == null || _targetTransform == null) return;
            
            // Update the destination to the target's position if it has changed
            _destination = _targetTransform.position;
            if (Vector3.Distance(transform.position, _destination) > 1f)
            {
                _navMeshAgent.SetDestination(_targetTransform.position);
            }

            // Check if the enemy is within attack range
            bool isInRange = Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange;
            _navMeshAgent.isStopped = isInRange;

            if (isInRange)
            {
                Attack();
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
            _attackCooldown = myEnemyData.attackCooldown;
            _attackRange = myEnemyData.attackRange;
            enemyType = myEnemyData.enemyType;

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetTransform = FindAnyObjectByType<PlayerMovement>().GetComponent<Transform>();
        }

        #endregion


    }
}