using System;
using RTSI.GameEntrypoint;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace GamePlay.PauseMenu
{
    public class PauseMenuViewEntrypoint : IStartable, IDisposable
    {
        [Inject] PauseMenuView m_pauseMenuView;
        [Inject] ICommandPublisher m_commandPublisher;
        
        public void Start()
        {
            m_pauseMenuView.ResumeButton.onClick.AddListener(OnResumeClicked);
            m_pauseMenuView.QuitButton.onClick.AddListener(OnQuitClicked);
        }
        
        void OnResumeClicked()
        {
            m_commandPublisher.PublishAsync(new UnloadInGameMenuSceneCommand());
        }
        
        async void OnQuitClicked()
        {
            await m_commandPublisher.PublishAsync(new UnloadGameSceneCommand());
            await m_commandPublisher.PublishAsync(new UnloadInGameMenuSceneCommand());
            await m_commandPublisher.PublishAsync(new LoadMainMenuSceneCommand());
        }

        public void Dispose()
        {
            m_pauseMenuView.ResumeButton.onClick.RemoveAllListeners();
            m_pauseMenuView.QuitButton.onClick.RemoveAllListeners();
        }
    }
}