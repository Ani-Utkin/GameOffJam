using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TRSI.GamePlay.AdventureMap
{
    public class OceanGrid : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] Tilemap mainTilemap;

        public float TileUnit => grid.cellSize.x;

        public Vector3Int WorldToGrid(Vector2 worldPosition)
        {
            return grid.WorldToCell(worldPosition);
        }
        

        public bool TryGetTile(Vector3 worldPosition, out OceanTile tile)
        {
            tile = null;
            
            var wtc = grid.WorldToCell(worldPosition);
            if (mainTilemap.HasTile(wtc))
            {
                tile = mainTilemap.GetTile<OceanTile>(wtc);
                return true;
            }
            
            return false;
        }
    }
}