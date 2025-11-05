using System.Threading;
using Cysharp.Threading.Tasks;
using RTSI.Services;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace RTSI.GameEntrypoint
{
    public class RootEntrypoint : IAsyncStartable
    {
        [Inject] ICommandPublisher  m_commandPublisher;
        
        public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
        {
            await m_commandPublisher.PublishAsync(new LoadMainMenuSceneCommand());
        }
    }
}