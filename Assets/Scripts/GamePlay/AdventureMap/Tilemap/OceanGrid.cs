using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TRSI.GamePlay.AdventureMap
{
    public class OceanGrid : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] Tilemap mainTilemap;
        [SerializeField] Tilemap overlayTilemap;
        
        [Header("Tile References")]
        [SerializeField] TileBase highlightTile;
        
        public float TileUnit => grid.cellSize.x;

        public Vector3Int WorldToGrid(Vector2 worldPosition)
        {
            return grid.WorldToCell(worldPosition);
        }
        
        public Vector3 WorldToGridSpace(Vector2 worldPosition)
        {
            var gridPosition = grid.WorldToCell(worldPosition);
            return grid.CellToWorld(gridPosition);
        }

        public Vector2 GridToWorld(Vector3Int gridPosition)
        {
            return  grid.CellToWorld(gridPosition);
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

        public bool TryGetTile(Vector3Int gridPosition, out OceanTile tile)
        {
            tile = null;
            if (mainTilemap.HasTile(gridPosition))
            {
                tile = mainTilemap.GetTile<OceanTile>(gridPosition);
                return true;
            }
            return false;
        }
        
        public void HighLightValidMoves(Vector3Int boatGridPosition)
        {
            overlayTilemap.ClearAllTiles();
            
            var directions = new Vector3Int[]
            {
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0),
            };

            foreach (var dir in directions)
            {
                var checkPos = boatGridPosition + dir;
                if (mainTilemap.HasTile(checkPos))
                {
                    var tile = mainTilemap.GetTile<OceanTile>(checkPos);
                    if (tile.OceanTileType != OceanTileType.None)
                    {
                        overlayTilemap.SetTile(checkPos, highlightTile);
                    }
                }
            }
        }
        
        public void ClearAllHighlights()
        {
            overlayTilemap.ClearAllTiles();
        }
    }
}