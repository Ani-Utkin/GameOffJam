using System;
using RTSI.GameEntrypoint;
using TRSI.GamePlay.AdventureMap;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace RTSI.GamePlay.AdventureMap.Input
{
    public class AdventureMapInputEntrypoint : IStartable, IDisposable
    {
        [Inject] ICommandPublisher m_commandPublisher;
        [Inject] OceanGrid m_oceanGrid;
        [Inject] Camera m_mainCamera;
        
        AdventureMapInputs m_inputs;
        
        public void Start()
        {
            m_inputs = new AdventureMapInputs();
            m_inputs.Enable();

            m_inputs.Player.PointerClick.performed    += OnPointerClick;
            m_inputs.Player.PointerPosition.performed += OnPointerMoved;
            m_inputs.Player.PauseMenu.performed       += OnPauseMenu;
        }

        void OnPauseMenu(InputAction.CallbackContext obj)
        {
            m_commandPublisher.PublishAsync(new LoadInGameMenuSceneCommand());
        }

        void OnPointerMoved(InputAction.CallbackContext ctx)
        {
            
            /*
             * This is temp debug stuff
             */
            
            Vector2 position = ctx.ReadValue<Vector2>();
            position = m_mainCamera.ScreenToWorldPoint(position);
            
            var gridPosition = m_oceanGrid.WorldToGrid(position);

            OceanTileType oceanTileType = OceanTileType.None;
            
            if (m_oceanGrid.TryGetTile(position, out var tile))
            {
                oceanTileType = tile.OceanTileType;
            }
            
            m_commandPublisher.PublishAsync(new DebugMouseInfos()
            {
                Position = position,
                GridPosition = gridPosition,
                OceanTileType = oceanTileType,
            });
        }

        void OnPointerClick(InputAction.CallbackContext ctx)
        {

        }

        public void Dispose()
        {
            
            m_inputs.Player.PointerClick.performed -= OnPointerClick;
            m_inputs.Player.PointerPosition.performed -= OnPointerMoved;
            
            m_inputs.Disable();
        }

    }
}