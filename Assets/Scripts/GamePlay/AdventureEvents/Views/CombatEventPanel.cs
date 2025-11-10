using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace GamePlay.AdventureEvents.Views
{
    public class CombatEventPanel : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [field:SerializeField] public Button TempQuitButton { get; private set; }
        
        [SerializeField] Image eventImage;
        [SerializeField] Image mobImage;

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
            eventImage.sprite = evt.EventSprite;
            mobImage.sprite   = evt.MobDefinition.MobSprite;
        }
    }
}