using System;
using TMPro;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using UnityEngine.Serialization;

namespace TRSI.GamePlay.AdventureMap.Debug
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] TMP_Text debugLabel;


        public void SetText(string text)
        {
            this.debugLabel.text = text;
        }

        public void DebugMouseInfos(DebugMouseInfos infos)
        {
            debugLabel.text = $"Mouse Position: WS : {infos.Position} , GS {infos.GridPosition}, Tile Type {infos.OceanTileType.ToString()}";
        }
    }
}