using System.Collections.Generic;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace TRSI.GamePlay.AdventureMap
{
    [CreateAssetMenu(menuName = "RTSI/Tiles", fileName = "New Scriptable Tile")]
    public class GameTile : TileBase
    {
        [SerializeField] Sprite          sprite;
        [SerializeField] EOceanTileType  eOceanTileType;
        [SerializeField] List<TileEvent> tileEvents;
        public EOceanTileType EOceanTileType => eOceanTileType;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = sprite;
        }


        public EventDefinitionBase GetRandomEvent()
        {
            var totalWeight = 0;
            foreach (var tileEvent in tileEvents) totalWeight += tileEvent.Weight;

            var randomValue = Random.Range(0, totalWeight);
            var currentWeight = 0;

            foreach (var tileEvent in tileEvents)
            {
                currentWeight += tileEvent.Weight;
                if (randomValue < currentWeight) return tileEvent.EventDefinitionBase;
            }

            return null;
        }
    }
}