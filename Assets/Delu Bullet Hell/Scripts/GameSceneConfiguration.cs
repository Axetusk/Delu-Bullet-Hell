using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    [Serializable]
    public struct PlayerSpawnPoint
    {
        [Range(0, 1)]
        public float x;

        [Min(0)]
        public float y;
    };

    [CreateAssetMenu(menuName = "Bullet Hell/Game Scene Config")]
    public class GameSceneConfiguration : ScriptableObject
    {
        public PlayerSpawnPoint defaultSpawnPoint;
        public float levelTraversalSpeed;
    }
}