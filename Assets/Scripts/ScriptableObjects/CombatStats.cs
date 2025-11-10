using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Combat Stats", menuName = "RTSI/ScriptableObjects/CombatStats", order = 1)]
    public class CombatStats : ScriptableObject
    {
        [field:SerializeField] public int MaxHealth { get; private set; }
        [field:SerializeField] public int Attack { get; private set; }
        [field:SerializeField] public int Dodge { get; private set; }
        [field: SerializeField] public int Speed { get; private set; }
    }
}