using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;
using VitalRouter;

namespace TRSI.GamePlay.AdventureMap
{
    public class PirateView : MonoBehaviour
    {
        [Inject] OceanGrid m_oceanGrid;
        Transform m_transform;
         
        public Vector3 WorldPosition
        {
            get
            {
               
                if (!m_transform)
                    m_transform = GetComponent<Transform>();
                
                return m_transform.position;
            }
            set => m_transform.position = value;
        }

        public void CheckMoves(Vector3 currentPosition)
        {
            var directions = new Vector3Int[]
            {
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0),
            };
            
            currentPosition = currentPosition + directions[Random.Range(0, directions.Length)];
            UnityEngine.Debug.Log("Pirate Next Position: " + currentPosition);
            if (m_oceanGrid.TryGetTile(currentPosition, out GameTile tile))
            {
                MoveShip(currentPosition);
            }
        }

        public void MoveShip(Vector3 destination)
        {
            transform.position = destination;
        }
    }
}
