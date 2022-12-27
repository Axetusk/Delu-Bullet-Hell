using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobEditor : MonoBehaviour
    {
        [SerializeField]
        private MobList m_mobList;

        [SerializeField]
        private MobStatsPanel m_mobStats;

        [SerializeField]
        private Button m_saveButton;

        private MobData m_selectedMob;
        private MobData m_selectedMobChanges;

        public event Action<MobData> onSaveMob;
        public event Action<MobData> onSelectedMobChanged;

        public MobData selectedMob => m_selectedMobChanges;

        private void Awake()
        {
            //m_mobList.onAddNewMobButtonClicked += HandleAddNewMob;
            m_mobList.onOpenMob += HandleMobOpened;
            m_mobList.onMobDeleted += HandleMobDeleted;
            m_saveButton.onClick.AddListener(HandleSaveMob);

            m_mobList.Initialize(this);
            m_mobStats.Initialize(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleMobOpened(MobData mob)
        {
            m_mobStats.gameObject.SetActive(true);
            m_selectedMob = mob;
            m_selectedMobChanges = m_selectedMob.CreateCopy();
            onSelectedMobChanged(mob);
        }

        private void HandleSaveMob()
        {
            m_selectedMob.Overwrite(m_selectedMobChanges);
            string assetPath = AssetDatabase.GetAssetPath(m_selectedMob);
            AssetDatabase.RenameAsset(assetPath, m_selectedMob.name);
            AssetDatabase.SaveAssetIfDirty(m_selectedMob);
            AssetDatabase.Refresh();

            onSaveMob(m_selectedMob);
        }

        private void HandleMobDeleted(MobData mob)
        {
            if (m_selectedMob == mob)
            {
                m_mobStats.gameObject.SetActive(false);
            }
        }
    }
}