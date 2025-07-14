using System;
using System.Collections.Generic;
using Abstracts;
using Misc;
using Object_Pooling;
using UnityEngine;
namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        private Dictionary<EnemyType, ObjectPool<BaseEnemy>> enemyPools;
        

        #region Initalize & Cleanup

        private void OnEnable()
        {
            enemyPools = new Dictionary<EnemyType, ObjectPool<BaseEnemy>>();
        }

        #endregion

    }
}