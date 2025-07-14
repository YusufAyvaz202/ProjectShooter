using Abstracts;
using Misc;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Pool Data")]
    public class EnemyPoolSO : ScriptableObject
    {
        public BaseEnemy baseEnemyPrefab;
        public EnemyType enemyType;
        public int initialSize;
    }
}