using System;
using GamePlay.AdventureEvents.Views;
using RTSI.Services;
using ScriptableObjects;
using ScriptableObjects.Events;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
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
        CombatStats m_mobCombatStats;
        
        public void Start()
        {
            m_eventPanel.TempQuitButton.onClick.AddListener(OnQuitClicked);

            m_eventPanel.PlayerHealthText.text = $"Player Health : {m_playerStatsService.CurrentHealth}";
            
            m_subscription = m_commandSubscriber.Subscribe<EventStartCommand>(OnEventStarted);
        }

        void OnEventStarted(EventStartCommand cmd, PublishContext ctx)
        {
            
            if (!(cmd.EventDefinitionBase is CombatEventDefinition combatEventDefinition))
                return;
            
            var mob = combatEventDefinition.MobDefinition;

            if (mob == null)
            {
                Debug.LogError($"[{nameof(AdventureCombatEventsEntrypoint)}] Mob definition is null in combat event");
                return;
            }

            m_mobCombatStats = mob.CombatStats;

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