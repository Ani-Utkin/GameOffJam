using ScriptableObjects.Events;
using UnityEngine;

namespace GamePlay.AdventureEvents.Views
{
    public class EventCanvas : MonoBehaviour
    {
        [field:SerializeField] public SimpleEventPanel SimpleEventPanel { get; private set; }
        [field:SerializeField] public CombatEventPanel CombatEventPanel { get; private set; }

        public void SetupEvent(EventDefinitionBase evt)
        {
            if (evt is CombatEventDefinition combatEvent)
            {
                SimpleEventPanel.Hide();
                CombatEventPanel.Show();
                CombatEventPanel.Populate(combatEvent);
            }
            else if (evt is SimpleEventDefinition simpleEventDefinition)
            {
                SimpleEventPanel.Show();
                SimpleEventPanel.Populate(simpleEventDefinition);
                CombatEventPanel.Hide();
            }
        }
    }
}