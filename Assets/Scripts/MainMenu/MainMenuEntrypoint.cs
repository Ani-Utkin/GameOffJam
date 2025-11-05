using System;
using RTSI.GameEntrypoint;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace RTSI.MainMenu
{
    public class MainMenuEntrypoint : IStartable, IDisposable
    {
        [Inject] MainMenuView m_mainMenuView;
        [Inject] ICommandPublisher  m_commandPublisher;
        
        public void Start()
        {
            m_mainMenuView.PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        void OnPlayButtonClicked()
        {
            m_commandPublisher.PublishAsync(new LoadGameSceneCommand());
            m_commandPublisher.PublishAsync(new UnloadMainMenuSceneCommand());
        }

        public void Dispose()
        {
            m_mainMenuView.PlayButton.onClick.RemoveAllListeners();
        }
    }
}