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
        [Inject] BoatView m_boatView;
        
        AdventureMapInputs m_inputs;
        
        Vector2 m_mousePosition;
        Vector2 currentPosition;
        
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
            m_mousePosition = position;
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
            var mouseWorldPosition = m_mainCamera.ScreenToWorldPoint(m_mousePosition);
            if (m_oceanGrid.TryGetTile(mouseWorldPosition, out var tile))
            {
                if (tile.OceanTileType == OceanTileType.None)
                {
                    // Not a useable tile
                    Debug.Log("This is not  a valid Tile!");
                    m_boatView.isSelected = false;
                }
                else
                {
                    // Useable tile.
                    // We can get the tile's world position back
                    var targetPosition = m_oceanGrid.WorldToGridSpace(mouseWorldPosition);
                    Vector3 difference = m_boatView.WorldPosition - targetPosition; // calculate if selected tile contains boat
                    
                    //if the boat is in the selected tile
                    if (difference is { x: 1.0f, y: 1.0f})
                    {
                        Debug.Log("Boat Here!");
                        m_boatView.isSelected = true;
                        currentPosition = targetPosition;
                        // Highlight tile and adjacent tiles
                        
                    }
                    else if (m_boatView.isSelected)
                    {
                        Debug.Log("Target Tile: " + targetPosition + " Boat Current Position: " + m_boatView.WorldPosition);
                        checkValidTileMovement(targetPosition, m_boatView.WorldPosition);
                        m_boatView.isSelected = false;
                    }
                    else
                    {
                        Debug.Log("Boat Not Here!");
                        m_boatView.isSelected = false;
                        // Unhighlight tiles
                    }
                }
            }
            else
            {
                // We clicked somewhere where there is no tile
                Debug.Log("This is not  a valid Tile!");
            }
        }

        private void checkValidTileMovement(Vector2 targetTile, Vector3 boatPosition)
        {
            Vector2 tileDifference = currentPosition - targetTile;
            Debug.Log("Tile Difference: " + tileDifference);

            if (Mathf.Abs(tileDifference.x) <= 2.0f && Mathf.Abs(tileDifference.y) <= 2.0f)
            {
                m_boatView.WorldPosition = new Vector3(targetTile.x + 1, targetTile.y + 1, boatPosition.z);
            }
        }

        public void Dispose()
        {
            
            m_inputs.Player.PointerClick.performed -= OnPointerClick;
            m_inputs.Player.PointerPosition.performed -= OnPointerMoved;
            
            m_inputs.Disable();
        }

    }
}