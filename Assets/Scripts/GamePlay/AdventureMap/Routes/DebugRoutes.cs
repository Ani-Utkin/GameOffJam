using TRSI.GamePlay.AdventureMap.Debug;
using UnityEngine;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap.Routes
{
    public struct DebugMouseInfos : ICommand
    {
        public Vector2    Position;
        public Vector3Int GridPosition;
        public OceanTileType OceanTileType;
    }
    
    [Routes]
    public partial class DebugRoutes
    {
        [Inject] DebugCanvas m_debugCanvas;
        
        [Route]
        void On(DebugMouseInfos cmd)
        {
            m_debugCanvas.DebugMouseInfos(cmd);
        }
    }
}