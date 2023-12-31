using UnityEngine;

namespace Collectables
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public int id;
        public string displayName;
        public Sprite icon;
    }
}
