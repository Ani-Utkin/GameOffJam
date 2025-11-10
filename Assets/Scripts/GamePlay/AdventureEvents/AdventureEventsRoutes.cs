using GamePlay.AdventureEvents.Views;
using ScriptableObjects;
using TRSI.GamePlay.AdventureMap.Routes;
using VContainer;
using VitalRouter;

namespace GamePlay.AdventureEvents
{


    [Routes]
    public partial class AdventureEventsRoutes
    {
        [Inject] EventCanvas m_eventCanvas;
        
        [Route]
        void On(EventStartCommand cmd)
        {
            var evt = cmd.EventDefinitionBase;
            
            m_eventCanvas.SetupEvent(evt);
        }
    }
}