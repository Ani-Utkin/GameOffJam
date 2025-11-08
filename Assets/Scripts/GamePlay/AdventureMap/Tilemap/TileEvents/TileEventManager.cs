using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.AdventureMap.Tilemap.TileEvents
{
    /// <summary>
    ///  Unused.
    /// </summary>
    public class TileEventManager : MonoBehaviour
    {

        public GameObject TileEventPanel;

        public void ShowPanel(string eventName, int random)
        {
            TileEventPanel.SetActive(random < 10);
        }

    }
}
