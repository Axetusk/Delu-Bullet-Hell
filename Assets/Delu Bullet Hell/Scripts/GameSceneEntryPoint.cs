using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        public PlayableUnitData testPlayerClass;

        public PlayField playField;
        public GameSceneConfiguration config;

        private Vector2 defaultSpawnPoint => playField.rect.min + (playField.rect.size * new Vector2(config.defaultSpawnPoint.x, config.defaultSpawnPoint.y));
        // Start is called before the first frame update
        void Start()
        {
            GameObject player = new GameObject("Player", new System.Type[]{ typeof(PlayerUnit) });
            player.GetComponent<PlayerUnit>().Initialize(testPlayerClass);

            player.transform.position = defaultSpawnPoint;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(defaultSpawnPoint, 0.1f);
        }
    }
}