using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GamePlay.AdventureEvents.Views;
using RTSI.Services;
using ScriptableObjects;
using ScriptableObjects.Events;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter;
using Random = UnityEngine.Random;

namespace GamePlay.AdventureEvents
{
    public class AdventureCombatEventsEntrypoint : IStartable, IDisposable, IAsyncUpdateHandler
    {
        [Inject] CombatEventPanel  m_eventPanel;
        [Inject] ICommandPublisher m_commandPublisher;
        [Inject] PlayerStatsService m_playerStatsService;
        
        [Inject] ICommandSubscribable m_commandSubscriber;

        Subscription m_subscription;
        CombatStats m_mobCombatStats;
        
        int m_mobCurrentHealth;
        int m_playerCurrentDodge;

        private bool m_IsPlayerTurn;
        
        public void Start()
        {
            m_eventPanel.TempQuitButton.onClick.AddListener(OnQuitClicked);
            m_eventPanel.AttackButton.onClick.AddListener(OnAttackClicked);
            m_eventPanel.DodgeButton.onClick.AddListener(OnDodgeClicked);
            m_eventPanel.FleeButton.onClick.AddListener(OnFleeClicked);

            m_eventPanel.PlayerHealthText.text = $"Player Health : {m_playerStatsService.CurrentHealth}";

            m_playerCurrentDodge = m_playerStatsService.Dodge;
            
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
            m_mobCurrentHealth = m_mobCombatStats.MaxHealth;
        }
        
        public UniTask UpdateAsync()
        {
            throw new NotImplementedException();
        }
        
        
        void OnAttackClicked()
        {
            //attack enemy
            var damage = Random.Range(1,7);
            Debug.Log($"[{nameof(AdventureCombatEventsEntrypoint)}] Damage : {damage}");
            if (damage >= m_mobCombatStats.Dodge)
            {
                AttackEnemy(damage);
            }
        }

        void AttackEnemy(int attackDamage)
        {
             m_mobCurrentHealth -= attackDamage;
             
             Debug.Log("Current Health: " + m_mobCurrentHealth);
             m_eventPanel.mobHealthText.text = $"Enemy Health {m_mobCurrentHealth}";
             
             if (m_mobCurrentHealth <= 0)
             {
                 m_commandPublisher.PublishAsync(new EventEndedCommand());
             }
        }

        void AttackPlayer(int attackDamage)
        {
            m_playerStatsService.CurrentHealth -= attackDamage;

            if (m_playerStatsService.CurrentHealth <= 0)
            {
                // Lose battle
                m_playerStatsService.CurrentHealth = m_playerStatsService.MaxHealth;
                m_commandPublisher.PublishAsync(new EventEndedCommand());
            }
        }

        void OnDodgeClicked()
        {
            //Dodge
            var dodge = Random.Range(1,7);
            if (m_playerCurrentDodge > 0)
            {
                CheckDodge(dodge);
            }
        }

        void CheckDodge(int dodge)
        {
            if (dodge < m_mobCombatStats.Attack)
            {
                AttackPlayer(m_mobCombatStats.Attack);
            }

            m_playerCurrentDodge--;
        }

        void OnFleeClicked()
        {
            var flee = Random.Range(1,7);

            if (flee >= m_mobCombatStats.Speed)
            { 
                m_commandPublisher.PublishAsync(new EventEndedCommand());
            }
        }

        void OnQuitClicked()
        {
            // Notify that the event has ended
            m_commandPublisher.PublishAsync(new EventEndedCommand());
        }
        
        public void Dispose()
        {
            m_eventPanel.TempQuitButton.onClick.RemoveAllListeners();
            m_eventPanel.AttackButton.onClick.RemoveAllListeners();
            m_eventPanel.DodgeButton.onClick.RemoveAllListeners();
            m_eventPanel.FleeButton.onClick.RemoveAllListeners();
            m_subscription.Dispose();
        }


    }
}