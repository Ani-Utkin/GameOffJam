using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TRSI.GamePlay.AdventureMap
{
    public class BoatView : MonoBehaviour
    {
        public bool isSelected;
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

        public void MoveShip()
        {
            
        }
    }
}