using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEngine;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap.Routes
{
    public struct OnPointerMovedCommand : ICommand
    {
        public Vector2 Position;
        public Vector2 WorldPosition;
    }

    public struct OnPointerClickedCommand : ICommand
    {
        public Vector2 Position;
        public Vector2 WorldPosition;
    }

    public struct ShipMovedCommand : ICommand
    {
        public EventDefinitionBase Event;
    }
    
    public struct EventEndedCommand : ICommand
    {
    }
    
    
    public struct EventStartCommand : ICommand
    {
        public EventDefinitionBase EventDefinitionBase;
    }

}