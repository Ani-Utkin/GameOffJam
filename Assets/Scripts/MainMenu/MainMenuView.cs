using UnityEngine;
using UnityEngine.UI;

namespace RTSI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button playButton;
        
        public Button PlayButton => playButton;
    }
}