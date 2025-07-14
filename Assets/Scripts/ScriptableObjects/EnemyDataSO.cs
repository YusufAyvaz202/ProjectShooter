using Misc;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public float health;
        public float damage;
        public EnemyType enemyType;
    }
}