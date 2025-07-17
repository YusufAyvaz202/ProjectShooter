using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AmmunitionData", menuName = "ScriptableObjects/AmmunitionData")]
    public class AmmunitionDataSO : ScriptableObject
    {
        public float speed;
        public  float damage;
        public  float lifeTime;
    }
}