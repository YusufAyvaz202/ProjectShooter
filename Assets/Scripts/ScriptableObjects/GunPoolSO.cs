using Abstracts;
using Misc;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Gun Pool Data", menuName = "ScriptableObjects/GunPoolSO")]
    public class GunPoolSO : ScriptableObject
    {
        public BaseGun baseGunPrefab;
        public GunType gunType;
        public int initialSize = 10;
    }
}