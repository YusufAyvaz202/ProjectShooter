using Interfaces;
using Managers;
using Misc;
using Player.Movement;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
namespace Abstracts
{
    public abstract class BaseEnemy : MonoBehaviour, IAttacker, IPoolable, IAttackable
    {
        [Header("Enemy Data")]
        [SerializeField] protected EnemyDataSO myEnemyData;
        
        [Header("References")]
        protected NavMeshAgent _navMeshAgent;
        protected Transform _targetTransform;
        private Collider _collider;

        [Header("Attack Settings")]
        protected float _attackCooldown;
        protected float _attackRange;
        protected float _damage; // this can be removed because damage comes from Guns.

        [Header("Health Settings")]
        [SerializeField] protected float _health;
        public EnemyType enemyType;

        [Header("Enemy Move Settings")]
        protected float _minMoveSensitivity;
        private Vector3 _destination;

        [Header("Enemy Animation Settings")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float _deadAnimationDuration;

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Enemy took damage: " + damage + " Current Health: " + _health);
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        // Moves the enemy towards the target player.
        private void MoveToTarget()
        {
            if (_navMeshAgent == null || _targetTransform == null) return;
            transform.LookAt(_targetTransform);

            // Update the destination to the target's position if it has changed
            _destination = _targetTransform.position;
            if (Vector3.Distance(transform.position, _destination) > _minMoveSensitivity)
            {
                _navMeshAgent.SetDestination(_targetTransform.position);
                _animator.SetFloat(Consts.ANIMATIONS_ENEMY_MOVE_SPEED, _navMeshAgent.velocity.magnitude);
            }

            if (Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange)
            {
                Attack();
            }
        }

        public abstract void Attack();

        public void Spawn()
        {
            //TODO: Check spawn position and rotation 
        }
        public void Despawn()
        {
            //TODO: Make a score System with EventManager
        }

        private void Die()
        {
            _animator.SetTrigger(Consts.ANIMATIONS_ENEMY_DEAD);
            Invoke(nameof(DieAnimationEnd), _deadAnimationDuration);
            _navMeshAgent.isStopped = true;
            _collider.enabled = false;
        }

        private void DieAnimationEnd()
        {
            EventManager.OnEnemyDie(this);
        }

        #region Initialize & Cleanup

        private void OnEnable()
        {
            _health = myEnemyData.health;
            _damage = myEnemyData.damage;
            _attackRange = myEnemyData.attackRange;
            _attackCooldown = myEnemyData.attackCooldown;
            _minMoveSensitivity = myEnemyData.minMoveSensitivity;
            enemyType = myEnemyData.enemyType;

            _collider = GetComponent<Collider>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetTransform = FindAnyObjectByType<PlayerMovementController>()?.GetComponent<Transform>();

            _collider.enabled = true;
            _navMeshAgent.stoppingDistance = _attackRange;
        }

        #endregion

    }
}