using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap.Routes
{
    [Routes]
    public partial class InputRoutes
    {
        [Inject] ICommandPublisher m_commandPublisher;
        [Inject] OceanGrid m_oceanGrid;
        [Inject] AdventureMapController m_adventureMapController;
        
        /// <summary>
        ///  Mostly debug stuff for now.
        /// </summary>
        /// <param name="cmd"></param>
        [Route]
        void On(OnPointerMovedCommand cmd)
        {
            var gridPosition = m_oceanGrid.WorldToGrid(cmd.WorldPosition);

            OceanTileType oceanTileType = OceanTileType.None;
            
            if (m_oceanGrid.TryGetTile(cmd.WorldPosition, out var tile))
            {
                oceanTileType = tile.OceanTileType;
            }
            
            // Send the debug infos to the DebugRoutes
            m_commandPublisher.PublishAsync(new DebugMouseInfos()
            {
                Position      = cmd.WorldPosition,
                GridPosition  = gridPosition,
                OceanTileType = oceanTileType,
            });
        }

        [Route]
        void On(OnPointerClickedCommand cmd)
        {
            // Forward the click to the AdventureMapController
            m_adventureMapController.OnMapClicked(cmd.WorldPosition);
        }
    }
}