using DBH.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DBH.Editor
{
    public class MobEditor : MonoBehaviour
    {
        [SerializeField]
        private MobList m_mobList;

        private void Awake()
        {
            m_mobList.onAddNewMobButtonClicked += HandleAddNewMob;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleAddNewMob()
        {
            m_mobList.CreateNewMob("Default Mob");
            m_mobList.Refresh();
        }
    }
}