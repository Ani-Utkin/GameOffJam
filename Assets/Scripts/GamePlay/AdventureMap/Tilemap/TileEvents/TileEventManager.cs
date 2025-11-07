using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.AdventureMap.Tilemap.TileEvents
{
    public class TileEventManager : MonoBehaviour
    {

        public GameObject TileEventPanel;

        public void ShowPanel(string eventName)
        {
            TileEventPanel.SetActive(true);
        }

    }
}
