using UnityEngine;
using UnityEngine.Tilemaps;

namespace TRSI.GamePlay.AdventureMap
{
    [CreateAssetMenu(menuName = "RTSI/Tiles", fileName = "New Scriptable Tile")]
    public class OceanTile : TileBase
    {
        [SerializeField] Sprite sprite;
        [SerializeField]  OceanTileType oceanTileType;
        
        public OceanTileType OceanTileType => oceanTileType;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = sprite;
        }
    }

    public enum OceanTileType
    {
        Ocean,
        Island,
        None,
    }
}