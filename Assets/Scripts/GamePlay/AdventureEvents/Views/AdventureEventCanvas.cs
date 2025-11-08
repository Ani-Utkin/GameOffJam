using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.AdventureEvents.Views
{
    public class AdventureEventCanvas : MonoBehaviour
    {
        [SerializeField] Button quitButton;
        
        public Button QuitButton => quitButton;
    }
}