using Cysharp.Threading.Tasks;
using RTSI.Services;
using VContainer;
using VitalRouter;

namespace RTSI.GameEntrypoint
{
    public struct LoadMainMenuSceneCommand : ICommand {}
    public struct UnloadMainMenuSceneCommand : ICommand {}
    public struct LoadGameSceneCommand : ICommand {}
    public struct UnloadGameSceneCommand : ICommand {}
    
    public struct UnloadInGameMenuSceneCommand : ICommand {}
    public struct LoadInGameMenuSceneCommand : ICommand {}
    
    [Routes]
    public partial class RootMessageRouter
    {
        [Inject] SceneLoaderService m_sceneLoaderService;

        [Route]
        async UniTask On(LoadMainMenuSceneCommand _)
        {
            await m_sceneLoaderService.LoadMainMenuSceneAsync();
        }

        [Route]
        async UniTask On(UnloadMainMenuSceneCommand _)
        {
            await m_sceneLoaderService.UnloadMainMenuSceneAsync();
        }
        
        [Route]
        async UniTask On(LoadGameSceneCommand _)
        {
            await m_sceneLoaderService.LoadPlaySceneAsync();
        }


        [Route]
        async UniTask On(UnloadGameSceneCommand _)
        {
            await m_sceneLoaderService.UnloadPlaySceneAsync();
        }

        [Route]
        async UniTask On(UnloadInGameMenuSceneCommand _)
        {
            await m_sceneLoaderService.UnloadPauseMenuSceneAsync();
        }

        [Route]
        async UniTask On(LoadInGameMenuSceneCommand _)
        {
            await m_sceneLoaderService.LoadPauseMenuSceneAsync();
        }
    }
}