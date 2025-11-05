using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] Button resumeButton;
        [SerializeField] Button quitButton;
        
        public Button ResumeButton => resumeButton;
        public Button QuitButton => quitButton;
    }
}