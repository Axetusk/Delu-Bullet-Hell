using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{


    public class LevelStrip : MonoBehaviour
    {
        [SerializeField]
        private GameSceneConfiguration m_config;

        [SerializeField]
        private PlayField m_playField;

        [SerializeField]
        private PlayerSpawnPoint m_playerSpawnPoint;

        [SerializeField]
        [Min(0)]
        private float m_stripTraversed = 0;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            m_stripTraversed += m_config.levelTraversalSpeed * Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            DrawStripBounds();

        }

        private void DrawStripBounds()
        {
            Gizmos.color = Color.blue;
            const float yMax = 300000000;
            Vector3 min = m_playField.rect.min;
            Vector3 max = m_playField.rect.max;
            Vector3 offset = new Vector3(0, m_playField.rect.height) * m_stripTraversed;
            Gizmos.DrawLine(min - offset, new Vector3(min.x, yMax) - offset);
            Gizmos.DrawLine(new Vector3(max.x, min.y) - offset, new Vector3(max.x, yMax) - offset);
        }
    }
}
