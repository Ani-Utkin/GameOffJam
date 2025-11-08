using RTSI.GameEntrypoint;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap.Routes
{
    [Routes]
    public partial class GameEventsRoute
    {
        [Inject] ICommandPublisher m_commandPublisher;
        
        [Route]
        void On(ShipMovedCommand command)
        {
            m_commandPublisher.PublishAsync(new LoadEventSceneCommand());
        }

        [Route]
        void On(EventEndedCommand _)
        {
            m_commandPublisher.PublishAsync(new UnloadEventSceneCommand());
        }
    }
}