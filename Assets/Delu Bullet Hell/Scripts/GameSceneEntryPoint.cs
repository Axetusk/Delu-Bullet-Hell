using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        public PlayableUnitData testPlayerClass;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = new GameObject("Player", new System.Type[]{ typeof(PlayerUnit) });
            player.GetComponent<PlayerUnit>().Initialize(testPlayerClass);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}