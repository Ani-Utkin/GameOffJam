using ScriptableObjects.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace GamePlay.AdventureEvents.Views
{
    public class SimpleEventPanel : MonoBehaviour
    {
        [SerializeField] Button quitButton;
        [SerializeField] TMP_Text buttonText;
        [SerializeField] TMP_Text eventMessageText;
        [SerializeField] Image eventImage;
        [SerializeField] CanvasGroup canvasGroup;
        
        public Button QuitButton => quitButton;
        public TMP_Text ButtonText => buttonText;
        public TMP_Text EventMessageText => eventMessageText;
        public Image EventImage => eventImage;
        
        public void Show()
        {
            canvasGroup.Show();
        }
        
        public void Hide()
        {
            canvasGroup.Hide();
        }

        public void Populate(SimpleEventDefinition evt)
        {
            buttonText.text       = evt.ButtonText;
            eventMessageText.text = evt.EventText;
            eventImage.sprite     = evt.EventSprite;
        }
    }
}