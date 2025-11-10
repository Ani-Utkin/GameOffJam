using System;
using GamePlay.AdventureEvents.Views;
using TRSI.GamePlay.AdventureMap.Routes;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace GamePlay.AdventureEvents
{
    public class AdventureCombatEventsEntrypoint : IStartable, IDisposable
    {
        [Inject] CombatEventPanel  m_eventPanel;
        [Inject] ICommandPublisher m_commandPublisher;
        
        public void Start()
        {
            m_eventPanel.TempQuitButton.onClick.AddListener(OnQuitClicked);
        }
        
        void OnQuitClicked()
        {
            // Notify that the event has ended
            m_commandPublisher.PublishAsync(new EventEndedCommand());
        }
        
        public void Dispose()
        {
            m_eventPanel.TempQuitButton.onClick.RemoveAllListeners();
        }

    }
}