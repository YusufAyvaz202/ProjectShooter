using Abstracts;
using Misc;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Collectible Pool Data", menuName = "ScriptableObjects/CollectiblePoolSO")]
    public class CollectiblePoolSO : ScriptableObject
    {
        public BaseCollectible baseCollectiblePrefab;
        public CollectibleType collectibleType;
        public int initialSize;
    }
}