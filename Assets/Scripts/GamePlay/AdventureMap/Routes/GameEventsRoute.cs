using Cysharp.Threading.Tasks;
using GamePlay.AdventureEvents;
using RTSI.GameEntrypoint;
using RTSI.Services;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap.Routes
{
    [Routes]
    public partial class GameEventsRoute
    {
        [Inject] ICommandPublisher m_commandPublisher;
        
        [Route]
        async UniTask On(ShipMovedCommand command)
        {
            await m_commandPublisher.PublishAsync(new LoadEventSceneCommand());
            await m_commandPublisher.PublishAsync(new EventStartCommand
            {
                EventDefinitionBase = command.Event,
            });
            
        }

        [Route]
        async UniTask On(EventEndedCommand _)
        {
            await m_commandPublisher.PublishAsync(new UnloadEventSceneCommand());
        }
    }
}