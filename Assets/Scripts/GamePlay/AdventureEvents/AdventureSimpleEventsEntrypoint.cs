using System;
using GamePlay.AdventureEvents.Views;
using TRSI.GamePlay.AdventureMap.Routes;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace GamePlay.AdventureEvents
{
    /// <summary>
    ///  Entry point for Adventure Events related logic.
    ///  Random event selection could be added here in the future, dynamically populating the event scene.
    /// </summary>
    public class AdventureSimpleEventsEntrypoint : IStartable, IDisposable
    {
        [Inject] SimpleEventPanel m_eventPanel;
        [Inject] ICommandPublisher m_commandPublisher;
        
        
        public void Start()
        {
            m_eventPanel.QuitButton.onClick.AddListener(OnQuitClicked);
        }
        
        void OnQuitClicked()
        {
            // Notify that the event has ended
            m_commandPublisher.PublishAsync(new EventEndedCommand());
        }
        
        public void Dispose()
        {
            m_eventPanel.QuitButton.onClick.RemoveAllListeners();
        }
    }
}