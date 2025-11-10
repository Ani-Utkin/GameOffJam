using System.Collections.Generic;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap
{
    public class AdventureMapController
    {
        [Inject] OceanGrid         m_oceanGrid;
        [Inject] BoatView          m_boatView;
        [Inject] ICommandPublisher m_commandPublisher;
        [Inject] List<PirateView>  m_pirates;


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

            if (tile.EOceanTileType == EOceanTileType.None)
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
                    Event = tile.GetRandomEvent()
                });

                MovePirateShips();
            }
            else
            {
                // Invalid move or boat not selected
                m_boatView.IsSelected = false;
                m_oceanGrid.ClearAllHighlights();
            }
        }

        void MovePirateShips()
        {
            // Move Pirate Ships in a random direction
            foreach (var pirate in m_pirates)
            {
                var pirateToGrid = m_oceanGrid.WorldToGrid(pirate.WorldPosition);

                var directions = new Vector3Int[]
                {
                    new(1, 0, 0),
                    new(-1, 0, 0),
                    new(0, 1, 0),
                    new(0, -1, 0)
                };

                var newGridPosition = Vector3Int.zero;
                var found = false;
                while (!found)
                {
                    newGridPosition = pirateToGrid + directions[Random.Range(0, directions.Length)];
                    if (m_oceanGrid.TryGetTile(newGridPosition, out var tile))
                        if (!(tile.EOceanTileType is EOceanTileType.None or EOceanTileType.Island))
                            found = true;
                }

                var newWorldPosition = m_oceanGrid.GridToWorld(newGridPosition);
                UnityEngine.Debug.Log($"Pirate Next Position: GS {newGridPosition} | WS {newWorldPosition}");
                pirate.MoveShip(newWorldPosition);
            }
        }
    }
}