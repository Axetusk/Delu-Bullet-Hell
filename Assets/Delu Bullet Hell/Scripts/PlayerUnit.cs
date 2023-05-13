using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class PlayerUnit : MonoBehaviour
    {
        private const float normalMovementSpeed = 5;
        private const float slowMovementSpeed = 2f;

        private PlayableUnitData m_playerData;

        private int m_powerLevel;
        private Vector2 m_movementDirection;
        private float m_movementSpeed;

        private SpriteRenderer m_spriteRenderer;
        private CircleCollider2D m_hitbox;
        private Rigidbody2D m_rigidbody;

        public void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Level");
            m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            m_hitbox = gameObject.GetComponent<CircleCollider2D>();
            m_rigidbody = gameObject.GetComponent<Rigidbody2D>();

            m_rigidbody.gravityScale = 0;
            m_rigidbody.drag = 0;
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
            m_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            m_hitbox.radius = 0.06f;
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
            m_rigidbody.MovePosition(transform.position + new Vector3(m_movementDirection.x, m_movementDirection.y, 0) * m_movementSpeed * delta);
        }
    }
}