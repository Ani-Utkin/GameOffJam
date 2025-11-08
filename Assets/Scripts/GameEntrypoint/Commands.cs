using VitalRouter;

namespace RTSI.GameEntrypoint
{
    public struct LoadMainMenuSceneCommand : ICommand {}
    public struct UnloadMainMenuSceneCommand : ICommand {}
    public struct LoadGameSceneCommand : ICommand {}
    public struct UnloadGameSceneCommand : ICommand {}
    
    public struct UnloadInGameMenuSceneCommand : ICommand {}
    public struct LoadInGameMenuSceneCommand : ICommand {}
    
    public struct LoadEventSceneCommand : ICommand {}
    public struct UnloadEventSceneCommand : ICommand {}
}