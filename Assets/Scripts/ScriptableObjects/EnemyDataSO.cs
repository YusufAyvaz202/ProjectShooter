using Misc;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public float health;
        public float damage;
        public float attackRange;
        public float attackCooldown;
        public float minMoveSensitivity = 1f;
        public EnemyType enemyType;
    }
}