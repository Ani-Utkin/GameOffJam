using System;
using GamePlay.AdventureMap.Tilemap.TileEvents;
using RTSI.GameEntrypoint;
using TRSI.GamePlay.AdventureMap;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using VitalRouter;
using Random = UnityEngine.Random;

namespace RTSI.GamePlay.AdventureMap.Input
{
    /// <summary>
    /// This class should only handle input-related stuff.
    /// When an input is valid, it should send a message so that movement/events/tile highlights
    /// can be handled by their own controllers.
    /// </summary>
    public class AdventureMapInputEntrypoint : IStartable, IDisposable, ITickable
    {
        [Inject] ICommandPublisher m_commandPublisher;
        [Inject] OceanGrid m_oceanGrid;
        [Inject] Camera m_mainCamera;
        [Inject] BoatView m_boatView;
        
        AdventureMapInputs m_inputs;
        bool               m_isPointerBlocked;
        
        TileEventManager m_Panel;
        Vector2 m_mousePosition;
        Vector2 currentPosition;
        
        public void Start()
        {
            m_inputs = new AdventureMapInputs();
            m_inputs.Enable();

            m_inputs.Player.PointerClick.performed    += OnPointerClick;
            m_inputs.Player.PointerPosition.performed += OnPointerMoved;
            m_inputs.Player.PauseMenu.performed       += OnPauseMenu;
            
         
            /*
             * This should have been referenced using [inject]. To do so:
             * - add a `[SerializedField] TileEventManager tileEventManager` attribute in AdventureMapLifetimeScope
             * - Register in the `Configure(IContainerBuilder builder)` method using `builder.RegisterInstance(tileEventManager);`
             */
            // m_Panel = GameObject.Find("Tile Event Manager").GetComponent<TileEventManager>();
        }
        
        
        
        public void Tick()
        {
            // Checking if the mouse is blocked by the UI
            m_isPointerBlocked = EventSystem.current.IsPointerOverGameObject(Mouse.current.deviceId);
        }
        

        void OnPauseMenu(InputAction.CallbackContext obj)
        {
            m_commandPublisher.PublishAsync(new LoadInGameMenuSceneCommand());
        }

        void OnPointerMoved(InputAction.CallbackContext ctx)
        {
            Vector2 position = ctx.ReadValue<Vector2>();
            m_mousePosition = position;
            Vector2 worldPosition        = m_mainCamera.ScreenToWorldPoint(position);
            
            m_commandPublisher.PublishAsync(new OnPointerMovedCommand
            {
                Position = position,
                WorldPosition = worldPosition,
            });
        }

        void OnPointerClick(InputAction.CallbackContext ctx)
        {

            if (m_isPointerBlocked) return;

            // Notify other systems about the click
            m_commandPublisher.PublishAsync(new OnPointerClickedCommand
            {
                Position      = m_mousePosition,
                WorldPosition = m_mainCamera.ScreenToWorldPoint(m_mousePosition),

            });
            
            /*
             * Not really the right place for this code or this function will quickly become huge.
             * Just send the OnPointerClickedCommand and let another controller handle the rest.
             */
            // var mouseWorldPosition = m_mainCamera.ScreenToWorldPoint(m_mousePosition);
            // if (m_oceanGrid.TryGetTile(mouseWorldPosition, out var tile))
            // {
            //     if (tile.OceanTileType == OceanTileType.None)
            //     {
            //         // Not a useable tile
            //         Debug.Log("This is not  a valid Tile!");
            //         m_boatView.isSelected = false;
            //     }
            //     else
            //     {
            //         // Useable tile.
            //         // We can get the tile's world position back
            //         var targetPosition = m_oceanGrid.WorldToGridSpace(mouseWorldPosition);
            //         Vector3 difference = m_boatView.WorldPosition - targetPosition; // calculate if selected tile contains boat
            //         
            //         //if the boat is in the selected tile
            //         if (difference is { x: 1.0f, y: 1.0f})
            //         {
            //             Debug.Log("Boat Here!");
            //             m_boatView.isSelected = true;
            //             currentPosition = targetPosition;
            //             // Highlight tile and adjacent tiles
            //             
            //         }
            //         else if (m_boatView.isSelected)
            //         {
            //             Debug.Log("Target Tile: " + targetPosition + " Boat Current Position: " + m_boatView.WorldPosition);
            //             checkValidTileMovement(targetPosition, m_boatView.WorldPosition);
            //             m_boatView.isSelected = false;
            //             m_Panel.ShowPanel("Boat Here", Random.Range(1, 21));
            //         }
            //         else
            //         {
            //             Debug.Log("Boat Not Here!");
            //             m_boatView.isSelected = false;
            //             // Unhighlight tiles
            //         }
            //     }
            // }
            // else
            // {
            //     // We clicked somewhere where there is no tile
            //     Debug.Log("This is not  a valid Tile!");
            // }
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