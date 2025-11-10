using UnityEngine;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "New Simple Event Definition", menuName = "RTSI/ScriptableObjects/Events/Simple Event Definition", order = 1)]
    public class SimpleEventDefinition : EventDefinitionBase
    {
        [field:SerializeField] public string ButtonText { get; private set; }
        [field:SerializeField] public string EventText { get; private set; }
    }
}