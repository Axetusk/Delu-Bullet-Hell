using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class PlayField : MonoBehaviour
    {
        [SerializeField]
        private Camera m_gameCamera;

        private EdgeCollider2D m_collider;
        private Rigidbody2D m_rigidBody;

        private Vector2 m_bottomLeftPoint;
        private Vector2 m_topRightPoint;
        private Vector2 m_hitboxSize { get => m_topRightPoint - m_bottomLeftPoint; }

        public event Action<Collider2D> onObjectExitField;
        public event Action<Collider2D> onObjectEnteredField;

        private void Awake()
        {
            m_collider = gameObject.AddComponent<EdgeCollider2D>();
            m_rigidBody = gameObject.AddComponent<Rigidbody2D>();
            m_rigidBody.bodyType = RigidbodyType2D.Static;

            Vector2 halfSize = new Vector2(m_gameCamera.orthographicSize * m_gameCamera.aspect, m_gameCamera.orthographicSize);
            m_bottomLeftPoint = m_gameCamera.rect.position - halfSize;
            m_topRightPoint = m_gameCamera.rect.position + halfSize;

            Vector3 topRight = m_topRightPoint;
            Vector3 topLeft = m_topRightPoint - new Vector2(m_hitboxSize.x, 0);
            Vector3 bottomLeft = m_bottomLeftPoint;
            Vector3 bottomRight = m_bottomLeftPoint + new Vector2(m_hitboxSize.x, 0);

            m_collider.points = new Vector2[] { topLeft, bottomLeft, bottomRight, topRight, topLeft };
        }

        private void OnDrawGizmos()
        {
            Vector2 halfSize = new Vector2(m_gameCamera.orthographicSize * m_gameCamera.aspect, m_gameCamera.orthographicSize);

            Vector3 topRight = m_gameCamera.transform.position + new Vector3(halfSize.x, halfSize.y, 0);
            Vector3 topLeft = m_gameCamera.transform.position + new Vector3(-halfSize.x, halfSize.y, 0);
            Vector3 bottomLeft = m_gameCamera.transform.position + new Vector3(-halfSize.x, -halfSize.y, 0);
            Vector3 bottomRight = m_gameCamera.transform.position + new Vector3(halfSize.x, -halfSize.y, 0);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.transform.position);
            if (EnteringBox(other.gameObject.transform))
            {
                Debug.Log("Entering Box " + other);
                onObjectEnteredField(other);
            }
            else
            {
                Debug.Log("Leaving box " + other);
                onObjectExitField(other);
            }
        }

        private bool EnteringBox(Transform other)
        {
            return other.transform.position.x < m_bottomLeftPoint.x || other.transform.position.x > m_topRightPoint.x 
                || other.transform.position.y < m_bottomLeftPoint.y || other.transform.position.y > m_topRightPoint.y;
        }
    }
}