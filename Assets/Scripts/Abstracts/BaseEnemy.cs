using System.Collections;
using Ammunition;
using Interfaces;
using Managers;
using Misc;
using Player.Movement;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
namespace Abstracts
{
    public abstract class BaseEnemy : MonoBehaviour, IAttacker, IPoolable
    {
        [Header("Enemy Base Settings")]
        [SerializeField] private EnemyDataSO myEnemyData;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        private Collider _collider;
        public EnemyType enemyType;

        [Header("Enemy AI Settings")]
        private NavMeshAgent _navMeshAgent;
        private Transform _targetTransform;
        private Vector3 _destination;
        private float _attackRange;
        private readonly float _minMoveSensitivity = 1f;
        private float _attackCooldown;

        [Header("Enemy Attack Settings")]
        [SerializeField] private GameObject _bulletPrefab;

        [Header("Enemy Animation Settings")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float _deadAnimationDuration;
        private Coroutine _deadAnimationCoroutine;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        // Moves the enemy towards the target player.
        private void MoveToTarget()
        {
            if (_navMeshAgent == null || _targetTransform == null) return;

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

        public void Attack()
        {
            if (_navMeshAgent == null || _targetTransform == null) return;

            // Check if the attack cooldown has elapsed
            if (_attackCooldown <= 0f)
            {
                // Instantiate a bullet and set its position and direction
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.LookAt(_targetTransform.position);

                // Set the bullet's damage
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                bulletComponent.Damage = _damage;

                // Reset the attack cooldown
                _attackCooldown = myEnemyData.attackCooldown;
            }
            else
            {
                // Decrease the cooldown timer
                _attackCooldown -= Time.deltaTime;
            }
        }

        public void Spawn()
        {
            //throw new System.NotImplementedException();
        }
        public void Despawn()
        {
            //throw new System.NotImplementedException();
        }

        private void Die()
        {
            _animator.SetTrigger(Consts.ANIMATIONS_ENEMY_DEAD);
            _deadAnimationCoroutine = StartCoroutine(DeadAnimationEnd());
            _navMeshAgent.isStopped = true;
            _collider.enabled = false;
        }

        private IEnumerator DeadAnimationEnd()
        {
            yield return new WaitForSeconds(_deadAnimationDuration);
            EventManager.OnEnemyDie?.Invoke(this);
        }

        #region Initalize & Cleanup

        private void OnEnable()
        {
            _health = myEnemyData.health;
            _damage = myEnemyData.damage;
            _attackCooldown = myEnemyData.attackCooldown;
            _attackRange = myEnemyData.attackRange;
            enemyType = myEnemyData.enemyType;
            
            _collider = GetComponent<Collider>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetTransform = FindAnyObjectByType<PlayerMovementController>()?.GetComponent<Transform>();

            _navMeshAgent.stoppingDistance = _attackRange;
        }

        private void OnDisable()
        {
            if (_deadAnimationCoroutine is not null)
            {
                StopCoroutine(_deadAnimationCoroutine);
            }
        }

        #endregion

    }
}