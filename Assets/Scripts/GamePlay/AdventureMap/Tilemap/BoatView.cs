using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TRSI.GamePlay.AdventureMap
{
    public class BoatView : MonoBehaviour
    {
        
        [SerializeField] Sprite selectedSprite;
        
        SpriteRenderer m_renderer;
        Sprite m_defaultSprite;

        
        
        bool m_isSelected;
        public bool IsSelected
        {
            get => m_isSelected;
            set
            {
                m_isSelected = value;

                if (m_isSelected)
                {
                    m_renderer.sprite = selectedSprite;
                }
                else
                {
                    m_renderer.sprite = m_defaultSprite;
                }
            }
        }
        Transform m_transform;


        void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
            m_defaultSprite = m_renderer.sprite;
        }

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

        public void MoveShip(Vector2 destination)
        {
            transform.position = destination;
        }
    }
}