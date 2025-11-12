using ScriptableObjects.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace GamePlay.AdventureEvents.Views
{
    public class CombatEventPanel : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [field:SerializeField] public Button TempQuitButton { get; private set; }

        [SerializeField] public Button AttackButton { get; private set; }
        [SerializeField] public Button DodgeButton { get; private set; }
        [SerializeField] public Button FleeButton { get; private set; }


        [SerializeField] Image eventImage;
        [SerializeField] Image mobImage;

        [field: FormerlySerializedAs("<playerHealthText>k__BackingField")] [field:SerializeField] public TMP_Text PlayerHealthText { get; set; }
        [SerializeField] TMP_Text mobHealthText;
        
        public void Show()
        {
            canvasGroup.Show();
        }

        public void Hide()
        {
            canvasGroup.Hide();
        }

        public void Populate(CombatEventDefinition evt)
        {
            eventImage.sprite  = evt.EventSprite;
            mobImage.sprite    = evt.MobDefinition.MobSprite;
            mobHealthText.text = $"Enemy Health {evt.MobDefinition.CombatStats.MaxHealth}";
        }
    }
}