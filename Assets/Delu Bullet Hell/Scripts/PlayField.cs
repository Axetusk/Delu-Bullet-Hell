using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class PlayField : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int m_aspectRatioReal;

        [SerializeField]
        private float m_fieldSize;

        public Vector2Int aspectRatioReal => m_aspectRatioReal;
        public float aspectRatio => (float)m_aspectRatioReal.x / (float)m_aspectRatioReal.y;
        public float fieldSize => m_fieldSize;
        public Rect rect => new Rect((Vector2)transform.position - (new Vector2(aspectRatio, 1) * m_fieldSize), new Vector2(aspectRatio, 1) * m_fieldSize * 2);

        private EdgeCollider2D m_collider;
        private Rigidbody2D m_rigidBody;

        private Vector2 m_bottomLeftPoint { get => new Vector2(rect.xMin, rect.yMin); }
        private Vector2 m_topRightPoint { get => new Vector2(rect.xMax, rect.yMax); }

        private Vector2 m_bottomRightPoint { get => new Vector2(rect.xMax, rect.yMin); }
        private Vector2 m_topLeftPoint { get => new Vector2(rect.xMin, rect.yMax); }

        public event Action<Collider2D> onObjectExitField;
        public event Action<Collider2D> onObjectEnteredField;

        private void Awake()
        {
            m_collider = gameObject.AddComponent<EdgeCollider2D>();
            m_rigidBody = gameObject.AddComponent<Rigidbody2D>();
            m_rigidBody.bodyType = RigidbodyType2D.Static;

            m_collider.points = new Vector2[] { m_topLeftPoint, m_bottomLeftPoint, m_bottomRightPoint, m_topRightPoint, m_topLeftPoint };
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(m_topLeftPoint, m_topRightPoint);
            Gizmos.DrawLine(m_topRightPoint, m_bottomRightPoint);
            Gizmos.DrawLine(m_bottomRightPoint, m_bottomLeftPoint);
            Gizmos.DrawLine(m_bottomLeftPoint, m_topLeftPoint);
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