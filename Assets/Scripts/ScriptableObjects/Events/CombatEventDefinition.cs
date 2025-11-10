using ScriptableObjects.Mobs;
using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New Combat Event Definition", menuName = "RTSI/ScriptableObjects/Events/Combat Event Definition", order = 2)]
    public class CombatEventDefinition : EventDefinitionBase
    {
        [field:SerializeField] public MobDefinitionBase MobDefinition { get; private set; }
    }
}