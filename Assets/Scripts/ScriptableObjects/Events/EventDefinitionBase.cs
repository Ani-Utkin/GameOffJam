using UnityEngine;

namespace ScriptableObjects.Events
{
    
    
    public abstract class EventDefinitionBase : ScriptableObject
    {
        [field:SerializeField] public Sprite EventSprite { get; private set; }
    }
}