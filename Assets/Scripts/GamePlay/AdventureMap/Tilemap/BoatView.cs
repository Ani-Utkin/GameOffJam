using UnityEngine;

namespace TRSI.GamePlay.AdventureMap
{
    public class BoatView : MonoBehaviour
    {
        Transform m_transform;
        
        public Vector3 WorldPosition
        {
            get
            {
                if (!m_transform)
                    m_transform = GetComponent<Transform>();
                
                return m_transform.position;
            }
        }
    }
}