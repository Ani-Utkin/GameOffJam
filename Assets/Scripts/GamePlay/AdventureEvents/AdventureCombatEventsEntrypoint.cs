using System;
using GamePlay.AdventureEvents.Views;
using RTSI.Services;
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
        [Inject] PlayerStatsService m_playerStatsService;
        
        [Inject] ICommandSubscribable m_commandSubscriber;

        Subscription m_subscription;
        
        public void Start()
        {
            m_eventPanel.TempQuitButton.onClick.AddListener(OnQuitClicked);

            m_eventPanel.PlayerHealthText.text = $"Player Health : {m_playerStatsService.CurrentHealth}";


            m_subscription = m_commandSubscriber.Subscribe<EventStartCommand>(OnEventStarted);
        }

        void OnEventStarted(EventStartCommand arg1, PublishContext arg2)
        {
            
        }

        void OnQuitClicked()
        {
            // Notify that the event has ended
            m_commandPublisher.PublishAsync(new EventEndedCommand());
        }
        
        public void Dispose()
        {
            m_eventPanel.TempQuitButton.onClick.RemoveAllListeners();
            m_subscription.Dispose();
        }

    }
}