using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RTSI.Services
{


    
    [System.Serializable]
    public class SceneLoaderService
    {
        [SerializeField] SceneReference mainMenuScene;
        [SerializeField] SceneReference playScene;
        [SerializeField] SceneReference pauseMenuScene;
        [SerializeField] SceneReference adventureEventsScene;
        
        SceneInstance  m_mainMenuSceneInstance;
        SceneInstance  m_playSceneInstance;
        SceneInstance m_pauseMenuSceneInstance;
        SceneInstance m_adventureEventsSceneInstance;
        
        public async UniTask LoadMainMenuSceneAsync()
        {
            if (!m_mainMenuSceneInstance.Scene.isLoaded)
                m_mainMenuSceneInstance = await Addressables.LoadSceneAsync(mainMenuScene.Guid, LoadSceneMode.Additive);
        }

        public async UniTask UnloadMainMenuSceneAsync()
        {
            if (m_mainMenuSceneInstance.Scene.isLoaded)
                m_mainMenuSceneInstance = await Addressables.UnloadSceneAsync(m_mainMenuSceneInstance);
        }


        public async UniTask LoadPlaySceneAsync()
        {
            if  (!m_playSceneInstance.Scene.isLoaded)
                m_playSceneInstance  = await Addressables.LoadSceneAsync(playScene.Guid, LoadSceneMode.Additive);
        }

        public async UniTask UnloadPlaySceneAsync()
        {
            if (m_playSceneInstance.Scene.isLoaded)
                m_playSceneInstance = await Addressables.UnloadSceneAsync(m_playSceneInstance);
        }

        public async UniTask LoadPauseMenuSceneAsync()
        {
            if (!m_pauseMenuSceneInstance.Scene.isLoaded)
                m_pauseMenuSceneInstance = await Addressables.LoadSceneAsync(pauseMenuScene.Guid, LoadSceneMode.Additive);
        }

        public async UniTask UnloadPauseMenuSceneAsync()
        {
             if (m_pauseMenuSceneInstance.Scene.isLoaded)
                 m_pauseMenuSceneInstance = await Addressables.UnloadSceneAsync(m_pauseMenuSceneInstance);
        }

        public async UniTask LoadAdventureEventsSceneAsync()
        {
            if (!m_adventureEventsSceneInstance.Scene.isLoaded)   
                m_adventureEventsSceneInstance = await Addressables.LoadSceneAsync(adventureEventsScene.Guid, LoadSceneMode.Additive);
        }
        
        public async UniTask UnloadAdventureEventsSceneAsync()
        {
            if (m_adventureEventsSceneInstance.Scene.isLoaded)
                m_adventureEventsSceneInstance = await Addressables.UnloadSceneAsync(m_adventureEventsSceneInstance);
        }
        
    }
}