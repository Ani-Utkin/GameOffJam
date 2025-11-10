using System;
using ScriptableObjects.Events;

namespace TRSI.GamePlay.AdventureMap
{
    [Serializable]
    public struct TileEvent
    {
        public EventDefinitionBase EventDefinitionBase;
        public int                 Weight;
    }
}