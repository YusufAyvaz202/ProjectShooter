using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunDataSO")]
    public class GunDataSO : ScriptableObject
    {
        public GameObject ammunitionPrefab;
        public int initialSize;
    }
}