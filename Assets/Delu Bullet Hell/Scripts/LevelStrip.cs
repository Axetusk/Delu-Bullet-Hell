using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{


    public class LevelStrip : MonoBehaviour
    {

        [SerializeField]
        private PlayField m_playField;

        [SerializeField]
        private PlayerSpawnPoint m_playerSpawnPoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            DrawStripBounds();

        }

        private void DrawStripBounds()
        {
            Gizmos.color = Color.blue;
            const float yMax = 300000000;
            Gizmos.DrawLine(m_playField.rect.min, new Vector3(m_playField.rect.xMin, yMax));
            Gizmos.DrawLine(new Vector3(m_playField.rect.xMax, m_playField.rect.yMin), new Vector3(m_playField.rect.xMax, yMax));
        }
    }
}
