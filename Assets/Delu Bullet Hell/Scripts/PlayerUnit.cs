using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class PlayerUnit : MonoBehaviour
    {
        private const float normalMovementSpeed = 5;
        private const float slowMovementSpeed = 2f;

        private PlayableUnitData m_playerData;

        private int m_powerLevel;
        private Vector2 m_movementDirection;
        private float m_movementSpeed;

        private SpriteRenderer m_spriteRenderer;

        public void Awake()
        {
            m_spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(PlayableUnitData unitData)
        {
            m_playerData = unitData;
            m_spriteRenderer.sprite = unitData.sprite; 
        }

        public void Update()
        {
            m_movementDirection = Vector2.zero;
            m_movementSpeed = normalMovementSpeed;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_movementDirection.x = 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_movementDirection.x = -1;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_movementDirection.y = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_movementDirection.y = -1;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_movementSpeed = slowMovementSpeed;
            }


            m_movementDirection.Normalize();
        }

        public void FixedUpdate()
        {
            UpdatePlayerPosition(Time.fixedDeltaTime);
        }

        private void UpdatePlayerPosition(float delta)
        {
            transform.position += new Vector3(m_movementDirection.x, m_movementDirection.y, 0) * m_movementSpeed * delta;
        }
    }
}