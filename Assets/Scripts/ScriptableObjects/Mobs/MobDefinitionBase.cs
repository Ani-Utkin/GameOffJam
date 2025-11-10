using UnityEngine;

namespace ScriptableObjects.Mobs
{
    [CreateAssetMenu(fileName = "MobDefinitionBase", menuName = "RTSI/ScriptableObjects/Mobs/MobDefinitionBase", order = 1)]
    public class MobDefinitionBase : ScriptableObject
    {
        [field:SerializeField] public Sprite MobSprite { get; private set; }
        [field:SerializeField] public CombatStats CombatStats { get; private set; }
    }
}