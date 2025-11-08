using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap
{
    public class AdventureMapController
    {
        [Inject] OceanGrid m_oceanGrid;
        [Inject] BoatView m_boatView;
        [Inject] ICommandPublisher m_commandPublisher;


        public void OnMapClicked(Vector2 mouseWorldPosition)
        {
            var gridPosition = m_oceanGrid.WorldToGrid(mouseWorldPosition);
            var boatToGrid = m_oceanGrid.WorldToGrid(m_boatView.WorldPosition);
            
            if (gridPosition == boatToGrid)
            {
                // We clicked on the boat
                m_boatView.IsSelected = true;
                // Highlight possible moves
                m_oceanGrid.HighLightValidMoves(boatToGrid);
                return;
            }

            if (!m_oceanGrid.TryGetTile(mouseWorldPosition, out var tile))
                return;
            
            if (tile.OceanTileType == OceanTileType.None)
            {
                // We clicked on a non-navigable tile
                m_boatView.IsSelected = false;
                m_oceanGrid.ClearAllHighlights();
                return;
            }

            if (
                m_boatView.IsSelected &&
                Mathf.Approximately(Vector3Int.Distance(gridPosition, boatToGrid), 1f))
            {
                // We clicked on a valid adjacent tile
                // Move the boat
                m_boatView.MoveShip(m_oceanGrid.GridToWorld(gridPosition));
                
                m_boatView.IsSelected = false;
                m_oceanGrid.ClearAllHighlights();

                // Notify other systems about the move (GameEventsRoute will handle loading events)
                m_commandPublisher.PublishAsync(new ShipMovedCommand
                {
                    TileType = tile.OceanTileType,
                });
            }
            else
            {
                // Invalid move or boat not selected
                m_boatView.IsSelected = false;
                m_oceanGrid.ClearAllHighlights();
            }
            
        }
    }
}